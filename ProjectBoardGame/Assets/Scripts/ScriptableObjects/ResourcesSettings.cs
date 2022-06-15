using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesSettings", menuName = "ScriptableObjects/ResourcesSettings", order = 1)]

public class ResourcesSettings : ScriptableObject
{
    [Serializable]
    public struct Theme
    {
        public WeightedItem<TileInteractor>[] Plains;
        public WeightedItem<TileInteractor>[] Desert;
        public WeightedItem<TileInteractor>[] Water;
        public WeightedItem<TileInteractor>[] Forest;
    }

    public Theme GenericTheme;

    public Theme GetTheme()
    {
        return GenericTheme;
    }

    public WeightedItem<TileInteractor>[] GetCorrectObjectList(SpawnObject spawnObject, Theme theme)
    {
        return GetCorrectObjectList(spawnObject.Type, theme);
    }

    public WeightedItem<TileInteractor>[] GetCorrectObjectList(TileBiome type, Theme theme)
    {
        switch (type)
        {
            case TileBiome.PLAINS:
                return theme.Plains;
            case TileBiome.DESERT:
                return theme.Desert;
            case TileBiome.WATER:
                return theme.Water;
            case TileBiome.FOREST:
                return theme.Forest;
            default:
                return null;
        }
    }
}

public enum TileBiome
{
    PLAINS,
    DESERT,
    WATER,
    FOREST
}
