using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count - 1);
        return list[randomIndex];
    }

    public static int GetRandomIndex<T>(this List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        return randomIndex;
    }

    public static int GetRandomValueInList(this List<int> list, int value, int maxRange, int minRange = 0)
    {
        int minRangeLeft, minRangeRight;
        int maxRangeLeft, maxRangeRight;
        minRangeLeft = (value - minRange + 1) < 0 ? -1 : (value - minRange + 1);
        minRangeRight = (value + minRange - 1) > list[list.Count - 1] ? -1 : (value + minRange - 1);
        maxRangeLeft = (value - maxRange + 1) < 0 ? 0 : (value - maxRange + 1);
        maxRangeRight = (value + maxRange - 1) > list[list.Count - 1] ? list[list.Count - 1] : (value + maxRange - 1);

        while (list.Count >= 1)
        {
            int randomNumber = Random.Range(list[0], list[list.Count - 1]);
            if (minRangeLeft != -1 && (randomNumber <= minRangeLeft))
            {
                if (randomNumber >= maxRangeLeft)
                {
                    return randomNumber;
                }
            }

            if (minRangeRight != -1 && (randomNumber >= minRangeRight))
            {
                if (randomNumber <= maxRangeRight)
                {
                    return randomNumber;
                }
            }

            list.Remove(randomNumber);
        }

        Debug.LogError("Failed ");
        return 0;
    }
}
