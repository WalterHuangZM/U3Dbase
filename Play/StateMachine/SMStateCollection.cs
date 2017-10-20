using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SMStateCollection : SMState
{
    //表示正在执行的任何状态
    public static string AnyStateKey = "AnyState";

    private Dictionary<string, SMState> stateCollection;
    private string defaultState;
    private SMState curState;

    public SMStateCollection()
    {
        this.stateCollection = new Dictionary<string, SMState>();
        defaultState = "";
    }

    public void AddState(SMState state)
    {
        string sn = state.name;
        
        if (stateCollection.ContainsKey(sn)) return;
        stateCollection.Add(sn, state);
        state.SetParams(param);
    }

    public void AddLinkAnyState(IMachineCondition condition, SMState state)
    {
        if(!stateCollection.ContainsKey(AnyStateKey))
        {
            SMState anyState = new SMState();
            anyState.name = AnyStateKey;
            AddState(anyState);
        }
        stateCollection[AnyStateKey].AddLinkState(state, condition);
    }

    public void SetDefauleState(SMState state)
    {
        defaultState = state.name;
    }

    public override void SetParams(StateParams param)
    {
        this.param = param;
        foreach (var state in stateCollection)
        {
            state.Value.SetParams(param);
        }
    }

    public override void OnStateEnter()
    {
        string defaultStateName = string.IsNullOrEmpty(defaultState) ? stateCollection[stateCollection.Keys.First()].name : defaultState;
        
        curState = stateCollection[defaultStateName];
        curState.OnStateEnter();
    }

    public override void StateUpdate()
    {
        curState.StateUpdate();

        SMState nextState = GetNextState();
        if (nextState == null)
        {
            param.OnCheck();
        }
        else
        {
            curState.OnStateExit();
            curState = nextState;
            curState.OnStateEnter();
        }
    }

    private SMState GetNextState()
    {
        if (!param.IsDirty) return null;
        
        SMState state = GetSatisfyState(curState.links);
        if (state != null) return state;

        if (!stateCollection.ContainsKey(AnyStateKey)) return null;
        state = GetSatisfyState(stateCollection[AnyStateKey].links);
        if (state == curState) return null;
        return state;
    }

    private SMState GetSatisfyState(Dictionary<string, IMachineCondition> linkDatas)
    {
        foreach(var value in linkDatas)
        {
            if (value.Value.IsSatisfy(param)) return stateCollection[value.Key];
        }
        return null;
    }

    public override void OnStateExit()
    {
        curState.OnStateExit();
    }

    protected override void SubDispose()
    {
        foreach (var state in stateCollection)
        {
            state.Value.Dispose();
        }
    }
}
