package org.example.classes;

import java.util.*;

public class Commandor implements Comparable<Commandor> {
    private String name;
    private int level;
    private double power;
    private Set<Commandor> subordinates;
    private Map<Commandor, Integer> statisticMap;

    public Commandor(String name, int level, double power, String sortingType){
        this.name=name;
        this.level=level;
        this.power=power;
        this.subordinates = newSortSet(sortingType);
        this.statisticMap = newSortMap(sortingType);
    }

    @Override
    public String toString() {
        return "Commandor{name='" + name + "', level=" + level + ", power=" + power + "}";
    }

    public String getName(){
        return this.name;
    }
    public int getLevel(){
        return this.level;
    }
    public double getPower(){
        return this.power;
    }
    public Set<Commandor> getsubordinates(){
        return this.subordinates;
    }

    public Map<Commandor, Integer> getStatisticMap() {
        return statisticMap;
    }

    public void setStatisticMap(Map<Commandor, Integer> statisticMap) {
        this.statisticMap = statisticMap;
    }

    public void setName(String name){
        this.name=name;
    }
    public void setLevel(int level){
        this.level=level;
    }
    public void setPower(double power){
        this.power=power;
    }
    public void setsubordinates(Set<Commandor> subordinates){
        this.subordinates=subordinates;
    }

    public void addApprentice(Commandor apprentice) {
        subordinates.add(apprentice);
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Commandor Commandor = (Commandor) o;
        return level == Commandor.level && Double.compare(power, Commandor.power) == 0 && Objects.equals(name, Commandor.name) && Objects.equals(subordinates, Commandor.subordinates);
    }

    @Override
    public int hashCode() {
        return Objects.hash(name, level, power, subordinates);
    }

    @Override
    public int compareTo(Commandor m1) {
        // Standardowy porzadek: name, level, power
        int isSame = getName().compareTo(m1.getName());

        if (isSame == 0) {
            isSame = Integer.compare(getLevel(), m1.getLevel());
            if (isSame == 0) {
                isSame = Double.compare(getPower(), m1.getPower());
            }
        }
        return isSame;
    }

    private static Set<Commandor> newSortSet(String sortingType){
        Set<Commandor> newSet;

        if(sortingType.equals("normal")){
            newSet = new TreeSet<>();
        }
        else if(sortingType.equals("alternative")){
            CommandorComparator comp1 = new CommandorComparator();
            newSet = new TreeSet<>(comp1);
        }
        else if(sortingType.equals("none")){
            newSet = new HashSet<>();
        }
        else{
            throw new IllegalArgumentException("Wrong sorting type. Enter 'none', 'normal', 'alternative'");
        }
        return newSet;
    }

    private static Map<Commandor, Integer> newSortMap(String sortingType){
        Map<Commandor, Integer> newMap;
        if(sortingType.equals("normal")){
            newMap = new TreeMap<>();
        }
        else if(sortingType.equals("alternative")){
            CommandorComparator comp1 = new CommandorComparator();
            newMap = new TreeMap<>(comp1);
        }
        else if(sortingType.equals("none")){
            newMap = new HashMap<>();
        }
        else{
            throw new IllegalArgumentException("Wrong sorting type. Enter 'none', 'normal', 'alternative'");
        }
        return newMap;
    }


    public static void showSubordinates(Commandor Commandor, String section) {;
        System.out.println(section + Commandor);
        for (Commandor apprentice : Commandor.getsubordinates()) {
            showSubordinates(apprentice, section + "-");
        }
    }


    public void generateStatistics(String sortingType) {
        countsubordinates(this, this.getStatisticMap());
        showStatistics();
    }

    private int countsubordinates(Commandor Commandor, Map<Commandor, Integer> statisticsMap) {
        int result = Commandor.getsubordinates().size();
        for (Commandor apprentice : Commandor.getsubordinates()) {
            result += countsubordinates(apprentice, statisticsMap);
        }
        statisticsMap.put(Commandor, result);
        return result;
    }

    public void showStatistics(){
        for (Map.Entry<Commandor, Integer> begin : this.getStatisticMap().entrySet()) {
            System.out.println(begin.getKey() + " has " + begin.getValue() + " subordinates.");
        }
    }
}

