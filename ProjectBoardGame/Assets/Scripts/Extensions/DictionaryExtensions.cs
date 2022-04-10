using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExtensions
{
    /// <summary>
    /// Adds an item to a dictionary, using a vector3 position as key. Returns false when key is already occupied.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="dictionaryObject"></param>
    /// <returns></returns>
    public static bool AddDictionaryItemWithPositionKey<T>(this Dictionary<Vector3, T> dictionary, T dictionaryObject) where T : PositionObject
    {
        if (dictionary.ContainsKey(dictionaryObject.Position)) { return false; }

        dictionary.Add(dictionaryObject.Position, dictionaryObject);
        return true;
    }
}
