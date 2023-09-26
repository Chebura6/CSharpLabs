using Banks.Interfaces;
using Banks.Models;

namespace Banks.Commands;

public class InitialAmountCommand : ICommand
{
    private CommandState _commandState;
    private TransactionContext _context;

    public InitialAmountCommand(TransactionContext context)
    {
        _commandState = CommandState.Created;

        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public CommandState State => _commandState;

    public void Execute()
    {
        if (_commandState != CommandState.Created)
        {
            throw new InvalidOperationException("Invalid state");
        }

        _context.To.TopupDepositInitialAmount(_context.Amount);
        _commandState = CommandState.Executed;
    }

    public void Revert()
    {
        if (_commandState != CommandState.Executed)
        {
            throw new InvalidOperationException("Invalid state");
        }

        _context.To.RevertTopupDepositInitialAmount(_context.Amount);
        _commandState = CommandState.Reverted;
    }
}