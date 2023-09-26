using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface ILimitAlgo
{
    Backup Backup { get; set; }
    List<RestorePoint> LimitCheck();
}