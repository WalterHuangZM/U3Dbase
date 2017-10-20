using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StringCondition : IMachineCondition
{
    private string key;
    private string str;

    public StringCondition(string key, string str)
    {
        this.key = key;
        this.str = str;
    }

    public bool IsSatisfy(StateParams param)
    {
        return str.Equals(param.GetParams<string>(key));
    }
}
