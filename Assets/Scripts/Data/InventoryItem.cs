using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemTemplate Template;
    public int Amount;

    public bool IsEmpty => Amount <= 0;
    public bool IsFull => Amount >= Template.MaxStackSize;

    public InventoryItem(ItemTemplate template, int amount = 1)
    {
        Template = template;
        Amount = amount;
    }

    public void Add(int count = 1)
    {
        int canAdd = Mathf.Min(count, Template.MaxStackSize - Amount);
        Amount += canAdd;
    }

    public void Remove(int count = 1)
    {
        Amount = Mathf.Max(0, Amount - count);
    }
}