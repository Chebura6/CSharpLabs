using Banks.Interfaces;

namespace Banks.Commands;

public class TransactionHandler
{
    private TransactionHandler _prev;
    private TransactionHandler _next;
    private ICommand _command;

    public TransactionHandler(ICommand command)
    {
        _prev = null;
        _next = null;

        ArgumentNullException.ThrowIfNull(command);

        _command = command;
    }

    public void Revert()
    {
        _next?.Revert();
        _command.Revert();
    }

    public bool Execute()
    {
        try
        {
            _command.Execute();
        }
        catch (Exception)
        {
            return false;
        }

        if (_next is null) return true;

        if (_next.Execute())
        {
            return true;
        }

        _command.Revert();

        return false;
    }

    internal void SetNext(TransactionHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        if (_next is not null || handler._prev is not null)
        {
            throw new InvalidOperationException("Handler already is set");
        }

        _next = handler;
        handler._prev = this;
    }
}