/*
 * Noah Gumm
 * 06/19/2025
 * 
 * Description: This script attaches to the visual UI drop point for inventory slots.
 *              It takes the slot being dragged and swaps the item between it and this drop slot.
 *              
 *              A majority of the if statements in OnDrop can be removed if no toolbar is being used
 */
using System.Collections.Generic;
using InventoryNamespace;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropSlot : MonoBehaviour, IDropHandler
{
    // Index of the slot for associating with items
    private int index;
    // Used for differentiating between drop actions
    public bool isToolbarSlot = false;

    public void SetIndex(int i) => index = i;

    public void OnDrop(PointerEventData eventData)
    {
        // Get the slot being dragged and make sure it isn't null and isn't being dropped on the same slot
        var dragged = eventData.pointerDrag?.GetComponent<ItemSlot>();
        if (dragged == null || (dragged.Index == index && ((isToolbarSlot == dragged.isToolbarSlot) || (!isToolbarSlot == !dragged.isToolbarSlot)))) return;

        if (!dragged.isToolbarSlot && isToolbarSlot)
        {
            //If the slot is dragged from inventory to toolbar
            SwapBetweenLists(Inventory.Instance.inventory, Inventory.Instance.itemSlots, dragged.Index, Toolbar.Instance.toolbar, Toolbar.Instance.itemSlots, index);
        }
        else if (!dragged.isToolbarSlot && !isToolbarSlot)
        {
            //If the slot is dragged from inventory to inventory
            Inventory.Instance.Swap(dragged.Index, index);
        }
        else if (dragged.isToolbarSlot && isToolbarSlot)
        {
            //If the slot is dragged from toolbar to toolbar
            Toolbar.Instance.Swap(dragged.Index, index);
        }
        else if (dragged.isToolbarSlot && !isToolbarSlot)
        {
            //If the slot is dragged from toolbar to inventory
            SwapBetweenLists(Inventory.Instance.inventory, Inventory.Instance.itemSlots, index, Toolbar.Instance.toolbar, Toolbar.Instance.itemSlots, dragged.Index);
        }
    }

    // Basic swap function that also sets items in slots for moving items between toolbar and inventory
    public void SwapBetweenLists(List<Item> listA, List<ItemSlot> slotA, int indexA,
                             List<Item> listB, List<ItemSlot> slotB, int indexB)
    {
        Item temp = listA[indexA];
        listA[indexA] = listB[indexB];
        listB[indexB] = temp;

        slotA[indexA].SetItem(listA[indexA]);
        slotB[indexB].SetItem(listB[indexB]);
    }
}
