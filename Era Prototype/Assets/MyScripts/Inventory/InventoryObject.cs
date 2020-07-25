using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "Inventory", menuName = "Prototype/Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    //public int MAX_ITEMS;
    public Inventory Container;

    public bool AddItem(Item _item, int _amount)
    {
        if (EmptySlotCount <= 0)
            return false;
        InventorySlot slot = FindItemOnInventory(_item);
        if (!database.GetItem[_item.Id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].item.Id <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
            {
                return Container.Items[i];
            }
        }
        return null;
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id <= -1)
            {
                Container.Items[i].UpdateSlot(_item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.item, item2.amount);
        item2.UpdateSlot(item1.item, item1.amount);
        item1.UpdateSlot(temp.item, temp.amount);
        

    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }

}
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[5];
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].RemoveItem();
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Rarity rarity = 0;
    [System.NonSerialized]
    public PlayerInventory parent;
    public Item item;
    public int amount;

    public InventorySlot()
    {
        //SetRarityColor();
        item = new Item();
        amount = 0;
    }
    public InventorySlot(Item _item, int _amount)
    {
        //SetRarityColor(item.rarity);
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(Item _item, int _amount)
    {
        //SetRarityColor(item.rarity);
        item = _item;
        amount = _amount;
    }
    public void RemoveItem()
    {
        //SetRarityColor(item.rarity);
        item = new Item();
        amount = 0;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void SetRarityColor()
    {
        parent.GetComponentInChildren<RawImage>().color = parent.inventory.database.rarityColorValues[(int)rarity];
    }

    public void SetRarityColor(Rarity rarity)
    {
        parent.transform.GetChild(1).GetComponent<Image>().color = parent.inventory.database.rarityColorValues[(int)rarity];
    }

    public Color32 GetRarityColor(Rarity rarity)
    {
        Color32 color = parent.inventory.database.rarityColorValues[(int)rarity];
        return color;
    }
}