package org.example;

import java.util.LinkedList;
import java.util.Queue;

public class TaskManager<T> {
    private Queue<T> taskQueue = new LinkedList<>();

    public boolean stopApp;
    public TaskManager(Queue<T> taskQueue) {
        this.taskQueue = taskQueue;
        this.stopApp = false;
    }

    public Queue<T> getTaskQueue() {
        return taskQueue;
    }

    public void setTaskQueue(Queue<T> taskQueue) {
        this.taskQueue = taskQueue;
    }

    public synchronized void put(T number){
        taskQueue.add(number);
        notifyAll();
    }

    public synchronized T take() throws InterruptedException {
        while (taskQueue.isEmpty()) {
            wait();
        }
        return taskQueue.remove();
    }

    public void interruptApp(){
        this.stopApp = true;
    }
}
