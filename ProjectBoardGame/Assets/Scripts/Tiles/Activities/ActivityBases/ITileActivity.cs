using System;
using UnityEngine;

public interface ITileActivity
{
    Vector3 PlayerSpawnLocation { get; set; }

    //Action TileActivityFailed { get; set; }
    //Action TileActivityDone { get; set; }

    void StartTileActivity();
}
