/*
 * Noah Gumm
 * 06/19/2025
 * 
 * Description: Inherits from item class and specifies variables needed for buildable items
 *              Not necessary for the system to function ,but a good example of how it can be extended.
 */
namespace InventoryNamespace
{
    // Inherits from Item
    public class BuildItem : Item
    {
        public SnapType SnapType { get; private set; }

        public BuildItem(int id, string name, SnapType snapType)
            : base(id, name)
        {
            SnapType = snapType;
        }
    }

    // Enum for snapping compatibility
    public enum SnapType
    {
        Any,
        Wall,
        Floor,
        Door,
        Window,
        Roof
    }
}