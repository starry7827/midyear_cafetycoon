using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; // Makes it easy to access from other scripts

    public int money = 0;
    public TextMeshProUGUI moneyText;

    public Drink currentDrinkInHand = Drink.NoDrink;
    public Food currentFoodInHand = Food.NoFood;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
        Debug.Log("Earned money! Total: " + money);
    }

    public void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "$" + money.ToString("N0");
    }

    public void PickUpItem(Drink drink, Food food)
    {
        currentDrinkInHand = drink;
        currentFoodInHand = food;
        Debug.Log("Holding: " + drink + " and " + food);
    }

    public void ClearInventory()
    {
        currentDrinkInHand = Drink.NoDrink;
        currentFoodInHand = Food.NoFood;
    }
}