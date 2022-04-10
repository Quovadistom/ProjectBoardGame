using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceService : GenericSingleton<ResourceService>
{
    private ResourcesSettings m_resourcesSettings;
    private ResourcesSettings.DebugAssets m_theme;

    public ResourceService()
    {
        m_resourcesSettings = ScriptableObjectService.Instance.GetScriptableObject<ResourcesSettings>();
        m_theme = m_resourcesSettings.GetDebugAssets();
    }
}
