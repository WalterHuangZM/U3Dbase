using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SMState
{
    protected Dictionary<string, IMachineCondition> linkStates;
    protected StateParams param;

    public SMState()
    {
        name = "";
        linkStates = new Dictionary<string, IMachineCondition>();
    }

    public string name { get; set; }

    public virtual void SetParams(StateParams param)
    {
        this.param = param;
    }

    public Dictionary<string, IMachineCondition> links { get { return linkStates; } }

    public void AddLinkState(SMState state, IMachineCondition condition)
    {
        string stateName = state.name;
        if (linkStates.ContainsKey(stateName)) return;
        linkStates.Add(stateName, condition);
    }

    public void Dispose()
    {
        SubDispose();
        linkStates = null;
        param = null;
    }

    //------------------------------------

    public virtual void OnStateEnter()
    {

    }
    public virtual void StateUpdate()
    {

    }
    public virtual void OnStateExit()
    {

    }
    protected virtual void SubDispose()
    {

    }

}
