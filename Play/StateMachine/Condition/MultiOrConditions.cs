using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MultiOrConditions : IMachineCondition
{
    private List<IMachineCondition> conditions;

    public MultiOrConditions(List<IMachineCondition> conditions)
    {
        this.conditions = conditions;
    }

    public bool IsSatisfy(StateParams param)
    {
        for (int i = conditions.Count - 1; i >= 0; i--)
        {
            if (conditions[i].IsSatisfy(param)) return true;
        }
        return false;
    }
}
