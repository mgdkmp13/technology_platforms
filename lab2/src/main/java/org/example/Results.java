package org.example;


import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

public class Results {

    private final List<Integer> results;

    public Results(List<Integer> taskQueue) {
        this.results = taskQueue;
    }

    public synchronized void addResult(Integer result) {
        results.add(result);
    }

    public synchronized void displayResults() {
        System.out.println("Prime numbers:");
        for (Integer result : results) {
            System.out.println(result);
        }
    }
}
