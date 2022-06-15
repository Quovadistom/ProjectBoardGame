using System;
using UnityEngine;

public class AppManager : GenericSingleton<AppManager>
{
    public ResourcesSettings ResourcesSettings;
    public DebugSettings DebugSettings;
    public TileAssets TileAssets;

    public event Action AppInitializationDone;

    public override void Awake()
    {
        base.Awake();

        ScriptableObjectService.Instance.AddScriptableObject(ResourcesSettings);
        ScriptableObjectService.Instance.AddScriptableObject(DebugSettings);
        ScriptableObjectService.Instance.AddScriptableObject(TileAssets);

        AppInitializationDone?.Invoke();
    }

}
