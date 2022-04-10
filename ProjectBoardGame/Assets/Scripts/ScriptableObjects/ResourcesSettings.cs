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
        [Header("Walls")]
        public GameObject OutsideWall;
        public GameObject InsideWall;

        [Header("Doors")]
        public GameObject OutsideDoor;
        public GameObject InsideDoor;

        [Header("Floors")]
        public GameObject CornerTile;
        public GameObject DoorTile;
        public GameObject GrassTile;
        public GameObject MiddleTile;
        public GameObject WallTile;

        [Header("PuzzleSlot")]
        public GameObject PuzzleSlot;
    }

    [Serializable]
    public struct Theme
    {
        [Header("Walls")]
        public WeightedItem<GameObject>[] OutsideWalls;
        public WeightedItem<GameObject>[] InsideWalls;

        [Header("Doors")]
        public WeightedItem<GameObject>[] OutsideDoors;
        public WeightedItem<GameObject>[] InsideDoors;

        [Header("Floors")]
        public WeightedItem<GameObject>[] OutsideTiles;
        public WeightedItem<GameObject>[] InsideTiles;

        public WeightedItem<GameObject>[] Corners;
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
            case ObjectType.WALLOUTSIDE:
                return theme.OutsideWalls;
            case ObjectType.WALLINSIDE:
                return theme.InsideWalls;
            case ObjectType.DOOROUTSIDE:
                return theme.OutsideDoors;
            case ObjectType.DOORINSIDE:
                return theme.InsideDoors;
            case ObjectType.FLOOROUTSIDE:
                return theme.OutsideTiles;
            case ObjectType.FLOORINSIDE:
                return theme.InsideTiles;
            case ObjectType.CORNER:
                return theme.Corners;
            default:
                return null;
        }
    }
}
