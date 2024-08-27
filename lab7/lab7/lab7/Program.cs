using System;
using System.IO;
using static System.IO.DirectoryInfo;
using static System.IO.FileSystemInfo;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.Serialization;


static class Program {

    static void Main(string[] args)
    {
        AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);

        if (args.Length == 0) { 
        Console.WriteLine("No arguments error!");
            return;
        }

        string path = args[0];
        if(!Directory.Exists(path))
        {
            Console.WriteLine("Path does not exist!"); 
            return;
        }

        try
        {
            if(Directory.Exists(path))
            {
                displayFiles(path, 0);
                Console.WriteLine("");
                DateTime oldest = GetOldestItemDate(new DirectoryInfo(path));
                Console.WriteLine($"Najstarszy plik: {oldest}");
                Console.WriteLine("");
                SortedDictionary<string, long> dictionary = createDictionary(path);
                string serializePath = "C:\\Users\\Magda\\Desktop\\studia\\sem4\\platformy_technologiczne\\lab7\\lab7\\lab7\\dictionary.dat";
                binarySerialize(dictionary, serializePath);
                SortedDictionary<string, long> desializedDictionary = binaryDeserialize(serializePath);
                foreach (var item in desializedDictionary)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Error! {ex.Message}");
        }
    }

    static void displayFiles(string path, int deepness)
    {
        string[] subdirectories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        foreach (string file in files)
        {
            long sizeB = new FileInfo(file).Length;
            FileInfo fsi = new FileInfo(file);
            string rahs = GetDosAttributes(fsi);
            Console.WriteLine($"{new string(' ', deepness * 4)}{Path.GetFileName(file)} {sizeB} bajtow {rahs}");
        }

        foreach (string subdirectory in subdirectories)
        {
            FileInfo fsi = new FileInfo(subdirectory);
            string rahs = GetDosAttributes(fsi);
            int subSize = Directory.GetDirectories(subdirectory).Length;
            subSize += Directory.GetFiles(subdirectory).Length;
            Console.WriteLine($"{new string(' ', deepness * 4)}{Path.GetFileName(subdirectory)} ({subSize}) {rahs}\\");
            displayFiles(subdirectory, deepness + 1);
        }
    }

    public static DateTime GetOldestItemDate(this DirectoryInfo directory)
    {
        DateTime oldestDate = DateTime.Now;

        foreach (var file in directory.GetFiles())
        {
            if (file.LastWriteTime < oldestDate)
            {
                oldestDate = file.LastWriteTime;
            }
        }

        foreach (var subdirectory in directory.GetDirectories())
        {
            DateTime subdirectoryOldestDate = subdirectory.GetOldestItemDate();
            if (subdirectoryOldestDate < oldestDate)
            {
                oldestDate = subdirectoryOldestDate;
            }
        }
        return oldestDate;
    }

    public static string GetDosAttributes(this FileSystemInfo fileInfo)
    {
        char read = (fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? 'r' : '-';
        char archive = (fileInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive ? 'a' : '-';
        char hidden = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? 'h' : '-';
        char system = (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System ? 's' : '-';

        return $"{read}{archive}{hidden}{system}";
    }

    static SortedDictionary<string, long> createDictionary(string path)
    {
        SortedDictionary<string, long> dictionary = new SortedDictionary<string, long>(new CustomStringComparer());

        string[] subdirectories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            string fileName = Path.GetFileName(file);
            if (!dictionary.ContainsKey(fileName))
            {
                dictionary.Add(fileName, fileInfo.Length);
            }
        }

        foreach (string subdirectory in subdirectories)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(subdirectory);
            string subdirectoryName = Path.GetFileName(subdirectory);
            if (!dictionary.ContainsKey(subdirectoryName))
            {
                int subSize = Directory.GetDirectories(subdirectory).Length;
                subSize += Directory.GetFiles(subdirectory).Length;
                dictionary.Add(subdirectoryName, subSize);
            }
        }
       
        return dictionary;
    }


    [Serializable]
    class CustomStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int compareLength = x.Length.CompareTo(y.Length);
            if (compareLength != 0)
            {
                return compareLength;
            }
            else
            {
                return string.Compare(x, y, StringComparison.Ordinal);
            }
        }
    }

    public static void binarySerialize(SortedDictionary<string, long> dict, string filePath)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            binaryFormatter.Serialize(fileStream, dict);
        }
    }

    public static SortedDictionary<string, long> binaryDeserialize(string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        {
            return (SortedDictionary<string, long>)formatter.Deserialize(fileStream);
        }
    }
};

