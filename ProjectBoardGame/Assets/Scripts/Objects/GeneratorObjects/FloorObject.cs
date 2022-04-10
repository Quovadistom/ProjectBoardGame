using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObject : RoomObject
{
    public List<PuzzleSlotObject> PuzzleSlotObjects = new List<PuzzleSlotObject>();

    public FloorObject(RoomObjectTypes type, bool inside, Vector3 position, Vector3 scale, Quaternion rotation) : base(type, inside, position, scale, rotation)
    {
    }

    public void SetOccupied(bool occupied = true)
    {
        foreach (PuzzleSlotObject puzzleSlot in PuzzleSlotObjects)
            puzzleSlot.IsOccupied = true;
    }
}
