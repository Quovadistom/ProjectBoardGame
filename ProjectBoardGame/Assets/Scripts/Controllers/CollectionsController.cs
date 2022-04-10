using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsController : GenericSingleton<CollectionsController>
{
    public GeneratedCollection GetGeneratedCollection()
    {
        return new GeneratedCollection();
    }
}
