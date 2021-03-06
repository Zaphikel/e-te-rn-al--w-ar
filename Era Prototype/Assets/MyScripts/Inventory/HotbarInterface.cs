﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarInterface : PlayerInventory
{
    public GameObject[] slots;

    public override void CreateSlots()
    {
        // base.CreateSlots();
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = slots[i];
            inventory.Container.Items[i].parent = this;

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            slotsOnInterface.Add(obj, inventory.Container.Items[i]);
        }
    }

    public override void Update()
    {
        if (previousInventory != inventory)
        {
            UpdateInventoryLinks();
        }
        previousInventory = inventory;
        slotsOnInterface.UpdateSlotDisplay();

    }
}
