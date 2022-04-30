using ProjectTM.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
    [Range(7, 15)] public int Width = 7;
    [Range(7, 15)] public int Height = 7;

    private GameObject m_nodeAsset;

    public List<List<Transform>> Grid = new List<List<Transform>>();

    public GameObject NodeAsset
    {
        get
        {
            if (m_nodeAsset == null)
                m_nodeAsset = Resources.Load("Grid/Node") as GameObject;

            return m_nodeAsset;
        }
    }

    public void CreateGrid()
    {
        Debug.Log("Creating grid...");

        foreach (Transform node in this.transform)
        {
            float[] xCoords = new float[Grid.Count];
            for (int i = 0; i < Grid.Count; i++)
            {
                xCoords[i] = Grid[i][0].localPosition.x;
            }

            float xCoordNode = node.localPosition.x;
            if (xCoords.Contains(xCoordNode))
                Grid[Array.IndexOf(xCoords, xCoordNode)].Add(node);
            else
            {
                List<Transform> newList = new List<Transform>();
                newList.Add(node);
                Grid.Add(newList);
            }
        }

        for (int i = 0; i < Grid.Count; i++)
            Grid[i] = Grid[i].OrderByDescending(x => x.localPosition.x).ToList();

        Grid = Grid.OrderBy(x => x[0].localPosition.z).ToList();

        HideNodes();
    }

    private void HideNodes()
    {
        DebugSettings debugSettings = ScriptableObjectService.Instance.GetScriptableObject<DebugSettings>();

        if (debugSettings.ShowDebugInfo) { return; }

        foreach (Transform node in this.transform)
            node.gameObject.SetActive(false);
    }


    public void CreateButtonClick()
    {
        RemoveNodes();

        float widthOffset = Width / 2;
        float heightOffset = Height / 2;

        for (int w = 0; w < Width; w++)
        {
            for (int h = 0; h < Height; h++)
            {
                //GameObject node = PrefabUtility.InstantiatePrefab(NodeAsset, this.transform) as GameObject;
                //node.transform.localPosition = new Vector3(widthOffset - w, 0, h - heightOffset);
            }
        }

    }

    public void RemoveNodes()
    {
        Component[] components = GetComponentsInChildren(typeof(Node), true);

        foreach (Node node in components)
            DestroyImmediate(node.gameObject);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridGenerator script = (GridGenerator)target;

        if (GUILayout.Button("Destroy current grid and create new grid"))
            script.CreateButtonClick();
        if (GUILayout.Button("Destroy current grid"))
            script.RemoveNodes();
    }
}
#endif
