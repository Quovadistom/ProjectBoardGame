using System;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
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
    public int Scale = 1;
    public Orientation Orientation;
    public TileBiome TileBiome;
    public TileType TileType;

    public Tile[] NeighboringTiles;

    public float DistanceToOtherTile(Vector2 closeTilePosition)
    {
        return Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.z), closeTilePosition);
    }

    public void SetOrientation(Orientation orientation)
    {
        Orientation = orientation;

        int[] randomRotations = new int[3] { 0, 120, 240 };
        Vector3 rotationOffset = new Vector3(0, randomRotations[UnityEngine.Random.Range(0, randomRotations.Length)], 0);
        Vector3 positionOffset = new Vector3(0, 0.05f * Scale, 0);
        if (orientation == Orientation.LEFT)
            rotationOffset += new Vector3(0, 60, 0);

        ResourcesSettings resourceSettings = ScriptableObjectService.Instance.GetScriptableObject<ResourcesSettings>();
        WeightedItem<GameObject>[] list = resourceSettings.GetCorrectObjectList(TileBiome, resourceSettings.GetTheme());
        GameObject gameObject = GameObject.Instantiate(list[list.GetRandomIndexWeighted()].Item, this.transform, false);
        gameObject.transform.localPosition = positionOffset;
        gameObject.transform.localEulerAngles = rotationOffset;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y / this.transform.localScale.y, gameObject.transform.localScale.z);

        if (gameObject.TryGetComponent<LODGroup>(out LODGroup test))
        {
            test.ForceLOD(1);
        }
    }
}
