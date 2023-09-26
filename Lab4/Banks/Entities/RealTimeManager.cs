using Banks.Interfaces;

namespace Banks.Entities;

public class RealTimeManager : ITimeManager
{
    public DateTime CurrentTime { get; }
    public DateTime IncreaseTimeFor1Hour()
    {
        return CurrentTime;
    }

    public void IncreaseTimeFor1Day()
    {
        for (int i = 0; i < 24; ++i)
        {
            IncreaseTimeFor1Hour();
        }
    }

    public void IncreaseTimeFor1Month()
    {
        for (int i = 0; i < 30; ++i)
        {
            IncreaseTimeFor1Day();
        }
    }

    public DateTime DecreaseTimeFor1Hour()
    {
        return CurrentTime;
    }
}