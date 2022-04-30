using System;
using UnityEngine;

[Serializable]
public class SpawnObject : PositionObject
{
    public SpawnObject(TileBiome type, Vector3 position, Vector3 scale, Quaternion rotation) : base(position, scale, rotation)
    {
        Type = type;
    }

    public TileBiome Type;
    public int Index;
}
