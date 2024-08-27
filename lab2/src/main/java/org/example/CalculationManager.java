package org.example;

public class CalculationManager implements Runnable{
    private TaskManager<Integer> taskManager;
    private Results results;

    public CalculationManager(TaskManager<Integer> taskManager, Results results) {
        this.taskManager = taskManager;
        this.results = results;
    }

    @Override
    public void run() {
        try {
            while (!this.taskManager.stopApp || !this.taskManager.getTaskQueue().isEmpty()) {
                Integer task = taskManager.take();
                if (task != null) {
                    Boolean isPrime = isPrime(task);
                    if (isPrime) {
                        results.addResult(task);
                        System.out.println(Thread.currentThread().getName() + ": " + task);
                    }
                }
            }
            System.out.println(Thread.currentThread().getName() + " stopped!");
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    }

    private boolean isPrime(Integer a) throws InterruptedException {
        Thread.sleep(2000);
        if (a <= 1) {
            return false;
        }

        for (Integer i = 2; i <= Math.sqrt(a); i++) {
            if (a % i == 0) {
                return false;
            }
        }
        return true;
    }

}
