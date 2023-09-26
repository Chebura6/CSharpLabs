using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface IRecover
{
    void RecoverPoint(RestorePoint restorePoint);
}