using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum TileType
{
    FLAT,
    CLIFFTOP,
    CLIFFBOTTOM
}

public enum Orientation
{
    UNDEFINED,
    LEFT,
    RIGHT
}

public class Tile : MonoBehaviour
{
    public Action TileSelected;

    public int Scale = 1;
    public Orientation Orientation;
    public TileBiome TileBiome;
    public TileType TileType;

    public Tile[] NeighboringTiles { get; set; }
    public bool IsInitialized { get; private set; } = false;
    public TileActivity TileActivity { get; private set; } = null;

    private XRSocketInteractor m_xrSocketInteractor;

    public float DistanceToOtherTile(Vector2 closeTilePosition)
    {
        return Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.z), closeTilePosition);
    }

    public void InitializeTile(Orientation orientation)
    {
        Orientation = orientation;
        IsInitialized = true;
    }

    public void CreatePawnInteractor()
    {
        Vector3 positionOffset = new Vector3(0, 0.05f * Scale, 0);

        TileAssets m_tileAssets = ScriptableObjectService.Instance.GetScriptableObject<TileAssets>();
        m_xrSocketInteractor = GameObject.Instantiate(m_tileAssets.XRSocketInteractor, this.transform);
        m_xrSocketInteractor.transform.localPosition = positionOffset;
        m_xrSocketInteractor.transform.localScale = new Vector3(m_xrSocketInteractor.transform.localScale.x, m_xrSocketInteractor.transform.localScale.y / this.transform.localScale.y, m_xrSocketInteractor.transform.localScale.z);

    }

    public void CreateActivity()
    {
        TileActivity = ActivityService.Instance.GetRandomActivity(TileType, TileBiome);
    }

    private void OnDestroy()
    {
        //m_tileInteractor.SocketInteractor.selectEntered.RemoveListener(OnTileSelected);
    }
}
