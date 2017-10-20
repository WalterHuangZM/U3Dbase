using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RandomUtil
{
    private static Random random = new Random();

    public static int Next(int maxValue)
    {
        return random.Next(maxValue);
    }

    public static double NextDouble()
    {
        return random.NextDouble();
    }

}
