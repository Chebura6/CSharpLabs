using Banks.Services;

namespace Banks.Interfaces;

public interface ITimeManager
{
    // internal CentralBank CentralBank { get; }
    DateTime CurrentTime { get; }
    DateTime IncreaseTimeFor1Hour();
    void IncreaseTimeFor1Day();
    void IncreaseTimeFor1Month();
    DateTime DecreaseTimeFor1Hour();
}