using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField]
    private InventoryStorage storage;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        if (storage != null)
        {
            moneyText.text = storage.Money.ToString();
            storage.OnMoneyChanged += UpdateMoney;
        }
    }

    private void OnDisable()
    {
        storage.OnMoneyChanged -= UpdateMoney;
    }

    private void UpdateMoney(int newValue)
    {
        if (storage == null)
        {
            moneyText.text = "0";
            return;
        }

        moneyText.text = newValue.ToString();
    }
}