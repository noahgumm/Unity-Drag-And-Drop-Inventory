# Unity-Drag-And-Drop-Inventory
TL;DR Inventory System in Unity 3D that utilizes 'drag and drop' and toolbar functionality.

## Introduction
I created this repository because nearly everytime I go to create a new game project I find myself needing an inventory system. I'm always been inclined to use the classic drag and drop with icons. So here I have included that
exact system. That way I will no longer need to recreate it for all of my projects. It can also be used and easily modified by anybody.

## How to use
Item  & BuildItem: These are simply classes for the item objects with constructors so you can easily create them. BuildItem is an example of how you can inherit and extend from Item.
ItemDatabase: ItemDatabase is simply, as its name implies, a place for creating and storing items for later access. Create your items here and attach this script anywhere as it uses the singleton pattern.
Inventory: Attach to anything. I don't recommend attaching to the inventory UI if you plan on enabling and disabling it. That's why there's a variable for the Transform of the inventory. Make sure the inventory transform is set to the object that will have drop slots as direct children.
Toolbar: The toolbar is exact copy of the inventory functionality-wise. The only key difference is you must attach the toolbar script to the parent object containing the toolbar drop slots.
DropSlot: Simply lay out your drop slots as children of the inventory with as many as you want in whatever configuration you want and attach the DropSlot script to each one. Make sure your inventorySize variable within inventory matches the number of slots you create.
ItemSlot: Copy all of your drop slots and keep the copies in the same position overlapping the originals. Attach the ItemSlot script to each one.
DropSlot + ItemSlot for toolbar: Make sure to tick isToolbarSlot to true for toolbar slots.

## Possible improvements
1. Item Creation: For a game with a large number of items it would be more efficient to create items using scriptable items. Then the use of an ItemDatabase would be completely unecessary. For my usual project scale I find it completely fine creating all items manually through code.
2. The item slots are iterated over and their properties set ,but the slots could just be generated automatically through code to avoid manual slot placement. I decided not to do this because I find positioning UI elements can be finnicky at times.

## Tips for modifications
Toolbar: If you aren't planning on using the toolbar you can simply remove the SwapBetweenLists functions on the DropSlot and remove the ToolbarSwap function within the if statement.
