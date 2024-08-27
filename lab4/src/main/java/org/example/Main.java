package org.example;
import javax.persistence.*;
import java.util.List;
import java.util.Scanner;


//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {

    public static void main(String[] args) {
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("myPersistenceUnit");
        EntityManager em = factory.createEntityManager();
        // em.getTransaction().begin();

        Scanner scanner = new Scanner(System.in);
        String input = "start";

        while(input != null){
            if(input.equals("create")) {
                createTower(em);
            }
            else if(input.equals("add")) {
                addMage(em);
            }
            else if(input.equals("delete mage")){
                deleteMage(em);
            }
            else if(input.equals("delete tower")){
                deleteTower(em);
            }
            else if(input.equals("show")){
                showMages(em);
                showTowers(em);
            }
            else if(input.equals("show mages")){
                showMages(em);
            }
            else if(input.equals("show towers")){
                showTowersWithMages(em);
            }
            //finds mages with level above n
            else if(input.equals("query1")){
                findMageWithLevelAbove(em);
            }
            //finds tower with height below n
            else if(input.equals("query2")){
                findTowersWithHeightBelow(em);
            }
            //finds mages with level above n from tower t
            else if(input.equals("query3")){
                findMageFromTowers(em);
            }
            else if(input.equals("quit")){
                break;
            }
            displayOptions();
            input = scanner.nextLine();
        }

        //em.getTransaction().commit();
        factory.close();
        em.close();
    }

    private static void displayOptions(){
        System.out.println("What do you want to do?");
        System.out.println("Enter 'create' to create a new tower, 'add' to add a new mage, 'delete mage' to delete a mage, 'delete tower' to delete a tower, ");
        System.out.println("Enter 'show' to show all object in database, 'show mages' to show all mages, 'show towers' to show all towers");
        System.out.println("Enter 'query1' to find mages with level above n, 'query2' to find towers with height below n, 'query3' to find mages with level above n from tower t");
        System.out.println("Enter 'quit' to quit the program");
    }
    private static void createTower(EntityManager em){
        Scanner scanner = new Scanner(System.in);
        System.out.println("How many towers do you want to create?");
        int number = scanner.nextInt();
        scanner.nextLine();
        for(int i = 0; i < number; i++) {
            em.getTransaction().begin();
            System.out.println("Enter the tower's name: ");
            String towerName = scanner.nextLine();
            System.out.println("Enter the tower's height: ");
            int height = scanner.nextInt();
            scanner.nextLine();
            Tower existingTower = em.find(Tower.class, towerName);
            if(existingTower == null) {
                System.out.println(towerName + " successfully created");
                Tower newTower = new Tower(towerName, height);
                em.persist(newTower);
                em.getTransaction().commit();
            } else {
                System.out.println("Tower with name " + towerName + " already exists.");
            }
        }
    }

    private static void addMage(EntityManager em){
        Scanner scanner = new Scanner(System.in);
        System.out.println("How many mages do you want to add?");
        int number = scanner.nextInt();
        scanner.nextLine(); // consume newline character
        for(int i = 0; i < number; i++) {
            em.getTransaction().begin();
            System.out.println("Enter the mage's name: ");
            String mageName = scanner.nextLine();
            System.out.println("Enter the mage's level: ");
            int level = scanner.nextInt();
            scanner.nextLine();
            System.out.println("Enter the mage's tower's name: ");
            String towerName = scanner.nextLine();
            Tower tower = em.find(Tower.class, towerName);
            if (tower != null) {
                System.out.println(mageName + " successfully added to tower " + towerName);
                Mage m1 = new Mage(mageName, level, tower);
                em.persist(m1);
                em.getTransaction().commit();
            } else {
                System.out.println("No tower named " + towerName);
                em.getTransaction().rollback();
            }
        }
    }

    private static void deleteMage(EntityManager em){
        Scanner scanner = new Scanner(System.in);
        System.out.println("How many mages do you want to delete?");
        int number = scanner.nextInt();
        for(int i = 0; i < number; i++) {
            em.getTransaction().begin();
            System.out.println("Enter the mage's name: ");
            scanner.nextLine();
            String mageName = scanner.nextLine();

            Mage mageDel = em.find(Mage.class, mageName);
            if (mageDel != null) {
                Tower t = mageDel.getTower();
                t.getMages().remove(mageDel);
                em.merge(t);
                System.out.println(mageName + " successfully deleted");
                em.remove(mageDel);
                em.getTransaction().commit();
            }
            else{
                System.out.println("No tower named " + mageName);
                em.getTransaction().rollback();
            }
        }
    }

    private static void deleteTower(EntityManager em){
        Scanner scanner = new Scanner(System.in);
        System.out.println("How many towers do you want to delete?");
        int number = scanner.nextInt();
        for(int i = 0; i < number; i++) {
            em.getTransaction().begin();
            System.out.println("Enter the tower's name: ");
            scanner.nextLine();
            String towerName = scanner.nextLine();

            Tower towerDel = em.find(Tower.class, towerName);
            if (towerDel != null) {
                List<Mage> mages = towerDel.getMages();
                for(Mage m : mages){
                    em.remove(m);
                }
                em.remove(towerDel);
                em.getTransaction().commit();
            } else {
                System.out.println("Tower " + towerName + " not found.");
                em.getTransaction().rollback();
            }
        }
    }

    private static void showMages(EntityManager em){
        Query query = em.createQuery("Select m from Mage m");
        List<Mage> list = query.getResultList();
        if(!list.isEmpty()) {
            System.out.println("Mages:");
            for (Mage m : list) {
                System.out.println(m);
            }
        }
    }

    private static void showTowers(EntityManager em){
        Query query = em.createQuery("Select t from Tower t");
        List<Tower> list = query.getResultList();
        if(!list.isEmpty()) {
            System.out.println("Towers:");
            for (Tower t : list) {
                System.out.println(t);
            }
        }
    }

    private static void showTowersWithMages(EntityManager em){
        Query query = em.createQuery("Select t from Tower t");
        List<Tower> list = query.getResultList();
        if(!list.isEmpty()) {
            System.out.println("Towers:");
            for (Tower t : list) {
                System.out.println(t);
                for(Mage m : t.getMages()){
                    System.out.println("~~" + m);
                }
            }
        }
    }

    private static void findMageWithLevelAbove(EntityManager em){
        Scanner scanner = new Scanner(System.in);
        System.out.println("Query1: Choose mages with level above n. Enter n:");
        int number = scanner.nextInt();
        scanner.nextLine();
        Query query = em.createQuery("Select m from Mage m where m.level > :level");
        query.setParameter("level", number);
        List list = query.getResultList();
        if(!list.isEmpty()) {
            System.out.println("Mages with level above " + number + ":");
            for (Object t : list) {
                System.out.println(t);
            }
        }
        else{
            System.out.println("No mages with level above " + number);
        }
    }

    private static void findTowersWithHeightBelow(EntityManager em){
        Scanner scanner = new Scanner(System.in);
        System.out.println("Query2: Choose towers with height below n. Enter n:");
        int number = scanner.nextInt();
        scanner.nextLine();
        Query query = em.createQuery("Select t from Tower t where t.height < :height");
        query.setParameter("height", number);
        List list = query.getResultList();
        if(!list.isEmpty()) {
            System.out.println("Towers with height below " + number + ":");
            for (Object t : list) {
                System.out.println(t);
            }
        }
        else{
            System.out.println("No towers with height below " + number);
        }
    }

    private static void findMageFromTowers(EntityManager em){
        Scanner scanner = new Scanner(System.in);
        System.out.println("Query3: Choose mages with level above n from tower t. Enter n:");
        int number = scanner.nextInt();
        scanner.nextLine();
        System.out.println("Enter tower t:");
        String tower = scanner.nextLine();

        Tower towerFind = em.find(Tower.class, tower);
        if (towerFind == null) {
            System.out.println("No tower named " + tower);
            //em.getTransaction().rollback();
        }
        else {
            Query query = em.createQuery("SELECT m FROM Mage m WHERE m.level > :level AND m.tower.name = :tower");
            query.setParameter("level", number);
            query.setParameter("tower", tower);
            List list = query.getResultList();
            if (!list.isEmpty()) {
                System.out.println("Mages with level above " + number + " from tower " + tower + ":");
                for (Object t : list) {
                    System.out.println(t);
                }
            } else {
                System.out.println("No mages with level above " + number + " from tower " + tower);
            }
        }
    }

}