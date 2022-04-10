using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlotObject : RoomObject
{
    public bool IsOccupied { get; set; } = false;

    public PuzzleSlotObject(RoomObjectTypes type, bool inside, Vector3 position, Vector3 scale, Quaternion rotation) : base(type, inside, position, scale, rotation)
    {
    }
}
