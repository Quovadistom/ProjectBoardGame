using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : RoomObject
{
    public List<FloorObject> ConnectingFloorTiles = new List<FloorObject>();
    public int RoomNumber { get; set; }

    public WallObject(RoomObjectTypes type, bool inside, Vector3 position, Vector3 scale, Quaternion rotation) : base(type, inside, position, scale, rotation)
    {
    }
}
