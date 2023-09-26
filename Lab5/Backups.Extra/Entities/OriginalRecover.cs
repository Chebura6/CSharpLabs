using System.IO.Compression;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class OriginalRecover : IRecover
{
    public OriginalRecover(IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        Recoverer = new Recoverer(repository);
    }

    internal Recoverer Recoverer { get; }

    public void RecoverPoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        foreach (Storage restorePointStorage in restorePoint.Storages)
        {
            if (restorePointStorage.PathToBackupObject.Count == 1)
            {
                Recoverer.RecoverObject(restorePointStorage.Path, Path.GetDirectoryName(restorePointStorage.PathToBackupObject[0]));
                continue;
            }

            Recoverer.RecoverObject(restorePointStorage.Path, Path.GetDirectoryName(restorePointStorage.Path));
            string[] files = Directory.GetFiles(Path.GetDirectoryName(restorePointStorage.Path));
            foreach (string file in files)
            {
                string destPath = restorePointStorage.PathToBackupObject
                    .FirstOrDefault(x => Path.GetFileName(x).Equals(file));
                Recoverer.RecoverObject(file, destPath);
            }
        }
    }
}