/*
 * Noah Gumm
 * 06/19/2025
 * 
 * Description: This acts as the draggable slot in the inventory.
 *              Attach it to a UI item that overlays and is a child of the drop slot. (Usually an exact copy of the drop slot visually)
 */
using TMPro;
using InventoryNamespace;
using UnityEngine.EventSystems;
using UnityEngine;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Currently uses text mesh pro to show what item is in the slot
    // Could easily add an icon/image to the time object and use that here instead
    [SerializeField] private TMP_Text label;

    // The original parent of the slot ( the drop slot), the canvas group of the slots, the canvas itself, and the item stored in the slot 
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Item currentItem;

    // Keep track of index and if its part of the toolbar
    public int Index { get; private set; }
    public bool isToolbarSlot = false;

    public void SetIndex(int index) => Index = index;

    public void Awake()
    {
        // Get all components
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalParent = transform.parent;
        label = GetComponentInChildren<TMP_Text>();
    }

    // Set the item in the slot and update visuals
    // This is where you would update the icon if you were using one
    public void SetItem(Item item)
    {
        currentItem = item;

        if (item != null)
        {
            // Update label with item name 
            label.text = item.Name;
            label.enabled = true;
            canvasGroup.alpha = 1f;
        }
        else
        {
            // Label slot as empty and lower alpha
            label.text = "Empty";
            label.enabled = true;
            canvasGroup.alpha = 0.5f;
        }

        // Prevent issues with raycasts hitting dropslots at the same time as drag slots
        canvasGroup.blocksRaycasts = true;
    }

    // Remove item and reset visuals
    public void RemoveItem()
    {
        currentItem = null;

        label.text = "Empty";
        label.enabled = true;
        canvasGroup.alpha = 0.5f;
    }

    // When you begin to drag the slot
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Can't drag empty slots
        if (currentItem == null) return;

        // Set parent to the transform
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    // When dragging
    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        // Item slot tracks mouse position
        transform.position = eventData.position;
    }

    // When dropping
    public void OnEndDrag(PointerEventData eventData)
    {
        // Return slot to original parent and position
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = currentItem != null ? 1f : 0.5f;
    }
}
