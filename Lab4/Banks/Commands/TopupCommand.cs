using Banks.Interfaces;
using Banks.Models;

namespace Banks.Commands;

public class TopupCommand : ICommand
{
    private CommandState _commandState;
    private TransactionContext _context;

    public TopupCommand(TransactionContext context)
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

        _context.To.TopupMoney(_context.Amount);
        _commandState = CommandState.Executed;
    }

    public void Revert()
    {
        if (_commandState != CommandState.Executed)
        {
            throw new InvalidOperationException("Invalid state");
        }

        _context.To.WithdrawMoney(_context.Amount);
        _commandState = CommandState.Reverted;
    }
}