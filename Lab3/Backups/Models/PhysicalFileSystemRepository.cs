using Backups.Exceptions;
using Backups.Interfaces;
using Newtonsoft.Json;
using Zio;
using Zio.FileSystems;

namespace Backups.Models;

[JsonObject(MemberSerialization.OptIn)]
public class PhysicalFileSystemRepository : IRepository
{
    // [JsonConstructor]
    public PhysicalFileSystemRepository(string pathToRepo, string pathToTaskDir)
    {
        PathToRepo = pathToRepo;
        PathToTaskDir = pathToTaskDir;
        FileSystem = new PhysicalFileSystem();
    }

    public PhysicalFileSystemRepository(PhysicalFileSystem fs, string path)
    {
        ArgumentNullException.ThrowIfNull(fs);
        PathToRepo = path;
        FileSystem = fs;
    }

    [JsonProperty]
    public string Type => nameof(PhysicalFileSystemRepository);
    [JsonProperty]
    public string PathToRepo { get; }
    [JsonProperty]
    public string PathToTaskDir { get; set; }
    public IFileSystem FileSystem { get; }
    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public void DeleteDirectory(string path, bool isRecursive)
    {
        FileSystem.DeleteDirectory(path, isRecursive);
    }

    public string GetZipName(BackupObject obj, long restorePointsCount)
    {
        ArgumentNullException.ThrowIfNull(obj);
        return Path.GetFileName(obj.Path) + "(" + restorePointsCount + ")";
    }

    public string GetZipName(List<BackupObject> objects, long restorePointsCount)
    {
        ArgumentNullException.ThrowIfNull(objects);
        return "RestorePoint(" + restorePointsCount + ")";
    }

    public bool IsDirectory(string path)
    {
        if (FileSystem.FileExists(path)) return false;
        if (FileSystem.DirectoryExists(path)) return true;
        throw RepositoryException.InvalidPath();
    }
}