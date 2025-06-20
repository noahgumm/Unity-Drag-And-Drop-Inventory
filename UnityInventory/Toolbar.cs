/*
 * Noah Gumm
 * 06/19/2025
 * 
 * Description: Acts as a secondary list with slots to be used as a tooldbar alongside the inventory.
 *              Attach to the parent of toolbar slots.
 *              Similar in structure and function to the inventory.
 *              Uses the singleton pattern.
 */
using System.Collections.Generic;
using InventoryNamespace;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    // Lists for slots and items along with size of toolbar
    private int toolbarSize = 6;
    public List<Item> toolbar = new List<Item>();
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public static Toolbar Instance { get; private set; }

    private RectTransform rt;

    private void Awake()
    {
        // Set up singleton
        Instance = this;

        // Populate toolbar list with null values
        for (int i = 0; i < toolbarSize; i++)
            toolbar.Add(null);
    }

    private void Start()
    {
        // Get drop slots from children
        var dropSlots = transform.GetComponentsInChildren<DropSlot>();

        // Iterate through drop slots and init them along with item slots list
        for (int i = 0; i < dropSlots.Length; i++)
        {
            ItemSlot itemSlot = dropSlots[i].GetComponentInChildren<ItemSlot>();
            itemSlot.SetIndex(i);
            itemSlots.Add(itemSlot);
            dropSlots[i].SetIndex(i);
            itemSlot.SetItem(toolbar[i]);
        }

        rt = GetComponent<RectTransform>();
        Vector2 offset = new Vector2(Screen.width / 2, rt.offsetMin.y);
        rt.offsetMin = offset;
    }

    // Basic swap between slots and setting items
    public void Swap(int fromIndex, int toIndex)
    {
        (toolbar[fromIndex], toolbar[toIndex]) = (toolbar[toIndex], toolbar[fromIndex]);

        itemSlots[fromIndex].SetItem(toolbar[fromIndex]);
        itemSlots[toIndex].SetItem(toolbar[toIndex]);
    }

    // Moves toolbar to make room for the inventory to pop up
    // May not be needed in many cases
    public void ShiftToolbar(bool shiftRight = true)
    {
        Vector2 newOffset;

        float xOffset = shiftRight ? 1310f : Screen.width / 2;
        newOffset = new Vector2(xOffset, rt.offsetMin.y);

        rt.offsetMin = newOffset;
    }
}
