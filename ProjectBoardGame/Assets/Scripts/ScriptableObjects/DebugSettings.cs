using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugSettings", menuName = "ScriptableObjects/DebugSettings", order = 1)]
public class DebugSettings : ScriptableObject
{
    public bool ShowDebugAssets = false;
    public bool ShowDebugInfo = false;
    public bool ShowGameAssets = true;
}
