using UnityEngine;

public class CoffeeMachine : MonoBehaviour, IInteractable
{
    public bool IsFull { get; private set; }
    public GameObject itemPrefab;
    [SerializeField] private int maxCoffee = 2;
    private int coffeeCount = 0;
    [SerializeField] private GameObject machineUI;
    private bool isOpened;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool CanInteract()
    {
        return coffeeCount < maxCoffee;
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
    }

    private bool OpenMachine() {
        machineUI.SetActive(true);
        return true;
    }

    private bool CloseMachine() {
        machineUI.SetActive(false);
        return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
