using System.IO.Compression;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class DifferentLocationRecover : IRecover
{
    public DifferentLocationRecover(IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        Repository = repository;
        Recoverer = new Recoverer(repository);
    }

    public IRepository Repository { get; }
    internal Recoverer Recoverer { get; }

    public void RecoverPoint(RestorePoint restorePoint)
    {
        foreach (Storage restorePointStorage in restorePoint.Storages)
        {
            Recoverer.RecoverObject(restorePointStorage.Path, Repository.PathToRepo);
        }
    }
}