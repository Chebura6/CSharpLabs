using System.Collections;
using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Services;
using Backups.Models;
using Newtonsoft.Json;
using Zio.FileSystems;
namespace Backups.Extra;

public class Program
{
    public static void Main()
    {
        var algo = new SplitAlgorithm();
        var fs = new PhysicalFileSystem();
        var logger = new ConsoleLogger();
        var repo = new PhysicalFileSystemRepository(fs, "/Users/artemparfenov/kuku2");
        var countLimitAlgo = new CountLimitAlgo(repo, 15);
        var timeLimitAlgo = new DateLimitAlgo(repo, new DateTime(2022, 12, 10, 16, 44, 49));

        var task = new BackupTaskExtra("testing", algo, repo, timeLimitAlgo, logger);

        task.AddObject("/Users/artemparfenov/Desktop/Source/Summarizer.pdf");
        task.AddObject("/Users/artemparfenov/Desktop/Source/IMG_0312.png");
        task.AddObject("/Users/artemparfenov/Desktop/Source/what");
        task.AddObject("/Users/artemparfenov/Desktop/Source/Task 2.png");
        task.AddObject("/Users/artemparfenov/Desktop/Source/OS_Lab5.pdf");
        Backup backup = task.Execute();

        task.RemoveFile("/Users/artemparfenov/Desktop/Source/IMG_0312.png");
        task.RemoveFile("/Users/artemparfenov/Desktop/Source/OS_Lab5.pdf");
        backup = task.Execute();
        task.AddObject("/Users/artemparfenov/Desktop/Source/Task 2.png");
        task.AddObject("/Users/artemparfenov/Desktop/Source/OS_Lab5.pdf");
        task.Execute();
        task.RemoveFile("/Users/artemparfenov/Desktop/Source/what");
        task.Execute();
        task.AddObject("/Users/artemparfenov/Desktop/Source/OS_Lab5.pdf");
        task.AddObject("/Users/artemparfenov/Desktop/Source/Task 2.png");
        task.Execute();

        // var recover = new DifferentLocationRecover(repo);
        // IReadOnlyCollection<RestorePoint> a = backup.GetPoints;
        // IEnumerator<RestorePoint> e = a.GetEnumerator();
        // while (e.MoveNext())
        // {
        //     var point = e.Current;
        //     recover.RecoverPoint(point);
        //     break;
        // }

        // task.RemoveFile("/Users/artemparfenov/Desktop/Source/Task 2.png");
        // task.Execute();

        // string json = JsonConvert.SerializeObject(task, Formatting.Indented);
        // Console.WriteLine(json);
        // var logger = new ConsoleLogger("what");
        // var user = new User(logger, true);
        //
        // string json = JsonConvert.SerializeObject(user, Formatting.Indented);
        // File.WriteAllText($"{"/Users/artemparfenov/BackupState2.json"}", json);
        // User usr = JsonConvert.DeserializeObject<User>(File.ReadAllText("/Users/artemparfenov/BackupState2.json"));
        // Console.Write("ghj");
    }
}