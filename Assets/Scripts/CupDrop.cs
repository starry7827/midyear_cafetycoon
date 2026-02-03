using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CupDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject cupObject;
    private Image rend;
    [SerializeField] private Sprite hotcup;
    [SerializeField] private Sprite coldcup;
    [SerializeField] public CupType cupType;
    // [SerializeField] private Script coffeeMachine;

    public enum Drink {
        HotLatte, ColdLatte, Espresso
    }

    public enum CupType {
        HotCup, ColdCup
    }

    void Start() {
        rend = cupObject.GetComponent<Image>();
    }

    public void OnDrop(PointerEventData data) {
        if (data.pointerDrag != null) {
            Debug.Log("Dropped Object was: " + data.pointerDrag.name);
            cupObject.SetActive(true);
            if (data.pointerDrag.name == "HotCup") {
                //coffeeMachine.setCup(CupType.HotCup);
                rend.sprite = hotcup;
            } if (data.pointerDrag.name == "ColdCup") {
                //coffeeMachine.setCup(CupType.ColdCup);
                rend.sprite = coldcup;
            }
        }
    }
}
