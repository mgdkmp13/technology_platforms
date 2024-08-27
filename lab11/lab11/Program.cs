using lab11;
using System;
using System.ComponentModel;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        int n = 10;
        int k = 5;

        Console.WriteLine("Ex 1\n");
        int result1 = calculateNewtonSymbol_ex1(n, k).Result;
        Console.WriteLine($"Newton symbol for n={n} and k={k}:\t {result1}");

        Console.WriteLine("\n\nEx 2\n");
        int result2 = calculateNewtonSymbol_ex2(n, k).Result;
        Console.WriteLine($"Newton symbol for n={n} and k={k}:\t {result2}");

        Console.WriteLine("\n\nEx 3\n");
        int result3 = calculateNewtonSymbol_ex3(n, k).Result;
        Console.WriteLine($"Newton symbol for n={n} and k={k}:\t {result3}");

        int index;
        Console.Write("\nEnter a number: ");
        index = int.Parse(Console.ReadLine());
        FibonacciBackgroundWorker.StartCalculation(index);
        Console.ReadLine();

        ex5();
    }



    public static Task<int> calculateNewtonSymbol_ex1(int n, int k)
    {
        Task<int> nominatorTask = Task.Run(() =>
        {
            Console.WriteLine("Nominator: started working.");
            int nominator = 1;
            for (int i = 0; i< k; i++)
            {
                nominator *= n-i;
                Console.WriteLine($"Nominator: new value {nominator}");
                Task.Delay(10000);
            }
            Console.WriteLine("Nominator: stopped working.");
            return nominator;
        });

        Task<int> denominatorTask = Task.Run(() =>
        {
            Console.WriteLine("Denominator: started working.");
            int denominator = 1;
            for (int i = 2; i<= k; i++)
            {
                denominator *= i;
                Console.WriteLine($"Denominator: new value {denominator}");
                Task.Delay(500);
            }
            Console.WriteLine("Denominator: stopped working.");
            return denominator;
        });
        Console.WriteLine("Waiting for results!");
        return Task.WhenAll(nominatorTask, denominatorTask).ContinueWith(t => nominatorTask.Result / denominatorTask.Result);
    }

    public static async Task<int> calculateNewtonSymbol_ex2(int n, int k)
    {
        Func<int, int, int> calculateNominatorDelegate = calculateNominatorDel;
        Func<int, int, int> calculateDenominatorDelegate = calculateDenominatorDel;

        var nominatorTask = Task.Run(() => calculateNominatorDelegate(n, k));
        var denominatorTask = Task.Run(() => calculateDenominatorDelegate(n, k));
        Console.WriteLine("Waiting for results!");

        int nominator = await nominatorTask;
        int denominator = await denominatorTask;
        return nominator/denominator;
    }
    public static async Task<int> calculateNewtonSymbol_ex3(int n, int k)
    {
        Task<int> nominatorTask = calculateNominatorAsync(n, k);
        Task<int> denominatorTask = calculateDenominatorAsync(n, k);

        int nominator = await nominatorTask;
        int denominator = await denominatorTask;

        return nominator / denominator;
    }

    static int calculateNominatorDel(int n, int k)
    {
        Console.WriteLine("Nominator: started working.");
        int nominator = 1;
        for (int i = 0; i< k; i++)
        {
            nominator *= n-i;
            Console.WriteLine($"Nominator: new value {nominator}");
            Task.Delay(1000);
        }
        Console.WriteLine("Nominator: stopped working.");
        return nominator;
    }

    static int calculateDenominatorDel(int n, int k)
    {
        Console.WriteLine("Denominator: started working.");
        int denominator = 1;
        for (int i = 2; i<= k; i++)
        {
            denominator *= i;
            Console.WriteLine($"Denominator: new value {denominator}");
            Task.Delay(1000);

        }
        Console.WriteLine("Denominator: stopped working.");
        return denominator;
    }

    static async Task<int> calculateNominatorAsync(int n, int k)
    {
        Console.WriteLine("Nominator: started working.");
        int nominator = 1;
        for (int i = 0; i< k; i++)
        {
            nominator *= n-i;
            Console.WriteLine($"Nominator: new value {nominator}");
            await Task.Delay(1000);
        }
        Console.WriteLine("Nominator: stopped working.");
        return nominator;
    }

    static async Task<int> calculateDenominatorAsync(int n, int k)
    {
        Console.WriteLine("Denominator: started working.");
        int denominator = 1;
        for (int i = 2; i<= k; i++)
        {
            denominator *= i;
            Console.WriteLine($"Denominator: new value {denominator}");
            await Task.Delay(1000);

        }
        Console.WriteLine("Denominator: stopped working.");
        return denominator;
    }

    public static readonly string CompressedFileExtension = ".gz";

    public static void ex5()
    {
        string path = "C:\\Users\\Magda\\Desktop\\studia\\sem4\\platformy_technologiczne\\lab11\\test";
        compression(path).Wait();
    }

    public static async Task compression(string path)
    {
        var files = Directory.GetFiles(path);
        var compressionTasks = files.Select(async file => await compressFile(file));
        await Task.WhenAll(compressionTasks);

        Console.WriteLine("Compression finished!");
    }

    private static async Task compressFile(string file)
    {
        Console.WriteLine($"\nCompression using thread:{Environment.CurrentManagedThreadId}");
        var newFile = $"{file}{CompressedFileExtension}";

        try
        {
            await using var fileStream = File.OpenRead(file);
            await using var compressedFileStream = File.Create(newFile);
            await using var gzipStream = new GZipStream(compressedFileStream, CompressionMode.Compress);
            await fileStream.CopyToAsync(gzipStream);
            Console.WriteLine($"Compression: {file} compressed to {newFile}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: cant compress {file}");
        }
    }
}