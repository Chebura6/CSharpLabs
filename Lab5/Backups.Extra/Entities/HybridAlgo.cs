using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class HybridAlgo
{
    internal ILimitAlgo Algo { get; }

    public List<RestorePoint> Conjunction(params ILimitAlgo[] algos)
    {
        var tempList = new List<List<RestorePoint>>();
        foreach (ILimitAlgo limitAlgo in algos)
        {
            tempList.Add(limitAlgo.LimitCheck());
        }

        var totalList = new List<RestorePoint>(tempList[0]);
        for (int i = 1; i < tempList.Count; ++i)
        {
            totalList = totalList.FindAll(x => tempList[i].Contains(x));
        }

        return totalList;
    }

    public List<RestorePoint> Disjunction(params ILimitAlgo[] algos)
    {
        var tempList = new List<List<RestorePoint>>();
        foreach (ILimitAlgo limitAlgo in algos)
        {
            tempList.Add(limitAlgo.LimitCheck());
        }

        var totalList = new List<RestorePoint>(tempList[0]);
        for (int i = 1; i < tempList.Count; ++i)
        {
            totalList.AddRange(tempList[i]);
        }

        return totalList.Distinct().ToList();
    }
}