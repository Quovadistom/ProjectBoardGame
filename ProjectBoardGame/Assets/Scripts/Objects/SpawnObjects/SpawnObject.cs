using System;
using UnityEngine;

[Serializable]
public class SpawnObject : PositionObject
{
    public SpawnObject(ObjectType type, Vector3 position, Vector3 scale, Quaternion rotation) : base(position, scale, rotation)
    {
        Type = type;
    }

    public ObjectType Type;
    public int Index;
}

public enum ObjectType
{
    WALLOUTSIDE,
    WALLINSIDE,
    DOOROUTSIDE,
    DOORINSIDE,
    FLOOROUTSIDE,
    FLOORINSIDE,
    CORNER,
    NONE
}
