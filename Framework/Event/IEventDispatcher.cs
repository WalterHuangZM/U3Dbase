using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IEventDispatcher
{
    void RegistEvent(string type, EventCallBack listener);
    void UnregistEvent(string type, EventCallBack listener);
    void DispatcherEvent(string type, IEventParams eveParams = null);
    bool HasRegistEvent(string type);
}
