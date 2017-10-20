using System;
using System.Collections.Generic;

public class StateParams
{
    private Dictionary<string, object> dic;

    //记录是否有值改变，减低执行频率
    private bool isDirty;

    public StateParams()
    {
        dic = new Dictionary<string, object>();
        isDirty = true;
    }

    public T GetParams<T>(string key)
    {
        if (!dic.ContainsKey(key)) return default(T);
        return (T)dic[key];
    }

    public void SetParams<T>(string key, T value)
    {
        if (!dic.ContainsKey(key)) return;
        dic[key] = value;
        isDirty = true;
    }

    public void AddParams<T>(string key, T initValue)
    {
        if (dic.ContainsKey(key))
        {
            dic[key] = initValue;
            return;
        }
        dic.Add(key, initValue);
    }

    public bool IsDirty { get { return isDirty; } }

    public void OnCheck() { isDirty = false; }

}
