using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SimpleCommand : EventDispatcher, ICommand, IEventDispatcher
{
    public virtual void Cancel()
    {
    }
    
    public virtual void Execute(object commandParams = null)
    {
    }

    protected void CommandSuc()
    {
        this.DispatcherEvent(CommandEvent.CommandSucc);
    }

    protected void CommandFail()
    {
        this.DispatcherEvent(CommandEvent.CommandFail);
    }


}
