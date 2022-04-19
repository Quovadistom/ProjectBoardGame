using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
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
        Tile[] tiles = GetComponentsInChildren<Tile>();
        foreach (Tile tile in tiles)
        {
            m_tiles.Add(tile);
        }

        Tile orientationTile = m_tiles.FirstOrDefault(tile => tile.Orientation != Orientation.UNDEFINED);
        SetOrientation(orientationTile);

        if (m_initialized)
            Debug.Log("Board succesfully Initialized.");
        else
            Debug.LogWarning("Something went wrong while initializing the board.");
    }

    private void SetOrientation(Tile orientationTile)
    {
        Vector2 orientationTilePosition = new Vector2(orientationTile.transform.localPosition.x, orientationTile.transform.localPosition.y);
        List<Tile> CloseTiles = new List<Tile>();
        CloseTiles = m_tiles.Where(tile => tile != orientationTile).OrderBy(tile => tile.HorizontalPosition(orientationTilePosition)).Take(3).ToList();
        CloseTiles = CloseTiles.Where(tile => MathExtensions.CloseTo(tile.HorizontalPosition(orientationTilePosition), CloseTiles[0].HorizontalPosition(orientationTilePosition))).ToList();

        foreach (Tile tile in CloseTiles)
        {
            if (tile.Orientation == Orientation.UNDEFINED)
            {
                if (orientationTile.Orientation == Orientation.LEFT)
                    tile.Orientation = Orientation.RIGHT;
                else if (orientationTile.Orientation == Orientation.RIGHT)
                    tile.Orientation = Orientation.LEFT;

                SetOrientation(tile);
            }
        }
    }
}
