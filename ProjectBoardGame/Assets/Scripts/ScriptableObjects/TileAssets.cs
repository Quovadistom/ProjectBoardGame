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

    public TileObjects[] TileObjects;

    public GameObject GetTileObjectFromArray(TileObject tileObject)
    {
        TileObjects list = TileObjects.FirstOrDefault(x => x.TileObjectType == tileObject);
        return list.TileObjectList.GetRandomWeightedItem();
    }
}

[Flags]
public enum TileObjectOptions
{
    NONE = 0,
    HOUSE = 1,
    CLIFFBOTTOM = 2,
    CLIFFTOP = 4,
    NPC = 8,
    COLLECTIBLE = 16
}

public enum TileObject
{
    NONE = 0,
    HOUSE = 1,
    CLIFFBOTTOM = 2,
    CLIFFTOP = 4,
    NPC = 8,
    COLLECTIBLE = 16
}

[Serializable]
public class TileObjects
{
    public TileObject TileObjectType;

    public WeightedItem<GameObject>[] TileObjectList;
}