using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TyleBiome
{
    PLAINS,
    DESERT,
    WATER,
    FOREST
}

public enum TyleType
{
    FLAT,
    CLIFFS
}

public enum Orientation
{
    UNDEFINED,
    LEFT,
    RIGHT
}

public class Tile : MonoBehaviour
{
    public Orientation Orientation;
    public TyleBiome TyleBiome;
    public TyleType TyleType;

    public float HorizontalPosition(Vector2 closeTilePosition)
    {
        return Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), closeTilePosition);
    }
}
