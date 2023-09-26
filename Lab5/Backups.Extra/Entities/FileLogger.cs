using System.Text;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class FileLogger : ILogger
{
    public FileLogger(string filePath, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        if (string.IsNullOrEmpty(filePath)) throw new Exception();
        FilePath = filePath;
        Repository = repository;
    }

    internal string FilePath { get; }
    internal IRepository Repository { get; }

    public void MakeLog(string logMessage)
    {
        var stream = Repository.FileSystem.OpenFile(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
        byte[] bytes = Encoding.ASCII.GetBytes(logMessage);
        stream.Write(bytes);
        stream.Close();
    }
}