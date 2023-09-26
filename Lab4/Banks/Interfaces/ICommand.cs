using Banks.Commands;

namespace Banks.Interfaces;

public interface ICommand
{
    CommandState State { get; }

    void Execute();

    void Revert();
}