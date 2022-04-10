using UnityEngine;

public class RoomObject : PositionObject
{
    public RoomObject(RoomObjectTypes type, bool inside, Vector3 position, Vector3 scale, Quaternion rotation) : base(position, scale, rotation)
    {
        RoomObjectType = type;
        Inside = inside;
    }

    public RoomObjectTypes RoomObjectType { get; set; }
    public bool Inside { get; set; }
    public GameObject Asset { get; set; }
}

public enum RoomObjectTypes
{
    PUZZLESLOT = 1,
    WALL = 2,
    DOOR = 3,
    CORNERTILE = 4,
    DOORTILE = 5,
    FLOORTILE = 6,
    GRASSTILE = 7,
    MIDDLETILE = 8,
    WALLTILE = 9,
    NONE = 10,
    EMPTY = 11
}

