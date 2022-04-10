using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesSettings", menuName = "ScriptableObjects/ResourcesSettings", order = 1)]
public class ResourcesSettings : ScriptableObject
{
    [Serializable]
    public struct DebugAssets
    {

    }

    [Serializable]
    public struct Theme
    {
        public WeightedItem<GameObject>[] TestObjectList;
    }

    public DebugAssets DebugObjects;

    public DebugAssets GetDebugAssets() 
    { 
        return DebugObjects;
    }

    public Theme GenericTheme;

    public Theme GetTheme()
    {
        return GenericTheme;
    }

    
    public WeightedItem<GameObject>[] GetCorrectObjectList(SpawnObject spawnObject, Theme theme)
    {
        switch (spawnObject.Type)
        {
            case ObjectType.NONE:
                return null;
            default:
                return null;
        }
    }
}
