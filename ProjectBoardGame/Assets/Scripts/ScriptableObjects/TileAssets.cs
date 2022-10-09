using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CreateAssetMenu(fileName = "TileAssets", menuName = "ScriptableObjects/TileAssets", order = 3)]

public class TileAssets : ScriptableObject
{
    public XRSocketInteractor XRSocketInteractor;

    public TileMetaData BaseTile;
    public TileMetaData SingleSpawn;
    public TileMetaData LowDensitySpawn;
    public TileMetaData HighDensitySpawn;

    public StaticTileObjects[] StaticTileObjects;

    public bool TryGetStaticObjectList(string tileOption, out WeightedItem<TileMetaData>[] weightedItems)
    {
        AllSpawnOptions option = (AllSpawnOptions) Enum.Parse(typeof(AllSpawnOptions), tileOption);
        weightedItems = StaticTileObjects.FirstOrDefault(type => type.TileObjectType == option).TileObjectList;
        return weightedItems != null;
    }

    internal int TryGetRandomStaticObject(string tileOption, out TileMetaData tileMetaData)
    {
        int index = -1;
        tileMetaData = null;

        AllSpawnOptions option = (AllSpawnOptions)Enum.Parse(typeof(AllSpawnOptions), tileOption);
        WeightedItem<TileMetaData>[] list = StaticTileObjects.FirstOrDefault(type => type.TileObjectType == option).TileObjectList;

        index = list.GetRandomIndexWeighted();
        if (index >= 0)
            tileMetaData = list[index].Item;

        return index;
    }
}

public enum AllSpawnOptions
{
    CLIFFBOTTOM,
    CLIFFTOP,
    FLAT,
    HOUSE,
    NPC,
    NAILS,
    FRUIT
}

[Serializable]
public class StaticTileObjects
{
    public AllSpawnOptions TileObjectType;

    public WeightedItem<TileMetaData>[] TileObjectList;
}