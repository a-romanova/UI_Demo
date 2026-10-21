using UnityEngine;

public class PlayerShopSlotGrid : SlotGrid
{
    [Header("Shop Specific")]
    [Tooltip("Хранилище магазина для продажи")]
    [SerializeField] private InventoryStorage shopStorage;

    [SerializeField] private float discountCoeff = 0.5f;

    public override void SellItem()
    {
        if (_selectedSlot?.Item != null && shopStorage != null && primaryStorage != null)
        {
            var item = _selectedSlot.Item;
            _selectedSlot = null;

            int sellPrice = (int)(item.Template.Price * discountCoeff);
            primaryStorage.GetMoney(sellPrice);

            primaryStorage.RemoveItem(item, 1);
            shopStorage.AddItem(new InventoryItem(item.Template, 1));

            HideContextMenu();
        }
    }

    //public override void UseItem()
    //{
    //    base.UseItem();
    //}

    //public override void DropItem()
    //{
    //    base.DropItem();
    //}

    protected override void HandleDoubleClick(SlotView clickedSlot)
    {
        base.HandleDoubleClick(clickedSlot);
        SellItem();
    }
}