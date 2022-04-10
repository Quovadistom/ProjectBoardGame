using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Node : MonoBehaviour
{
    public Action OnInitDone;
    public TMP_Text RoomNumber;

    private List<Vector3> m_cornerCoordinates = new List<Vector3>();

    public int LocalRoomNumber { get; set; } = 0;

    public void Init()
    {
        RoomNumber.text = LocalRoomNumber.ToString();

        OnInitDone?.Invoke();
    }
}