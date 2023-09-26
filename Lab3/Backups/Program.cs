using System.IO.Compression;
using Backups.Models;
using Zio;
using Zio.FileSystems;
using PhysicalFileSystem = Zio.FileSystems.PhysicalFileSystem;

namespace Backups;

public static class Program
{
    public static void Main()
    {
        var algo = new SplitAlgorithm();
        var fs = new PhysicalFileSystem();
        var repo = new PhysicalFileSystemRepository(fs, "/Users/artemparfenov/kuku");
        var task = new BackupTask("Pirozhok", algo, repo);
        task.AddObject("/Users/artemparfenov/Desktop/Source/IMG_0312.png");
        task.AddObject("/Users/artemparfenov/Desktop/Source/what");
        task.AddObject("/Users/artemparfenov/Desktop/Source/Task 2.png");
        task.AddObject("/Users/artemparfenov/Desktop/Source/Summarizer.pdf");
        task.AddObject("/Users/artemparfenov/Desktop/Source/OS_Lab5.pdf");
        task.Execute();
        task.RemoveFile("/Users/artemparfenov/Desktop/Source/Task 2.png");
        task.Execute();
    }
}