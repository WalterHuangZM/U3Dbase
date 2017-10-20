
using System;
using UnityEngine;

public class ThreadTask
{
    private bool _isRunning = true;
    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isErro = false;

    public void Excute()
    {
        try
        {
            _isRunning = true;
            while (_isRunning)
            {
                SubExcute();
            }
        }
        catch (Exception e)
        {
            _isRunning = false;
            isErro = true;
            Debug.LogError(e.Message);
        }
    }

    protected virtual void SubExcute()
    {
        isRunning = false;
    }

}
