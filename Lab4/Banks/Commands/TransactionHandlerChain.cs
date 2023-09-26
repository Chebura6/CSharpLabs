namespace Banks.Commands;

public class TransactionHandlerChain
{
    private readonly TransactionHandler _root;

    public TransactionHandlerChain(params TransactionHandler[] handlers)
    {
        _root = handlers.First();
        for (int i = 0; i < handlers.Length - 1; i++)
        {
            handlers[i].SetNext(handlers[i + 1]);
        }
    }

    public bool Execute() => _root.Execute();

    public void Revert() => _root.Revert();
}