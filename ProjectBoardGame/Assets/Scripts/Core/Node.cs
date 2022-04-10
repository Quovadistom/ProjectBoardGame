using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Node : MonoBehaviour
{
    public Action OnInitDone;
    public TMP_Text RoomNumber;

    private FloorObject m_floorObject = null;
    private GeneratedCollection m_generatedCollection;
    private List<Vector3> m_cornerCoordinates = new List<Vector3>();

    public int LocalRoomNumber { get; set; } = 0;
    public RoomObjectTypes TileObjectType { get; set; } = RoomObjectTypes.NONE;

    public GeneratedCollection GeneratedRoom
    {
        get { return m_generatedCollection; }
        set
        {
            m_generatedCollection = value;
            m_generatedCollection.Nodes.Add(this.transform.localPosition, this);
        }
    }

    public void Init()
    {
        AddFloor();

        if (TileObjectType == RoomObjectTypes.WALLTILE || TileObjectType == RoomObjectTypes.CORNERTILE)
        {
            Vector3 localPosition = this.transform.localPosition;
            PrepareWall(new Vector3(localPosition.x, localPosition.y, localPosition.z - 1), 0);
            PrepareWall(new Vector3(localPosition.x + 1, localPosition.y, localPosition.z), -90);
            PrepareWall(new Vector3(localPosition.x - 1, localPosition.y, localPosition.z), 90);
            PrepareWall(new Vector3(localPosition.x, localPosition.y, localPosition.z + 1), 180);
        }

        RoomNumber.text = LocalRoomNumber.ToString();

        OnInitDone?.Invoke();
    }

    private void PrepareWall(Vector3 neighbourPosition, int rotation)
    {
        bool inside = false;

        if (m_generatedCollection.Nodes.TryGetValue(neighbourPosition, out Node node))
        {
            RoomObjectTypes nodeFloorType = node.TileObjectType;
            if (nodeFloorType == RoomObjectTypes.CORNERTILE || nodeFloorType == RoomObjectTypes.WALLTILE || nodeFloorType == RoomObjectTypes.MIDDLETILE)
            {
                if (node.LocalRoomNumber == LocalRoomNumber)
                    return;

                inside = true;
            }
        }

        Vector3 positionToSpawn = (neighbourPosition - this.transform.localPosition) / 2;

        if (TileObjectType == RoomObjectTypes.CORNERTILE)
        {
            PrepareCorner(positionToSpawn);
        }

        AddWall(positionToSpawn, rotation, inside);
    }

    private void PrepareCorner(Vector3 neighbourPosition)
    {
        m_cornerCoordinates.Add(neighbourPosition);

        if (m_cornerCoordinates.Count == 2)
        {
            float xPosition = m_cornerCoordinates.Select(coordinate => coordinate.x).FirstOrDefault(x => x != 0);
            float zPosition = m_cornerCoordinates.Select(coordinate => coordinate.z).FirstOrDefault(z => z != 0);

            Vector3 cornerPosition = new Vector3(xPosition, transform.localPosition.y, zPosition);
            Vector3 position = transform.TransformPoint(cornerPosition);
            SpawnObject corner = new SpawnObject(ObjectType.CORNER, position, Vector3.one, Quaternion.identity);
            m_generatedCollection.AddCorner(corner);
        }
    }

    private void AddWall(Vector3 spawnPosition, int rotation, bool inside)
    {
        Vector3 position = transform.TransformPoint(spawnPosition);
        Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
        WallObject wallObject = new WallObject(RoomObjectTypes.WALL, inside, position, this.transform.localScale, newRotation)
        {
            RoomNumber = LocalRoomNumber
        };

        var createdWall = m_generatedCollection.AddWall(wallObject);
        if (m_floorObject != null)
            createdWall.ConnectingFloorTiles.Add(m_floorObject);
    }

    private void AddFloor()
    {
        m_floorObject = new FloorObject(TileObjectType, LocalRoomNumber == 0, this.transform.position, this.transform.localScale, this.transform.rotation);

        m_generatedCollection.AddFloorTile(m_floorObject);
    }
}