namespace Banks.Commands;

public enum CommandState
{
    Created = 0,
    Executed,
    Reverted,
}