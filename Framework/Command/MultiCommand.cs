
using CommandList = System.Collections.Generic.List<ICommand>;

public class MultiCommand : SimpleCommand
{
    private CommandList commands = new CommandList();
    private int cur = -1;

    private object commandParams;

    public sealed override void Cancel()
    {
        if (cur >= 0 && cur < commands.Count)
        {
            ICommand command = commands[cur];
            command.UnregistEvent(CommandEvent.CommandSucc, OnCommandSuc);
            command.UnregistEvent(CommandEvent.CommandFail, OnCommandFial);
            command.Cancel();
            cur = -1;
        }
    }

    public sealed override void Execute(object commandParams = null)
    {
        this.commandParams = commandParams;
        SetUp();
        ExcuteNextCommand();
    }

    private void ExcuteNextCommand()
    {
        cur++;
        if (cur >= commands.Count)
        {
            CommandSuc();
            return;
        }
        ICommand command = commands[cur];
        command.RegistEvent(CommandEvent.CommandSucc, OnCommandSuc);
        command.RegistEvent(CommandEvent.CommandFail, OnCommandFial);
        command.Execute(commandParams);
    }

    private void OnCommandSuc(IEventParams ep)
    {
        ICommand command = commands[cur];
        command.UnregistEvent(CommandEvent.CommandSucc, OnCommandSuc);
        command.UnregistEvent(CommandEvent.CommandFail, OnCommandFial);
        ExcuteNextCommand();
    }

    private void OnCommandFial(IEventParams ep)
    {
        ICommand command = commands[cur];
        command.UnregistEvent(CommandEvent.CommandSucc, OnCommandSuc);
        command.UnregistEvent(CommandEvent.CommandFail, OnCommandFial);
        CommandFail();
    }

    protected virtual void SetUp()
    {

    }

    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }

    
}
