using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectService : GenericSingleton<ScriptableObjectService>
{
    private List<ScriptableObject> m_scriptableObjects = new List<ScriptableObject>();

    public void AddScriptableObject(ScriptableObject scriptableObject) => m_scriptableObjects.Add(scriptableObject);

    public T GetScriptableObject<T>() where T : ScriptableObject
    {
        T scriptableObject = (T) m_scriptableObjects.Find(x => x.GetType() == typeof(T));
        return scriptableObject;
    }
}
