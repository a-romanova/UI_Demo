using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private int _health = 100;

    [SerializeField] private int _food = 100;
    [SerializeField] private InventoryStorage _inventory;

    #region События

    public event Action<int> OnHealthChanged;

    public event Action<int> OnFoodChanged;

    #endregion События

    #region Свойства

    public int Health => _health;
    public int Food => _food;
    public InventoryStorage Inventory => _inventory;

    #endregion Свойства

    public void GetDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, 100);
        OnHealthChanged?.Invoke(_health);
    }

    public void Heal(int hp)
    {
        _health = Mathf.Clamp(_health + hp, 0, 100);
        OnHealthChanged?.Invoke(_health);
    }

    public void GetFood(int amount)
    {
        _food = Mathf.Clamp(_food + amount, 0, 100);
        OnFoodChanged?.Invoke(_food);
    }

    public bool SpendFood(int amount)
    {
        if (_food >= amount)
        {
            _food -= amount;
            OnFoodChanged?.Invoke(_food);
            return true;
        }
        return false;
    }
}