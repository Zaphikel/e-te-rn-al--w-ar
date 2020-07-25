using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Prototype/Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public static ItemDatabaseObject itemDatabaseObject;
    public Item[] Items;
    public Color32[] rarityColorValues;
    public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();


    public void OnAfterDeserialize()
    {
        itemDatabaseObject = this;
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, Item>();
    }
}
