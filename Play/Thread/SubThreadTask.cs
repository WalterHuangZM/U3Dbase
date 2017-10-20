using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SubThreadTask : ThreadTask
{
    public virtual void BeforExcute()
    {
        throw new NotImplementedException();
    } 
}
