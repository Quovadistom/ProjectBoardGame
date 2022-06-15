using System;
using System.Linq;

public static class ArrayExtensions
{
    /// <summary>
    /// Returns a random index, taking into account the weights of each item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int GetRandomIndexWeighted<T>(this WeightedItem<T>[] array)
    {
        int[] weights = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            int accumulatedWeight = i > 0 ? weights[i - 1] : 0;
            weights[i] = array[i].Weight + accumulatedWeight;
        }

        int randomIndex = UnityEngine.Random.Range(0, weights[weights.Length - 1]);

        return Array.IndexOf(weights, weights.FirstOrDefault(x => x > randomIndex));
    }

    public static T GetRandomWeightedItem<T>(this WeightedItem<T>[] array)
    {
        int index = GetRandomIndexWeighted(array);
        return array[index].Item;
    }
}
