/*
 * Noah Gumm
 * 06/19/2025
 * 
 * Description: Class handles inventory size and adding and removing items
 *              Uses a singleton pattern. 
 *              Attach to any game object.
 */
using System.Collections.Generic;
using InventoryNamespace;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Reference of iventory for singleton and a reference to the item database for testing
    public static Inventory Instance;
    private ItemDatabase itemDatabase;

    // A list for inventory items and the associated slots
    public List<Item> inventory = new List<Item>();
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    // A reference to the inventory to panel to get the item slots
    public Transform inventoryPanel;

    // Size of inventory
    // Just used as a safegaurd when looping, make sure it matches actual slot count
    public int inventorySize = 12;

    private void Awake()
    {
        // Set singleton
        Instance = this;

        // Populate the inventory with null values
        for (int i = 0; i < inventorySize; i++)
            inventory.Add(null);
    }

    private void Start()
    {
        // For testing purposes add two items to the inventory
        itemDatabase = ItemDatabase.Instance;
        AddItem(itemDatabase.itemDatabase[0]);
        AddItem(itemDatabase.itemDatabase[1]);

        // Auto-grab slots from DropSlot children
        var dropSlots = inventoryPanel.GetComponentsInChildren<DropSlot>();

        // Iterate over item slots and initilize them
        for (int i = 0; i < dropSlots.Length; i++)
        {
            ItemSlot itemSlot = dropSlots[i].GetComponentInChildren<ItemSlot>();
            // Set the index of the item slot and add to list
            itemSlot.SetIndex(i);
            itemSlots.Add(itemSlot);

            // Set drop slot index to match item slot
            dropSlots[i].SetIndex(i);

            // Set items based on inventory values
            itemSlot.SetItem(inventory[i]);
        }
    }

    // Swap two slots based on index, works on moving items to empty slots as well
    public void Swap(int fromIndex, int toIndex)
    {
        // Swap slots
        (inventory[fromIndex], inventory[toIndex]) = (inventory[toIndex], inventory[fromIndex]);

        // Update the slots values
        itemSlots[fromIndex].SetItem(inventory[fromIndex]);
        itemSlots[toIndex].SetItem(inventory[toIndex]);
    }

    // Adds an item to the inventory
    public bool AddItem(Item item)
    {
        // Iterate over invenory and add item to first empty slot
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;

                if (itemSlots != null && i < itemSlots.Count)
                {
                    itemSlots[i].SetItem(item);
                }
                return true;
            }
        }

        // Return with warning if the inventory is full
        Debug.LogWarning("Inventory full. Could not add item: " + item?.Name);
        return false;
    }

    // Remove an item from the inventory
    public void RemoveItem(int index)
    {
        Debug.Log($"Removing item at index {index}");
        itemSlots[index].RemoveItem();
    }
}
