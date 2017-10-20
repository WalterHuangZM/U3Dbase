
using System;

public static class Singleton<T> where T : IDisposable
{
    private static T _instance;

    static Singleton()
    {
        return;
    }

    public static void Create()
    {
        _instance = (T)Activator.CreateInstance(typeof(T), true);
        return;
    }

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    public static void Destroy()
    {
        _instance.Dispose();
        _instance = default(T);
        return;
    }
}
