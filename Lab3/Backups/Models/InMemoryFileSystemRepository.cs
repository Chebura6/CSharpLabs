using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
using Zio;
using Zio.FileSystems;
namespace Backups;

[JsonObject(MemberSerialization.OptIn)]
public class InMemoryFileSystemRepository : IRepository
{
    // [JsonConstructor]
    public InMemoryFileSystemRepository(string pathToRepo, string pathToTaskDir)
    {
        PathToRepo = pathToRepo;
        PathToTaskDir = pathToTaskDir;
        FileSystem = new MemoryFileSystem();
    }

    public InMemoryFileSystemRepository(MemoryFileSystem fs, string path)
    {
        FileSystem = fs;
        PathToRepo = path;
    }

    [JsonProperty]
    public string Type => nameof(InMemoryFileSystemRepository);
    [JsonProperty]
    public string PathToRepo { get; }
    [JsonProperty]
    public string PathToTaskDir { get; set; }
    public IFileSystem FileSystem { get; }

    public void CreateDirectory(string path)
    {
        FileSystem.CreateDirectory(path);
    }

    public void DeleteDirectory(string path, bool isRecursive)
    {
        FileSystem.DeleteDirectory(path, isRecursive);
    }

    public string GetZipName(BackupObject obj, long restorePointsCount)
    {
        ArgumentNullException.ThrowIfNull(obj);
        if (IsDirectory(obj.Path)) return Path.GetFileName(Path.GetDirectoryName(obj.Path.ToString())) + "(" + restorePointsCount + ")";
        return Path.GetFileName(obj.Path.ToString()) + "(" + restorePointsCount + ")";
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