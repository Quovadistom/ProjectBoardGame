using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppManager : GenericSingleton<AppManager>
{
    public List<ScriptableObject> ScriptableObjects;

    /// <summary>
    /// The action that fires when the initialization is done. No unsubscribing needed!
    /// </summary>
    public event Action AppInitializationDone;

    public override void Awake()
    {
        base.Awake();

        foreach (ScriptableObject scriptableObject in ScriptableObjects)
        {
            ScriptableObjectService.Instance.AddScriptableObject(scriptableObject);
        }

        AppInitializationDone?.Invoke();
    }

    public void OnDestroy()
    {
        AppInitializationDone = null;
    }
}
