using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CoffeeMachineLogic : MonoBehaviour
{
    private int coffee;
    public bool Idle;
    public bool DrinkSelected;
    public string MachineState;
    public CupType currentCup;
    public Drink drink;
    private float pourDuration;
    [SerializeField] private Animator cupAnimator;
    [SerializeField] private Animator pourAnimator;
    [SerializeField] private Image buttonBg;
    [SerializeField] private Button button;
    [SerializeField] private GameObject coffeeCupImg;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject pourObject;
    [SerializeField] private GameObject claimDrink;


    // Coffee Machine Logic 
    // ------------------------------------------------------------------------------------------------------

    void Start()
    {
        currentCup = CupType.NoCup;
        drink = Drink.NoDrink;
        buttonBg.color = new Color32(247, 103, 103, 255);
        button.interactable = false;
        // animator = GetComponentInChild<Animator>();
    }

    public void reset()
    {
        currentCup = CupType.NoCup;
        drink = Drink.NoDrink;
        buttonBg.color = new Color32(247, 103, 103, 255);
        updateTimeUI(0f);
        button.interactable = false;
        cupAnimator.SetTrigger("reset");
        coffeeCupImg.SetActive(false);
        claimDrink.SetActive(false);
    }

    public void HotLatte()
    {
        drink = Drink.HotLatte;
        DrinkSelected = true;
        MachineState = "DrinkSelected";
        updateTimeUI(12f);
        tryEnable();
    }
    public void ColdLatte()
    {
        drink = Drink.ColdLatte;
        DrinkSelected = true;
        MachineState = "DrinkSelected";
        updateTimeUI(10f);
        tryEnable();
    }
    public void Espresso()
    {
        drink = Drink.Espresso;
        DrinkSelected = true;
        MachineState = "DrinkSelected";
        updateTimeUI(6f);
        tryEnable();
    }

    public void Pour()
    {
        StartCoroutine(Pouring());
    }

    private void aniUp() {
        cupAnimator.Update(0f);
        pourAnimator.Update(0f);
    }

    private IEnumerator Pouring() {
        cupAnimator.ResetTrigger("reset");
        cupAnimator.SetBool("isColdLattePouring", false);
        cupAnimator.SetBool("isHotLattePouring", false);
        pourAnimator.SetBool("isColdLattePouring", false);
        pourAnimator.SetBool("isHotLattePouring", false);   
        if (drink == Drink.HotLatte) {
            //Debug.Log("Pouring a Hot Latte with a side of 67!");
            pourDuration = 12f;
            pourObject.SetActive(true);
            //aniUp();
            cupAnimator.SetBool("HotLatteSel", false);
            cupAnimator.SetBool("isHotLattePouring", true);
            pourAnimator.SetBool("isHotLattePouring", true);

        } else if (drink == Drink.ColdLatte) {
            //Debug.Log("Matter of fact let's have a cold latte pls!");
            pourDuration = 10f;
            pourObject.SetActive(true);
            //aniUp();
            cupAnimator.SetBool("ColdLatteSel", false);
            cupAnimator.SetBool("isColdLattePouring", true);
            //cupAnimator.Play("ColdLatteFillUp", 0, 0f);
            //cupAnimator.Update(0f);
            pourAnimator.SetBool("isColdLattePouring", true);
        } else if (drink == Drink.Espresso) {
            //Debug.Log("I like small bitter amounts like espresso.");
            pourDuration = 6f;
        }
        float remaining = pourDuration;
        while (remaining > 0f) {
            remaining -= Time.deltaTime;
            updateTimeUI(remaining);
            // Debug.Log(remaining);
            yield return null;
        }
        updateTimeUI(0f);
        cupAnimator.SetBool("isColdLattePouring", false);
        cupAnimator.SetBool("isHotLattePouring", false);
        pourAnimator.SetBool("isColdLattePouring", false);
        pourAnimator.SetBool("isHotLattePouring", false);
        pourObject.SetActive(false);
        claimDrink.SetActive(true);

        //reset();
    }

    private void updateTimeUI(float remaining) {
        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void setCup(CupType cupType)
    {
        currentCup = cupType;
        cupAnimator.SetBool("HotLatteSel", false);
        cupAnimator.SetBool("ColdLatteSel", false);
        if (currentCup == CupType.HotCup) {
            cupAnimator.SetBool("HotLatteSel", true);
        }
        if (currentCup == CupType.ColdCup) {
            cupAnimator.SetBool("ColdLatteSel", true);
        }
        //Debug.Log("Set Cup Type to: " + cupType);
    }

    // new Color32(255, 100, 50, 255);

    public void tryEnable()
    {
            if (drink == Drink.HotLatte && (currentCup == CupType.HotCup || currentCup == CupType.CoffeeMug))
            {
                button.interactable = true;
                //Debug.Log("Can Pour " + drink + " in " + currentCup);
                buttonBg.color = new Color32(124, 247, 103, 255);
            } else if (drink == Drink.ColdLatte && currentCup == CupType.ColdCup)
            {
                button.interactable = true;
                //Debug.Log("Can Pour " + drink + " in " + currentCup);
                buttonBg.color = new Color32(124, 247, 103, 255);
            } else if (drink == Drink.Espresso && currentCup == CupType.SmallCup) {
                button.interactable = true;
                //Debug.Log("Can Pour " + drink + " in " + currentCup);
                buttonBg.color = new Color32(124, 247, 103, 255);
            } else {
                buttonBg.color = new Color32(247, 103, 103, 255);
                button.interactable = false;
            }
    }
}
