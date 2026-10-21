using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class SlotGrid : MonoBehaviour
{
    public static event Action<SlotGrid> OnAnyMenuOpened;

    private static readonly List<SlotGrid> _activeGrids = new List<SlotGrid>();

    [Header("Data")]
    [Tooltip("¿ÒÒÂÚ InventoryStorage (Ë„ÓÍ‡ ËÎË Ï‡„‡ÁËÌ‡)")]
    [SerializeField] protected InventoryStorage primaryStorage;

    [Header("UI")]
    [SerializeField] protected SlotView slotPrefab;

    [SerializeField] protected Transform contentParent;

    [SerializeField] protected GameObject contextMenu;

    protected List<SlotView> _activeSlots = new List<SlotView>();
    protected SlotView _selectedSlot = null;

    private void OnEnable()
    {
        _activeGrids.Add(this);
        OnAnyMenuOpened += HandleOtherMenuOpened;

        if (primaryStorage != null)
        {
            primaryStorage.OnInventoryChanged += RefreshGrid;
        }
        RefreshGrid();
    }

    private void OnDisable()
    {
        _activeGrids.Remove(this);
        OnAnyMenuOpened -= HandleOtherMenuOpened;

        if (primaryStorage != null)
        {
            primaryStorage.OnInventoryChanged -= RefreshGrid;
        }

        ClearGrid();
    }

    protected virtual void RefreshGrid()
    {
        ClearGrid();
        if (primaryStorage == null) return;

        foreach (var item in primaryStorage.Inventory)
        {
            CreateSlot(item);
        }
    }

    protected void CreateSlot(InventoryItem item)
    {
        var slot = Instantiate(slotPrefab, contentParent);
        slot.Initialize(item);

        slot.OnRightClicked += HandleRightClick;
        slot.OnLeftClicked += HandleLeftClick;
        slot.OnDoubleClicked += HandleDoubleClick;

        _activeSlots.Add(slot);
    }

    protected void ClearGrid()
    {
        _selectedSlot = null;
        foreach (var slot in _activeSlots)
        {
            slot.OnRightClicked -= HandleRightClick;

            if (slot != null)
                Destroy(slot.gameObject);
        }
        _activeSlots.Clear();
    }

    public static void CloseAllContextMenus()
    {
        foreach (var grid in _activeGrids)
        {
            grid.HideContextMenu();
        }
    }

    #region Œ¡–¿¡Œ“◊» »  À» Œ¬

    protected void HandleRightClick(SlotView clickedSlot)
    {
        _selectedSlot = clickedSlot;
        if (contextMenu != null)
        {
            contextMenu.transform.position = clickedSlot.gameObject.transform.position;
            contextMenu.SetActive(true);
            OnAnyMenuOpened?.Invoke(this);
        }
    }

    protected void HandleLeftClick(SlotView clickedSlot)
    {
        _selectedSlot = clickedSlot;
        CloseAllContextMenus();
        if (contextMenu != null)
            contextMenu.SetActive(false);
    }

    protected virtual void HandleDoubleClick(SlotView clickedSlot)
    {
        _selectedSlot = clickedSlot;
        CloseAllContextMenus();
        if (contextMenu != null)
            contextMenu.SetActive(false);
    }

    public void HideContextMenu()
    {
        if (contextMenu != null)
            contextMenu.SetActive(false);
    }

    protected void HandleOtherMenuOpened(SlotGrid otherGrid)
    {
        if (otherGrid != this)
        {
            HideContextMenu();
        }
    }

    #endregion Œ¡–¿¡Œ“◊» »  À» Œ¬

    #region ƒ≈…—“¬»ﬂ

    public virtual void UseItem()
    {
        if (_selectedSlot?.Item != null && primaryStorage != null)
        {
            primaryStorage.UseItem(_selectedSlot.Item);
            _selectedSlot = null;
            HideContextMenu();
        }
    }

    public virtual void DropItem()
    {
        if (_selectedSlot?.Item != null && primaryStorage != null)
        {
            var item = _selectedSlot.Item;
            _selectedSlot = null;
            primaryStorage.RemoveItem(item, item.Amount);
            HideContextMenu();
        }
    }

    public virtual void BuyItem()
    { }

    public virtual void SellItem()
    { }

    #endregion ƒ≈…—“¬»ﬂ
}