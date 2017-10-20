using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MultiThreadTask : ThreadTask
{
    private SubThreadTask[] tasks;
    private int cur;

    public MultiThreadTask(SubThreadTask[] tasks)
    {
        this.tasks = tasks;
        cur = -1;
        RunNextTask();
    }

    protected override void SubExcute()
    {
        tasks[cur].Excute();
        if (!tasks[cur].isRunning) RunNextTask();
    }

    public ThreadTask[] GetTasks()
    {
        return tasks;
    }

    private void RunNextTask()
    {
        cur++;
        if (cur >= tasks.Length)
        {
            isRunning = false;
            return;
        }
        tasks[cur].BeforExcute();
    }

}
