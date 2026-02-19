using UnityEngine;
using UnityEngine.UI; // Fixes the Image error

public class Tray : MonoBehaviour
{
    public GameObject trayPrefab;


    public Sprite[] drinkSprites;
    public Sprite[] foodSprites;

    private Image itemIcon1;
    private Image itemIcon2;
    public Vector2 rightOffset = new Vector2(15f, 0f);
    public Vector2 leftOffset = Vector2.zero;
    public Vector2 idleOffset = Vector2.zero;

    private Transform trayTransform;
    private GameObject trayInstance;
    private bool facingRight = true;


    void Awake()
    {
        trayInstance = Instantiate(trayPrefab, transform);
        trayTransform = trayInstance.transform;

        trayTransform.localScale = new Vector3(0.1f, 0.1f, 1f);

        itemIcon1 = trayInstance.transform.Find("IconContainer/ItemIcon1").GetComponent<Image>();
        itemIcon2 = trayInstance.transform.Find("IconContainer/ItemIcon2").GetComponent<Image>();

        trayInstance.SetActive(false);
    }

    public void SetDirection(bool isRight)
    {
        facingRight = isRight;
        UpdateTrayPosition();
    }

    private void UpdateTrayPosition()
    {
        if (trayTransform == null) return;

        Vector2 currentOffset = facingRight ? rightOffset : leftOffset;

        trayTransform.localPosition = new Vector3(currentOffset.x, currentOffset.y, -0.1f);
    }

    public void ResetToIdle()
    {
        // When idling, we just keep it at the current directional offset
        UpdateTrayPosition();
    }

    public void ShowTrayIcons(Drink drink, Food food)
    {
        trayInstance.SetActive(true);

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

    public void HideTray()
    {
        trayInstance.SetActive(false);
    }


}