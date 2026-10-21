using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Template")]
public class ItemTemplate : ScriptableObject
{
    public string Id;
    public string DisplayName;
    public string Description;
    public Sprite Icon;
    public float Price;
    public int MaxStackSize = 99;
}