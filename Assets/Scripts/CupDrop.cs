using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CupDrop : MonoBehaviour, IDropHandler
{
    private Image rend;
    [SerializeField] private GameObject cupObject;
    [SerializeField] private Sprite hotcup;
    [SerializeField] private Sprite coldcup;
    [SerializeField] private Sprite coffeemug;
    [SerializeField] private Sprite smallcup;
    [SerializeField] public CupType cupType;
    [SerializeField] public CoffeeMachineLogic cfm;
    // [SerializeField] private Script coffeeMachine;

    void Start() {
        rend = cupObject.GetComponent<Image>();
    }

    public void OnDrop(PointerEventData data) {
        cfm.reset();
        if (data.pointerDrag != null) {
            Debug.Log("Dropped Object was: " + data.pointerDrag.name);
            cupObject.SetActive(true);
            cfm.tryEnable();
            if (data.pointerDrag.name == "HotCup") {
                cfm.setCup(CupType.HotCup);
                //rend.sprite = hotcup;
            } if (data.pointerDrag.name == "ColdCup") {
                cfm.setCup(CupType.ColdCup);
                // rend.sprite = coldcup;
            } if (data.pointerDrag.name == "CoffeeMug") {
                cfm.setCup(CupType.CoffeeMug);
                // rend.sprite = coffeemug;
            } if (data.pointerDrag.name == "SmallCup") {
                cfm.setCup(CupType.SmallCup);
                // rend.sprite = smallcup;
            }
        }
    }

    public void reset() {
        cupObject.SetActive(false);
    }
}
