using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class SlotView : MonoBehaviour, IPointerClickHandler
{
    #region События

    public event Action<SlotView> OnRightClicked;

    public event Action<SlotView> OnLeftClicked;

    public event Action<SlotView> OnDoubleClicked;

    #endregion События

    [Header("Visuals")]
    [SerializeField] private Image iconImage;

    [SerializeField] private TextMeshProUGUI countText;

    private InventoryItem _item;
    public InventoryItem Item => _item;

    public void Initialize(InventoryItem item)
    {
        _item = item;

        if (item != null && item.Template != null)
        {
            iconImage.sprite = item.Template.Icon;
            iconImage.gameObject.SetActive(true);

            if (item.Amount > 1)
            {
                countText.text = $"x{item.Amount}";
                countText.gameObject.SetActive(true);
            }
            else
            {
                countText.gameObject.SetActive(false);
            }
        }
        else
        {
            iconImage.gameObject.SetActive(false);
            countText.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && _item != null)
        {
            OnRightClicked?.Invoke(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Left && _item != null)
        {
            if (eventData.clickCount == 2)
                OnDoubleClicked?.Invoke(this);
            else
                OnLeftClicked?.Invoke(this);
        }
    }
}