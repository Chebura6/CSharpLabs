using Banks.Interfaces;

namespace Banks.Entities;

public class TimeManager : ITimeManager
{
    private const int _hoursPerDay = 24;
    public TimeManager()
    {
        CurrentTime = DateTime.Now;
    }

    public delegate void MethodContainer();
    public delegate void OneMoreMethodContainer(DateTime dateTime);
    public event MethodContainer NewDay;
    public event MethodContainer NewMonth;
    public event OneMoreMethodContainer ChangeAccountsTime;
    public event OneMoreMethodContainer TimeDecreased;
    public DateTime CurrentTime { get; private set; }

    public DateTime IncreaseTimeFor1Hour()
    {
        int prevDay = CurrentTime.Day;
        if (CurrentTime.Hour == 23)
        {
            NewDay?.Invoke();
        }

        CurrentTime = CurrentTime.AddHours(1);
        ChangeAccountsTime?.Invoke(CurrentTime);
        if (CurrentTime.Day == 1 && prevDay > 27)
        {
            NewMonth?.Invoke();
        }

        prevDay = CurrentTime.Day;
        return CurrentTime;
    }

    public void IncreaseTimeFor1Day()
    {
        for (int i = 0; i < _hoursPerDay; ++i)
        {
            IncreaseTimeFor1Hour();
        }
    }

    public void IncreaseTimeFor1Month()
    {
        int day = CurrentTime.Day;
        IncreaseTimeFor1Day();
        while (CurrentTime.Day != day)
        {
            IncreaseTimeFor1Day();
        }
    }

    public DateTime DecreaseTimeFor1Hour()
    {
        CurrentTime = CurrentTime.Subtract(new TimeSpan(1, 0, 0));
        TimeDecreased?.Invoke(CurrentTime);
        return CurrentTime;
    }
}