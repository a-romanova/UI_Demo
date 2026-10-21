using UnityEngine;

public class InventorySlotGrid : SlotGrid
{
    protected override void HandleDoubleClick(SlotView clickedSlot)
    {
        base.HandleDoubleClick(clickedSlot);
        UseItem();
    }
}