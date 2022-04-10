using UnityEngine;

[System.Serializable]
public class WeightedItem<T>
{
    [Tooltip("The item")]
    public T Item;
    [Tooltip("The chance of spawning object (value between 1 and 100)")]
    [Range(1, 100)] public int Weight = 100;
}
