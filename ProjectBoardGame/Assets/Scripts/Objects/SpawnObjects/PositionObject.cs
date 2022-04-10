using UnityEngine;

public class PositionObject
{
    public PositionObject(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }

    public Vector3 Position;
    public Vector3 Scale;
    public Quaternion Rotation;
}