using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomGenerationSettings", menuName = "ScriptableObjects/RoomGenerationSettings", order = 1)]
public class RoomGenerationSettings : ScriptableObject
{
    public int MinRoomWidth;
    public int MinRoomHeight;

    public int MaxRoomWidth;
    public int MaxRoomHeight;
}
