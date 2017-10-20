using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CompareableCondition<T> : IMachineCondition where T : IComparable
{
    private string key;
    private T value;
    private int compareType;

    public CompareableCondition(string key, T value, MachineCompareType compareType)
    {
        this.key = key;
        this.value = value;
        this.compareType = (int)compareType;
    }

    public bool IsSatisfy(StateParams param)
    {
        T value = param.GetParams<T>(key);
        int result = this.value.CompareTo(value);
        int type = (int)GetTypeByCompareResule(result);
        return (type & compareType) != 0;
    }

    public MachineCompareType GetTypeByCompareResule(int result)
    {
        if (result == -1) return MachineCompareType.Small;
        if (result == 1) return MachineCompareType.Big;
        return MachineCompareType.Equal;
    }
    
}
