using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class ThreadManager : MonoBehaviour, IDisposable
{
    private const int ThreadNum = 4;
    private List<ThreadInfo> waitingList = new List<ThreadInfo>();
    private List<ThreadInfo> runningList = new List<ThreadInfo>();

    public void AddThread(ThreadTask task, Action callBack=null)
    {
        ThreadInfo info = new ThreadInfo();
        info.task = task;
        info.callBack = callBack;

        int count = runningList.Count;
        if (count < ThreadNum)
        {
            runningList.Add(info);
            info.ThreadStart();
            if (count == 0) this.enabled = true;
        }
        else
        {
            waitingList.Add(info);
        }
    }

    public void RemoveThread(ThreadTask task, Action callBack = null)
    {
        for (int i=waitingList.Count-1; i>=0; i--)
        {
            if(waitingList[i].task == task)
            {
                waitingList.RemoveAt(i);
                return;
            }
        }
        for (int i = runningList.Count - 1; i >= 0; i--)
        {
            if (runningList[i].task == task)
            {
                ThreadInfo info = runningList[i];
                runningList.RemoveAt(i);
                info.task.isRunning = false;
                if (waitingList.Count > 0)
                {
                    ThreadInfo nextInfo = waitingList[0];
                    waitingList.RemoveAt(0);
                    runningList.Add(nextInfo);
                    nextInfo.ThreadStart();
                }
                return;
            }
        }
    }


    void Update()
    {
        int runningLen = runningList.Count;
        if(runningLen == 0)
        {
            this.enabled = false;
            return;
        }
        for (int i=runningLen - 1; i>=0; i--)
        {
            ThreadInfo info = runningList[i];
            if (info.task.isRunning) continue;

            runningList.RemoveAt(i);
            if (waitingList.Count > 0)
            {
                ThreadInfo nextInfo = waitingList[0];
                waitingList.RemoveAt(0);
                runningList.Add(nextInfo);
                nextInfo.ThreadStart();
            }
            if (info.callBack != null) info.callBack.Invoke();
        }
    }
    
    public void Dispose()
    {
        for (int i = runningList.Count - 1; i >= 0; i--)
        {
            runningList[i].task.isRunning = false;
        }
        runningList = null;
        waitingList = null;
    }

    class ThreadInfo
    {
        public Action callBack;
        public ThreadTask task;

        public void ThreadStart()
        {
            Thread thread = new Thread(new ThreadStart(task.Excute));
            thread.Start();
        }
    }

}
