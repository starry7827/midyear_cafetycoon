using UnityEngine;
using UnityEngine.UI; // Fixes the Image error

public class NPCOrderPopUpController : MonoBehaviour
{
    public GameObject popupPrefab;
    public Vector3 offset = new Vector3(0f, 1.5f, -0.1f);

    public Sprite[] drinkSprites; 
    public Sprite[] foodSprites;  

    private Image itemIcon1;        
    private Image itemIcon2;        
    public GameObject takeOrderText; 

    private GameObject popupInstance;


    void Awake()
    {
        popupInstance = Instantiate(popupPrefab, transform);
        popupInstance.transform.localPosition = offset;
        popupInstance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        itemIcon1 = popupInstance.transform.Find("IconContainer/ItemIcon1").GetComponent<Image>();
        itemIcon2 = popupInstance.transform.Find("IconContainer/ItemIcon2").GetComponent<Image>();
        takeOrderText = popupInstance.transform.Find("TakeOrderText").gameObject;

        popupInstance.SetActive(false);
    }

    public void ShowTakeOrder()
    {
        popupInstance.SetActive(true);
        takeOrderText.SetActive(true);
        itemIcon1.gameObject.SetActive(false);
        itemIcon2.gameObject.SetActive(false);
    }

    public void ShowOrderIcons(Drink drink, Food food)
    {
        popupInstance.SetActive(true);
        takeOrderText.SetActive(false);

        if (drink != Drink.NoDrink && (int)drink < drinkSprites.Length)
        {
            itemIcon1.sprite = drinkSprites[(int)drink];
            itemIcon1.gameObject.SetActive(true);
        }
        else { itemIcon1.gameObject.SetActive(false); }

        if (food != Food.NoFood && (int)food < foodSprites.Length)
        {
            itemIcon2.sprite = foodSprites[(int)food];
            itemIcon2.gameObject.SetActive(true);
        }
        else { itemIcon2.gameObject.SetActive(false); }
    }

    public void HidePopup(){
        popupInstance.SetActive(false);
    }

    public void Display()
    {
        Debug.Log("appear!");
        popupInstance.SetActive(true);
    }
}