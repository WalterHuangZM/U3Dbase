using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CommandAction
{
    private ICommand command;

    public CommandAction(ICommand command)
    {
        this.command = command;
        this.command.RegistEvent(CommandEvent.CommandSucc, OnSucc);
        this.command.RegistEvent(CommandEvent.CommandFail, OnFail);
    }

    private Action suc = null;
    private Action fail = null;

    public void Start(Action suc = null, Action fail = null)
    {
        this.suc = suc;
        this.fail = fail;
        this.command.Execute();
    }

    private EventCallBack sucp = null;
    private EventCallBack failp = null;

    private void StartWithParams(EventCallBack sucp = null, EventCallBack failp = null)
    {
        this.sucp = sucp;
        this.failp = failp;
        this.command.Execute();
    }

    private void OnSucc(IEventParams ep)
    {
        if (suc != null) suc.Invoke();
        if (sucp != null) sucp.Invoke(ep);
        Dispose();
    }

    private void OnFail(IEventParams ep)
    {
        if (fail != null) fail.Invoke();
        if (failp != null) failp.Invoke(ep);
        Dispose();
    }

    public void Dispose()
    {
        this.command.UnregistEvent(CommandEvent.CommandSucc, OnSucc);
        this.command.UnregistEvent(CommandEvent.CommandFail, OnFail);
        this.command = null;
    }

}
