using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceService : GenericSingleton<ResourceService>
{
    private ResourcesSettings m_resourcesSettings;

    public ResourceService()
    {
        m_resourcesSettings = ScriptableObjectService.Instance.GetScriptableObject<ResourcesSettings>();
    }
}
