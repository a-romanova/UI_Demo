using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryStorage", menuName = "Data/InventoryStorage")]
public class InventoryStorage : ScriptableObject
{
    [SerializeField] private int _money = 500;
    [SerializeField] private List<InventoryItem> _inventory = new();
    [SerializeField] private PlayerData OwnerData;
    public int Money => _money;
    public ReadOnlyCollection<InventoryItem> Inventory => _inventory.AsReadOnly();

    public event Action<int> OnMoneyChanged;

    public event Action OnInventoryChanged;

    public void GetMoney(int amount)
    {
        _money += amount;
        OnMoneyChanged?.Invoke(_money);
    }

    public bool SpendMoney(int amount)
    {
        if (_money >= amount)
        {
            _money -= amount;
            OnMoneyChanged?.Invoke(_money);
            return true;
        }
        return false;
    }

    public void AddItem(InventoryItem item, int amount = 1)
    {
        var existingItem = _inventory.Find(i => i.Template.DisplayName == item.Template.DisplayName);
        if (existingItem != null)
        {
            existingItem.Add(amount);
        }
        else
        {
            _inventory.Add(item);
        }

        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(InventoryItem item, int amount = 1)
    {
        if (item.Amount <= amount)
        {
            _inventory.Remove(item);
        }
        else
        {
            item.Amount -= amount;
        }

        OnInventoryChanged?.Invoke();
    }

    public void BuyItem(ItemTemplate template, int amount = 1)
    {
        if (SpendMoney((int)template.Price * amount)) // Если денег хватило
        {
            AddItem(new InventoryItem(template, amount));
        }
        OnInventoryChanged?.Invoke();
    }

    public void SellItem(InventoryItem item, int amount = 1)
    {
        GetMoney((int)(item.Template.Price * amount));
        RemoveItem(item);
    }

    public void UseItem(InventoryItem item)
    {
        if (OwnerData == null)
        {
            Debug.Log($"Cant use {item?.Template.name}, no owner");
            return;
        }

        RemoveItem(item);
    }
}