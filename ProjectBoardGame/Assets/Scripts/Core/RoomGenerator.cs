using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using ProjectTM.Managers;

public enum OrientationGuideline
{
    X,
    Y
}

public class RoomGenerator : MonoBehaviour
{
    public Vector2Int MinRoomDimensions;
    public Vector2Int MaxRoomDimensions;

    public event Action OnNodeAllocationDone;
    public event Action OnNodeGeneratingDone;

    private int m_width, m_height;
    private int m_amountOfNodes = 0;
    private int m_amountOfNodesInitialized = 0;
    private GeneratedCollection m_generatedCollection;
    private GridGenerator m_gridGenerator;

    private void Awake()
    {
        m_generatedCollection = CollectionsController.Instance.GetGeneratedCollection();
        NetworkManager.Instance.RoomCreatedAndReady += OnRoomCreatedAndReady;
    }

    private void OnRoomCreatedAndReady()
    {
        m_gridGenerator = GetComponent<GridGenerator>();
        m_gridGenerator.CreateGrid();

        m_generatedCollection.RotationOffset = m_gridGenerator.transform.eulerAngles.y;
        OnNodeGeneratingDone += m_generatedCollection.Init;

        m_width = m_gridGenerator.Grid.Count;
        m_height = m_gridGenerator.Grid[0].Count;

        CreateRooms();
    }

    private void OnDestroy()
    {
        OnNodeGeneratingDone -= m_generatedCollection.Init;
    }

    private void CreateRooms()
    {
        List<int> availableX = Enumerable.Range(0, m_width - MinRoomDimensions.x).ToList();
        List<int> availableY = Enumerable.Range(0, m_height - MinRoomDimensions.y).ToList();

        Vector2[] corners = CreateRoom(availableX, availableY);
        int[] xExtremes = new int[] { (int)corners.Min(x => x.x), (int)corners.Max(x => x.x) };
        int[] yExtremes = new int[] { (int)corners.Min(x => x.y), (int)corners.Max(x => x.y) };

        CalculateBoundaries(xExtremes, yExtremes, 1);

        OrientationGuideline orientationGuideline = OrientationGuideline.X;
        (availableX, availableY, orientationGuideline) = CalculateBiggestEmptySpace(corners);
        corners = CreateSecondRoom(availableX, availableY, orientationGuideline);

        xExtremes = new int[] { (int)corners.Min(x => x.x), (int)corners.Max(x => x.x) };
        yExtremes = new int[] { (int)corners.Min(x => x.y), (int)corners.Max(x => x.y) };
        CalculateBoundaries(xExtremes, yExtremes, 2);

        OnNodeAllocationDone?.Invoke();
    }

    private void CalculateBoundaries(int[] xExtremes, int[] yExtremes, int localRoomNumber)
    {
        IEnumerable<int> rangeY = Enumerable.Range(yExtremes[0], yExtremes[1] - yExtremes[0]);
        IEnumerable<int> rangeYInside = Enumerable.Range(yExtremes[0] + 1, yExtremes[1] - yExtremes[0] - 1); // Error with both 10 10
        IEnumerable<int> rangeX = Enumerable.Range(xExtremes[0], xExtremes[1] - xExtremes[0]);
        IEnumerable<int> rangeXInside = Enumerable.Range(xExtremes[0] + 1, xExtremes[1] - xExtremes[0] - 1);

        for (int i = 0; i < m_gridGenerator.Grid.Count; i++)
        {
            for (int j = 0; j < m_gridGenerator.Grid[0].Count; j++)
            {
                Node node = m_gridGenerator.Grid[i][j].GetComponent<Node>();

                if (node.GeneratedRoom == null)
                    node.GeneratedRoom = m_generatedCollection;

                if (node.TileObjectType == RoomObjectTypes.CORNERTILE || node.TileObjectType == RoomObjectTypes.WALLTILE || node.TileObjectType == RoomObjectTypes.MIDDLETILE) { continue; }

                if (xExtremes.Contains(i) && yExtremes.Contains(j))
                {
                    node.TileObjectType = RoomObjectTypes.CORNERTILE;
                    node.LocalRoomNumber = localRoomNumber;
                }
                else if ((xExtremes.Contains(i) && rangeY.Contains(j)) || (yExtremes.Contains(j) && rangeX.Contains(i)))
                {
                    node.TileObjectType = RoomObjectTypes.WALLTILE;
                    node.LocalRoomNumber = localRoomNumber;
                }
                else if (rangeXInside.Contains(i) && rangeYInside.Contains(j))
                {
                    node.TileObjectType = RoomObjectTypes.MIDDLETILE;
                    node.LocalRoomNumber = localRoomNumber;
                }
                else
                {
                    node.TileObjectType = RoomObjectTypes.EMPTY;
                    node.LocalRoomNumber = 0;
                }

                m_amountOfNodes++;
                node.OnInitDone += InitializeNodeDone;
                OnNodeAllocationDone += node.Init;
            }
        }
    }

    private (List<int>, List<int>, OrientationGuideline orientationGuideline) CalculateBiggestEmptySpace(Vector2[] corners)
    {
        List<List<int>> allLists = new List<List<int>>();

        float maxXDistance = m_width - 1 - corners.Max(x => x.x);
        allLists.Add(Enumerable.Range((int)corners.Max(x => x.x) == m_width ? m_width : (int)corners.Max(x => x.x) + 1, (int)maxXDistance).ToList());
        float maxYDistance = m_height - 1 - corners.Max(x => x.y);
        allLists.Add(Enumerable.Range((int)corners.Max(x => x.y) == m_height ? m_height : (int)corners.Max(x => x.y) + 1, (int)maxYDistance).ToList());
        float minXDistance = corners.Min(x => x.x);
        allLists.Add(Enumerable.Range(0, (int)minXDistance == 0 ? 0 : (int)minXDistance).ToList());
        float minYDistance = corners.Min(x => x.y);
        allLists.Add(Enumerable.Range(0, (int)minYDistance == 0 ? 0 : (int)minYDistance).ToList());

        List<int> suitableListNumbers = new List<int>();
        int longestList = allLists.Max(x => x.Count);

        for (int i = 0; i < allLists.Count; i++)
        {
            if (allLists[i].Count == longestList)
                suitableListNumbers.Add(i);
        }

        int listNumber = suitableListNumbers[Random.Range(0, suitableListNumbers.Count)];

        List<int> finalList = allLists[listNumber];
        List<int> yList = Enumerable.Range((int)corners.Min(x => x.y), (int)(corners.Max(x => x.y) + 1 - corners.Min(x => x.y))).ToList();
        List<int> xList = Enumerable.Range((int)corners.Min(x => x.x), (int)(corners.Max(x => x.x) + 1 - corners.Min(x => x.x))).ToList();
        List<int> boundaryList = new List<int>();

        switch (listNumber)
        {
            case 0:
                return (finalList, yList, OrientationGuideline.X);
            case 1:
                return (xList, finalList, OrientationGuideline.Y);
            case 2:
                return (finalList, yList, OrientationGuideline.X);
            case 3:
                return (xList, finalList, OrientationGuideline.Y);
            default:
                return (null, null, OrientationGuideline.X);
        }
    }

    private void InitializeNodeDone()
    {
        m_amountOfNodesInitialized++;

        if (m_amountOfNodesInitialized == m_amountOfNodes)
            OnNodeGeneratingDone.Invoke();
    }

    private Vector2[] CreateRoom(List<int> availableWidth, List<int> availableHeight)
    {
        Vector2[] coordinates = new Vector2[4];

        int x = availableWidth.GetRandomItem();
        int y = availableHeight.GetRandomItem();
        coordinates[0] = new Vector2(x, y);

        x = Random.Range(x + MinRoomDimensions.x - 1, GetMin(m_width, x + MaxRoomDimensions.x));
        coordinates[1] = new Vector2(x, y);
        y = Random.Range(y + MinRoomDimensions.y - 1, GetMin(m_height, y + MaxRoomDimensions.y));
        coordinates[2] = new Vector2(x, y);

        coordinates[3] = new Vector2(coordinates[0].x, coordinates[2].y);

        return coordinates;
    }

    private Vector2[] CreateSecondRoom(List<int> availableX, List<int> availableY, OrientationGuideline orientationGuideline)
    {
        Vector2[] coordinates = new Vector2[4];

        int x1 = 0;
        int y1 = 0;
        int x2 = 0;
        int y2 = 0;

        if (orientationGuideline == OrientationGuideline.X)
        {
            List<int> yRange = Enumerable.Range(0, m_height - 1).ToList();

            x1 = availableX.Min() == 0 ? availableX.Max() : availableX.Min();
            availableX.Remove(x1);
            y1 = Random.Range(availableY[1], availableY[availableY.Count - 2]);
            yRange.Remove(y1);

            x2 = availableX.GetRandomValueInList(x1, MaxRoomDimensions.x, MinRoomDimensions.x);
            y2 = yRange.GetRandomValueInList(y1, MaxRoomDimensions.y, MinRoomDimensions.y);
        }
        else
        {
            List<int> xRange = Enumerable.Range(0, m_width - 1).ToList();

            x1 = Random.Range(availableX[1], availableX[availableX.Count - 2]);
            xRange.Remove(x1);
            y1 = availableY.Min() == 0 ? availableY.Max() : availableY.Min();
            availableY.Remove(y1);

            x2 = xRange.GetRandomValueInList(x1, MaxRoomDimensions.x, MinRoomDimensions.x);
            y2 = availableY.GetRandomValueInList(y1, MaxRoomDimensions.y, MinRoomDimensions.y);
        }

        coordinates[0] = new Vector2(x1, y1);
        coordinates[1] = new Vector2(x2, y1);
        coordinates[2] = new Vector2(x2, y2);
        coordinates[3] = new Vector2(x1, y2);

        return coordinates;
    }

    private int GetMin(int a, int b)
    {
        int[] values = new int[] { a, b };
        return values.Min();
    }
}
