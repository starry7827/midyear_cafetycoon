using UnityEngine;

public class CoffeeMachine : MonoBehaviour, IInteractable
{
    public bool IsFull { get; private set; }
    public GameObject itemPrefab;
    private int coffeeCount = 0;
    private bool isOpened;
    public Sprite reg;
    public Sprite glow;
    public SpriteRenderer rend;
    private Transform trans;
    private CoffeeMachineLogic cfm;
    [SerializeField] private int maxCoffee = 2;
    [SerializeField] private GameObject machineUI;
    [SerializeField] private CupDrop cd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool CanInteract()
    {
        return coffeeCount < maxCoffee;
    }

    public void Glow() {
    if (glow != null) {
        Vector3 pos = trans.position;
        pos.x = -71.5f;
        trans.position = pos;

        rend.sprite = glow;
    }
}

public void Regular() {
    if (reg != null) {
        Vector3 pos = trans.position;
        pos.x = -72f;
        trans.position = pos;

        rend.sprite = reg;
    }
}


    public void Interact()
    {
        if (machineUI != null && !isOpened) {
            isOpened = OpenMachine();
        } else if (machineUI != null && isOpened) {
            isOpened = CloseMachine();
        }
    }

    public void Close() {
        if (machineUI != null && isOpened) {
            isOpened = CloseMachine();
        }
        cfm.reset();
        cd.reset();
    }

    private bool OpenMachine() {
        machineUI.SetActive(true);
        return true;
    }

    private bool CloseMachine() {
        machineUI.SetActive(false);
        return false;
    }

    public KeyCode GetInteractKey() {
        return KeyCode.C;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
        cfm = GetComponent<CoffeeMachineLogic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
