using Backups.Models;
using Zio;

namespace Backups.Interfaces;

public interface IRepository
{
    string Type { get; }
    string PathToRepo { get; }
    string PathToTaskDir { get; set; }
    IFileSystem FileSystem { get; }
    void CreateDirectory(string path);
    void DeleteDirectory(string path, bool isRecursive);
    string GetZipName(BackupObject obj, long restorePointsCount);
    string GetZipName(List<BackupObject> objects, long restorePointsCount);
    bool IsDirectory(string path);
}