using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnPointMetaData
{
    public SpawnPointSize SpawnPointSize;
    public Transform Transform;
}

public class TileMetaData : MonoBehaviour
{
    public SpawnPointSize SpawnPointSize;
    public List<SpawnPointMetaData> SpawnLocations;
}
