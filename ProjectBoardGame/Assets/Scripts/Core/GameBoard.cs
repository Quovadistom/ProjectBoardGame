using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public event Action<bool> DisableInteractors;

    private List<Tile> m_tiles = new List<Tile>();

    private bool m_initialized
    {
        get
        {
            bool isOriented = !m_tiles.Any(tile => tile.Orientation == Orientation.UNDEFINED);
            return isOriented;
        }
    }

    public void Awake()
    {
        AppManager.Instance.AppInitializationDone += OnAppInitializatonDone;
    }

    public void OnDestroy()
    {
        AppManager.Instance.AppInitializationDone -= OnAppInitializatonDone;
    }

    public void DisableAllInteractors()
    {
        DisableInteractors?.Invoke(false);
    }

    private void OnAppInitializatonDone()
    {
        Tile[] tiles = GetComponentsInChildren<Tile>();
        foreach (Tile tile in tiles)
        {
            m_tiles.Add(tile);
        }

        Tile orientationTile = m_tiles.FirstOrDefault(tile => tile.Orientation != Orientation.UNDEFINED);
        orientationTile.SetOrientation(orientationTile.Orientation);
        SetOrientation(orientationTile);

        if (m_initialized)
            Debug.Log("Board succesfully Initialized.");
        else
            Debug.LogWarning("Something went wrong while initializing the board.");
    }

    private void SetOrientation(Tile orientationTile)
    {
        Vector2 orientationTilePosition = new Vector2(orientationTile.transform.localPosition.x, orientationTile.transform.localPosition.z);
        List<Tile> CloseTiles = new List<Tile>();
        CloseTiles = m_tiles.Where(tile => tile != orientationTile).OrderBy(tile => tile.DistanceToOtherTile(orientationTilePosition)).Take(3).ToList();
        orientationTile.NeighboringTiles = CloseTiles.Where(tile => MathExtensions.CloseTo(tile.DistanceToOtherTile(orientationTilePosition), CloseTiles[0].DistanceToOtherTile(orientationTilePosition))).ToArray();

        foreach (Tile tile in orientationTile.NeighboringTiles)
        {
            if (tile.Orientation == Orientation.UNDEFINED)
            {
                if (orientationTile.Orientation == Orientation.LEFT)
                    tile.SetOrientation(Orientation.RIGHT);
                else if (orientationTile.Orientation == Orientation.RIGHT)
                    tile.SetOrientation(Orientation.LEFT);

                SetOrientation(tile);
            }
        }
    }
}
