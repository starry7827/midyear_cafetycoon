using UnityEngine;
using UnityEngine.UI;

public class CoffeeMachineLogic : MonoBehaviour
{
    private int coffee;
    public bool Idle;
    public bool DrinkSelected;
    public string MachineState;
    public CupType currentCup;
    public Drink drink;
    [SerializeField] private Image buttonBg;
    [SerializeField] private Button button;

    // Coffee Machine Logic 
    // ------------------------------------------------------------------------------------------------------

    void Start() {
        currentCup = CupType.NoCup;
        drink = Drink.NoDrink;
        buttonBg.color = new Color32(247, 103, 103, 255);
        button.interactable = false;
    }

    public void reset() {
        currentCup = CupType.NoCup;
        drink = Drink.NoDrink;
        buttonBg.color = new Color32(247, 103, 103, 255);
        button.interactable = false;
    }
    
    public void HotLatte() {
        drink = Drink.HotLatte;
        DrinkSelected = true;
        MachineState = "DrinkSelected";
        tryEnable();
    }
    public void ColdLatte() {
        drink = Drink.ColdLatte;
        MachineState = "DrinkSelected";
        tryEnable();
    }
    public void Espresso() {
        drink = Drink.Espresso;
        MachineState = "DrinkSelected";
        tryEnable();
    }

    public void Pour() {
        MachineState = "Pouring";
        Debug.Log("pouring: " + coffee);
    }

    public void setCup(CupType cupType) {
        currentCup = cupType;
        Debug.Log("Set Cup Type to: " + cupType);
    }

    // new Color32(255, 100, 50, 255);

    public void tryEnable() {
        if (drink == Drink.HotLatte && currentCup == CupType.HotCup) {
            button.interactable = true;
            Debug.Log("Can Pour " + drink + " in " + currentCup);
            buttonBg.color = new Color32(124, 247, 103, 255);
        }
        if (drink == Drink.ColdLatte && currentCup == CupType.ColdCup) {
            button.interactable = true;
            Debug.Log("Can Pour " + drink + " in " + currentCup);
            buttonBg.color = new Color32(124, 247, 103, 255);
        }
    }
}
