using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerData player;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider foodSlider;
    [SerializeField] private Slider waterSlider;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        player.OnHealthChanged += UpdateHealth;
        player.OnFoodChanged += UpdateFood;

        UpdateHealth(player.Health);
        UpdateFood(player.Food);
        UpdateMoney(player.Inventory.Money);
    }

    private void OnDisable()
    {
        player.OnHealthChanged -= UpdateHealth;
        player.OnFoodChanged -= UpdateFood;
        player.Inventory.OnMoneyChanged -= UpdateMoney;
    }

    private void UpdateHealth(int value) => healthSlider.value = value;

    private void UpdateFood(int value) => foodSlider.value = value;

    private void UpdateMoney(int value)
    {
        moneyText.text = value.ToString();
    }
}