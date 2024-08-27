package org.example;

import java.util.LinkedList;
import java.util.Queue;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) throws InterruptedException {
        int numberOfThreads = 1;
        for (String arg : args) {
            numberOfThreads = Integer.parseInt(arg);
        }
        System.out.println("Number of threads: " + numberOfThreads);

        System.out.println("Write numbers to check if they are prime");
        System.out.println("Write 'exit' to exit the program");

        TaskManager taskManager = new TaskManager(new LinkedList<>());
        Results results = new Results(new LinkedList<>());


        Thread[] threads = new Thread[numberOfThreads];
        for (int i = 0; i < numberOfThreads; i++) {
            CalculationManager calculationTask = new CalculationManager(taskManager, results);
            threads[i] = new Thread(calculationTask, "Thread " + i);
            threads[i].start();
        }

        Scanner scanner = new Scanner(System.in);
        String input = scanner.next();
        while(!input.equals("exit")){

            int number=0;
            number = Integer.parseInt(input);
            taskManager.put(number);
            input = scanner.next();
        }
        scanner.close();

        taskManager.interruptApp();


        while(!taskManager.getTaskQueue().isEmpty()){
            Thread.sleep(4000);
        }

        System.out.println("Exit process!");
        results.displayResults();
    }
}