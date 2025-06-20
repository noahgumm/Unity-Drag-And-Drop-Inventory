/*
 * Noah Gumm
 * 06/19/2025
 * 
 * Description: Item database for creating and storing new items.
 *              It's good to attempt to match the item id to the index in which it is stored although not necessary.
 *              It follows the singleton pattern.
 *              Attach this item to any gameobject.
 *              
 *              Another option for item creation is to make the item class a scriptable object and store objects within a folder hierarchy.
 *              For my personal needs I think it will be fine manually creating all the items in code.
 */
using System.Collections.Generic;
using InventoryNamespace;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    // Singleton reference
    public static ItemDatabase Instance;

    public List<Item> itemDatabase = new List<Item>();

    //  Random Items
    public Item testItem = new Item(0, "Test");

    // Build Items
    public BuildItem testBuildBlock = new BuildItem(1, "Building Block", SnapType.Any);

    private void Awake()
    {
        //Set singleton and add items to the database
        Instance = this;
        testItem.InitObject(); itemDatabase.Add(testItem);
        testBuildBlock.InitObject(); itemDatabase.Add(testBuildBlock);
    }
}
