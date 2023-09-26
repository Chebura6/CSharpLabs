using Banks.Interfaces;
using Banks.Models;

namespace Banks.Commands;

public class CommissionCommand : ICommand
{
    private CommandState _commandState;
    private TransactionContext _context;

    public CommissionCommand(TransactionContext context)
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

        _context.From.RemoveCommission(_context.Amount);
        _commandState = CommandState.Executed;
    }

    public void Revert()
    {
        if (_commandState != CommandState.Executed)
        {
            throw new InvalidOperationException("Invalid state");
        }

        _context.From.ReturnCommission(_context.Amount);
        _commandState = CommandState.Reverted;
    }
}