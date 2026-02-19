using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; // Makes it easy to access from other scripts

    public int money = 0;
    public TextMeshProUGUI moneyText;

    public Drink currentDrinkInHand;
    public Food currentFoodInHand;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();

    }

    public void AddMoney()
    {
        if (currentDrinkInHand == Drink.ColdLatte) money+= 6;
        if (currentDrinkInHand == Drink.HotLatte) money+= 5;
        if (currentDrinkInHand == Drink.Espresso) money+= 3;
        if (currentFoodInHand == Food.Muffin) money+= 4;
        if (currentFoodInHand == Food.Crossaint) money+= 3;
        if (currentFoodInHand == Food.Cookie) money+= 2;
        UpdateUI();
        Debug.Log("Earned money! Total: " + money);
    }

    public void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "$" + money.ToString("N0");
    }

    public void PickUpDrink(Drink drink)
    {
        currentDrinkInHand = drink;
        Debug.Log("Holding: " + drink + " and " + currentFoodInHand);
    }

    public void PickUpFood(Food food)
    {
        currentFoodInHand = food;
        Debug.Log("Holding: " + currentDrinkInHand + " and " + food);
    }

    public void ClearInventory()
    {
        currentDrinkInHand = Drink.NoDrink;
        currentFoodInHand = Food.NoFood;
    }
}