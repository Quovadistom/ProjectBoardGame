using UnityEngine;

[CreateAssetMenu(fileName = "Activities", menuName = "ScriptableObjects/Activities", order = 2)]

public class Activities : ScriptableObject
{
    public WeightedItem<TileActivity>[] ActivityList;
}