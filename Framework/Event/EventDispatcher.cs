
using EventList = System.Collections.Generic.List<EventCallBack>;
using EventListDic = System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<EventCallBack>>;

public class EventDispatcher : IEventDispatcher
{
    private EventListDic dic = new EventListDic();

    public void Dispose()
    {
        dic.Clear();
        dic = null;
    }

    public void RegistEvent(string type, EventCallBack listener)
    {
        EventList actions = null;
        if (!dic.TryGetValue(type, out actions))
        {
            actions = new EventList();
            dic.Add(type, actions);
        }
        int index = actions.IndexOf(listener);
        if (index != -1) return;
        actions.Add(listener);
    }

    public void UnregistEvent(string type, EventCallBack listener)
    {
        EventList actions = null;
        if (!dic.TryGetValue(type, out actions)) return;
        int index = actions.IndexOf(listener);
        if (index == -1) return;
        actions.RemoveAt(index);
        if (actions.Count <= 0) dic.Remove(type);
    }

    public void DispatcherEvent(string type, IEventParams eveParams = null)
    {
        EventList actions = null;
        if (!dic.TryGetValue(type, out actions)) return;
        int len = actions.Count;
        for (int i=0; i<actions.Count; i++)
        {
            actions[i].Invoke(eveParams);
        }
    }

    public bool HasRegistEvent(string type) { return dic.ContainsKey(type); }

}
