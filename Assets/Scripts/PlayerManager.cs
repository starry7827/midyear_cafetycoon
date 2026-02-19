using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; // Makes it easy to access from other scripts

    public int money = 1000;
    public int moneyg = 0;
    public TextMeshProUGUI moneyText;

    public Drink currentDrinkInHand;
    public Food currentFoodInHand;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI mg;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
        mg.gameObject.SetActive(false);

    }

    public void AddMoney()
    {
        if (currentDrinkInHand == Drink.ColdLatte) moneyg = 6;
        if (currentDrinkInHand == Drink.HotLatte) moneyg = 5;
        if (currentDrinkInHand == Drink.Espresso) moneyg = 3;
        if (currentFoodInHand == Food.Muffin) moneyg = 4;
        if (currentFoodInHand == Food.Crossaint) moneyg = 3;
        if (currentFoodInHand == Food.Cookie) moneyg = 2;
        money += moneyg;
        StartCoroutine(popUp());
        moneyg = 0;
        UpdateUI();
        Debug.Log("Earned money! Total: " + money);
    }

    private IEnumerator popUp()
    {
        mg.gameObject.SetActive(true);
        mg.text = "+$" + moneyg.ToString("N0");
        float remaining = 3;
        while (remaining > 0f)
        {
            remaining -= Time.deltaTime;
            yield return null;
        }
        mg.gameObject.SetActive(false);
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
        animator.SetBool("Holding", true);
        if (PlayerMovement.Instance != null && PlayerMovement.Instance.trayScript != null)
            PlayerMovement.Instance.trayScript.ShowTrayIcons(currentDrinkInHand, currentFoodInHand); 
    }

    public void PickUpFood(Food food)
    {
        currentFoodInHand = food;
        Debug.Log("Holding: " + currentDrinkInHand + " and " + food);
        animator.SetBool("Holding", true);
        if (PlayerMovement.Instance != null && PlayerMovement.Instance.trayScript != null)
            PlayerMovement.Instance.trayScript.ShowTrayIcons(currentDrinkInHand, currentFoodInHand); 
    }

    public void ClearInventory()
    {
        currentDrinkInHand = Drink.NoDrink;
        currentFoodInHand = Food.NoFood;
        animator.SetBool("Holding", false);
        PlayerMovement.Instance.trayScript.HideTray();
    }
}