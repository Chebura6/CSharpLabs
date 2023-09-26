using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class CountLimitAlgo : ILimitAlgo
{
    public CountLimitAlgo(IRepository repository, long pointsLimit)
    {
        ArgumentNullException.ThrowIfNull(repository);
        if (pointsLimit < 1) throw CountLimitException.InvalidPointsLimit();

        PointsLimit = pointsLimit;
    }

    public Backup Backup { get; set; }
    internal long PointsLimit { get; }

    public void SetBackup(Backup backup)
    {
        ArgumentNullException.ThrowIfNull(backup);
        Backup = backup;
    }

    public List<RestorePoint> LimitCheck()
    {
        var totalList = new List<RestorePoint>();
        if (Backup.GetPoints.Count > PointsLimit)
        {
            long diff = Backup.GetPoints.Count - PointsLimit;
            IReadOnlyCollection<RestorePoint> collection = Backup.GetPoints;
            IEnumerator<RestorePoint> enumerator = collection.GetEnumerator();
            for (int i = 0; i < diff; ++i)
            {
                enumerator.MoveNext();
                RestorePoint point = enumerator.Current;
                totalList.Add(point);
            }

            enumerator.MoveNext();
            totalList.Add(enumerator.Current);
        }

        if (totalList.Count == Backup.GetPoints.Count) throw CountLimitException.InvalidLimitParamter();
        return totalList;
    }
}