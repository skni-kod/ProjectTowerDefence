using System;


public class ConsoleCommandBase
{
    public string CommandId { get; }
    public string CommandDescription { get; }
    public string CommandFormat { get; }

    public ConsoleCommandBase(string commandId, string commandDescription, string commandFormat)
    {
        CommandId = commandId;
        CommandDescription = commandDescription;
        CommandFormat = commandFormat;
    }
}

public class ConsoleCommand: ConsoleCommandBase
{
    private Action command;

    public ConsoleCommand(string commandId, string commandDescription, string commandFormat, Action command): base(commandId, commandDescription, commandFormat)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}

public class ConsoleCommand<T> : ConsoleCommandBase
{
    private Action<T> command;

    public ConsoleCommand(string commandId, string commandDescription, string commandFormat, Action<T> command) : base(commandId, commandDescription, commandFormat)
    {
        this.command = command;
    }

    public void Invoke(T value)
    {
        command.Invoke(value);
    }
}