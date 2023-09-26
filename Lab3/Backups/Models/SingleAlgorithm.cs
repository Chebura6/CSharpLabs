using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Models;

[JsonObject(MemberSerialization.OptIn)]
public class SingleAlgorithm : IAlgorithm
{
    // [JsonConstructor]
    public SingleAlgorithm()
    {
        Archiver = new Archiver();
    }

    [JsonProperty]
    public string Type => nameof(SingleAlgorithm);
    private IArchiver Archiver { get; }

    public List<Storage> ArchiveAlgo(List<BackupObject> backupObjects, IRepository repository, long restorePointsCount)
    {
        if (backupObjects.Count < 2) throw SingleAlgorithmException.InvalidCountOfObjects();
        ArgumentNullException.ThrowIfNull(backupObjects);

        string pathToRestPoint = Path.Combine(repository.PathToTaskDir, "RestorePoint(" + restorePointsCount + ")");
        repository.CreateDirectory(pathToRestPoint);

        string zipName = repository.GetZipName(backupObjects, restorePointsCount);
        var zipArch = Archiver.CreateArchive(pathToRestPoint, zipName, repository);

        List<Storage> storage = new List<Storage> { new Storage(Path.Combine(pathToRestPoint, zipName) + ".zip") };

        foreach (BackupObject backupObject in backupObjects)
        {
            if (repository.IsDirectory(backupObject.Path))
            {
                Archiver.AddFolderInExistArchive(backupObject.Path, repository, zipArch);
                storage[storage.Count - 1].PathToBackupObject.Add(backupObject.Path);
            }
            else
            {
                Archiver.AddFileInExistArchive(backupObject.Path, repository, zipArch);
                storage[storage.Count - 1].PathToBackupObject.Add(backupObject.Path);
            }
        }

        Archiver.DeleteArchive(zipArch);
        return storage;
    }
}