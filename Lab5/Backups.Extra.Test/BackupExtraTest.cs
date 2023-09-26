using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Services;
using Backups.Models;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Test;

public class BackupExtraTest
{
    [Fact]
    public void LimitPointsCheck()
    {
        var algo = new SplitAlgorithm();
        var fs = new MemoryFileSystem();
        var repo = new InMemoryFileSystemRepository(fs, "/home/repo1");
        var limit = new CountLimitAlgo(repo, 2);
        var logger = new FileLogger("/home/repo/log.txt", repo);
        var recover = new OriginalRecover(repo);
        var task = new BackupTaskExtra("Pirozhok", algo, repo, limit, logger);
        fs.CreateDirectory("/home");
        fs.CreateDirectory("/home/repo");
        fs.CreateDirectory("/home/repo1");
        Stream stream = fs.CreateFile("/home/repo/waaaaat.txt");
        stream.Close();
        stream = fs.CreateFile("/home/repo/log.txt");
        stream.Close();
        stream = fs.CreateFile("/home/repo/oneMorePNG.png");
        stream.Close();
        stream = fs.CreateFile("/home/repo/a cat.png");
        stream.Close();
        stream = fs.CreateFile("/home/repo/log.txt");
        stream.Close();
        task.AddObject("/home/repo/a cat.png");
        task.AddObject("/home/repo/waaaaat.txt");
        task.Execute();
        task.RemoveFile("/home/repo/a cat.png");
        task.AddObject("/home/repo/oneMorePNG.png");
        task.Execute();
        task.RemoveFile("/home/repo/waaaaat.txt");
        Backup backup = task.Execute();
        IReadOnlyCollection<RestorePoint> a = backup.GetPoints;
        IEnumerator<RestorePoint> e = a.GetEnumerator();
        e.MoveNext();
        var point = e.Current;

        Assert.False(fs.DirectoryExists("/home/repo1/Pirozhok/RestorePoint(0)"));
        Assert.True(fs.FileExists("/home/repo1/Pirozhok/RestorePoint(1)/a cat.png(0).zip"));
    }
}