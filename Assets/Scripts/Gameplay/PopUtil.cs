using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PopUtil
{
    private const int BASE_POP_CAP = 20;
    private static List<int> popCapIncrease = new List<int> { 5, 10, 15, 20, 25, 40, 50 };
    private static List<int> popFoodRequirements = new List<int> { 0, 1, 2, 3, 5, 6, 8, 10, 12, 14, 17, 20, 23, 26, 30, 34, 39, 44, 50, 56, 63, 70, 78, 87, 97, 108, 120 };

    public static int GetPopCap(int popCount)
    {
        var cap = BASE_POP_CAP;
        var x = popCount / 10;
        var mod = popCount % 10;
        for (int i = 0; i < x; i++)
            cap += popCapIncrease[i] * 10;
        cap += popCapIncrease[x] * mod;

        return cap;
    }

    public static int GetFoodRequirements(int popCount)
    {
        return popFoodRequirements[popCount / 5];
    }
}
