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
    [SerializeField] public CoffeeMachineLogic cfm;
    // [SerializeField] private Script coffeeMachine;

    void Start() {
        rend = cupObject.GetComponent<Image>();
    }

    public void OnDrop(PointerEventData data) {
        if (data.pointerDrag != null) {
            Debug.Log("Dropped Object was: " + data.pointerDrag.name);
            cupObject.SetActive(true);
            cfm.tryEnable();
            if (data.pointerDrag.name == "HotCup") {
                cfm.setCup(CupType.HotCup);
                rend.sprite = hotcup;
            } if (data.pointerDrag.name == "ColdCup") {
                cfm.setCup(CupType.ColdCup);
                rend.sprite = coldcup;
            }
        }
    }

    public void reset() {
        cupObject.SetActive(false);
    }
}
