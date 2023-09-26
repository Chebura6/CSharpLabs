using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class DateLimitAlgo : ILimitAlgo
{
    public DateLimitAlgo(IRepository repository, DateTime oldestRestorePointLimit)
    {
        ArgumentNullException.ThrowIfNull(repository);
        Repository = repository;
        TimeLimit = oldestRestorePointLimit;
    }

    public Backup Backup { get; set; }
    internal IRepository Repository { get; }
    internal DateTime TimeLimit { get; }
    public void SetBackup(Backup backup)
    {
        ArgumentNullException.ThrowIfNull(backup);
        Backup = backup;
    }

    public List<RestorePoint> LimitCheck()
    {
        var totalList = new List<RestorePoint>();
        IReadOnlyCollection<RestorePoint> collection = Backup.GetPoints;
        IEnumerator<RestorePoint> enumerator = collection.GetEnumerator();
        while (enumerator.MoveNext())
        {
            RestorePoint point = enumerator.Current;
            if (point?.Timestamp < TimeLimit)
            {
                totalList.Add(point);
            }
        }

        if (totalList.Count == Backup.GetPoints.Count) throw CountLimitException.InvalidPointsLimit();
        return totalList;
    }
}