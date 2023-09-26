using Backups.Models;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupTest
{
    [Fact]
    private void IsMemoryFSWorkingSuccessfully()
    {
        var algo = new SplitAlgorithm();
        var fs = new MemoryFileSystem();
        fs.CreateDirectory("/home");
        fs.CreateDirectory("/home/repo");
        fs.CreateDirectory("/home/repo1");
        Stream stream = fs.CreateFile("/home/repo/waaaaat.txt");
        stream.Close();
        stream = fs.CreateFile("/home/repo/a cat.png");
        stream.Close();
        var repo = new InMemoryFileSystemRepository(fs, "/home/repo1");
        var task = new BackupTask("Pirozhok", algo, repo);
        task.AddObject("/home/repo/a cat.png");
        task.AddObject("/home/repo/waaaaat.txt");
        task.Execute();
        task.RemoveFile("/home/repo/a cat.png");
        task.Execute();
        Assert.True(fs.FileExists("/home/repo1/Pirozhok/RestorePoint(0)/waaaaat.txt(0).zip"));
        Assert.True(fs.FileExists("/home/repo1/Pirozhok/RestorePoint(0)/a cat.png(0).zip"));
        Assert.True(fs.FileExists("/home/repo1/Pirozhok/RestorePoint(1)/waaaaat.txt(1).zip"));
        Assert.False(fs.FileExists("/home/repo1/Pirozhok/RestorePoint(1)/a cat.png(1).zip"));
    }
}