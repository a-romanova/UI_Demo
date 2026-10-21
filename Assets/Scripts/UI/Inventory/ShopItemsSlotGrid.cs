using UnityEngine;

public class ShopItemsSlotGrid : SlotGrid
{
    [Header("Shop Specific")]
    [Tooltip("Хранилище игрока для покупки")]
    [SerializeField] private InventoryStorage playerStorage;

    public override void BuyItem()
    {
        if (_selectedSlot?.Item != null && playerStorage != null && primaryStorage != null)
        {
            var item = _selectedSlot.Item;
            if (playerStorage.SpendMoney((int)item.Template.Price))
            {
                primaryStorage.GetMoney((int)item.Template.Price);
                primaryStorage.RemoveItem(item, 1);

                playerStorage.AddItem(new InventoryItem(item.Template, 1));

                _selectedSlot = null;
                HideContextMenu();
            }
            else
            {
                Debug.Log("Недостаточно денег!");
            }
        }
    }

    protected override void HandleDoubleClick(SlotView clickedSlot)
    {
        base.HandleDoubleClick(clickedSlot);
        BuyItem();
    }
}