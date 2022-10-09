using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActivityInfo
{
    public CollectibleObject CollectibleObject { get; set; }
    public int AmountOfNeededCollectibles { get; set; }

    public ActivityInfo(CollectibleObject randomCollectible, int amountOfRequiredItems)
    {
        CollectibleObject = randomCollectible;
        AmountOfNeededCollectibles = amountOfRequiredItems;
    }
}
