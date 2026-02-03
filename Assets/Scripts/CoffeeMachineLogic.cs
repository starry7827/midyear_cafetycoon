using UnityEngine;

public class CoffeeMachineLogic : MonoBehaviour
{
    private int coffee;
    public bool Idle;
    public bool DrinkSelected;
    public string MachineState;
    public string currentCup;

    // Coffee Machine Logic 
    // ------------------------------------------------------------------------------------------------------

    public void HotLatte() {
        coffee = 1;
        DrinkSelected = true;
        MachineState = "DrinkSelected";
    }
    public void ColdLatte() {
        coffee = 2;
        MachineState = "DrinkSelected";
    }
    public void Espresso() {
        coffee = 3;
        MachineState = "DrinkSelected";
    }

    public void check() {
        if ((coffee == 1) && currentCup == "HotCup") {
            
        }
    }

    public void Pour() {
        MachineState = "Pouring";
        Debug.Log("pouring: " + coffee);
    }

    public void setCup(string cupType) {
        currentCup = cupType;
    }
}
