using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace lab11
{
    public static class FibonacciBackgroundWorker
    {
        public static void StartCalculation(int index)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (sender, e) => { e.Result = Worker_DoWork(sender, e, index); };
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.RunWorkerAsync();
        }

        private static int Worker_DoWork(object? sender, DoWorkEventArgs e, int index)
        {
            int result = calculateFib(index);
            Console.WriteLine($"Result: { result}");
            return result;
        }

        private static int calculateFib(int index)
        {
           
            double percentage = 0.0;
            int prev = 0;
            int curr = 1;

            percentage = (double)1/(double)index *100.0;
            Draw_ProgressBar(percentage, curr, index, 1);
            Thread.Sleep(500);

            for (int i = 2; i <= index; i++)
            {
                int next = prev + curr;
                prev = curr;
                curr = next;

                percentage = (double)i/(double)index *100.0;
                Draw_ProgressBar(percentage, curr, index, i);
                Thread.Sleep(500);
            }
            return curr;
        }

        private static void Draw_ProgressBar(double percentage, int curr, int index, int i)
        {
            int totalWidth = 20;

            Console.CursorLeft = 0;
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.CursorLeft = 0;
           
            int progressBarWidth = (int)Math.Floor(percentage* totalWidth / 100);

            Console.Write("[");
            Console.Write(new string('O', progressBarWidth));
            Console.Write(new string('-', totalWidth - progressBarWidth));
            Console.Write($"] {Math.Floor(percentage)}%");

            Console.WriteLine($"\tStep {i}: {curr}\n");
        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
             Console.WriteLine($"Worker finished!");
        }
    }
}
