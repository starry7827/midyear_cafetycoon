using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private Button board;
    [SerializeField] private Button lights;
    [SerializeField] private GameObject boardUp;
    [SerializeField] private GameObject lightsUp;
    private PlayerManager pm;
    private bool isOpened;
    private bool u1p;
    private bool u2p;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = PlayerManager.Instance;
        upgradeMenu.SetActive(false);
        board.interactable = false;
        lights.interactable = false;
        u1p = false;
        u2p = false;
        pm.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        menu();
    }

    public void interact() {
        if (!isOpened) {
            upgradeMenu.SetActive(true);
            isOpened = true;
        }
        else {
            upgradeMenu.SetActive(false);
            isOpened = false;
        }
    }

    private void menu() {
        // Debug.Log("money: " + pm.money);
        if (pm.money >= 300 && !u1p) {
            board.interactable = true;
        } else {
            board.interactable = false;
        }

        if (pm.money >= 700 && !u2p) {
            lights.interactable = true;
        } else {
            lights.interactable = false;
        }
    }

    public void upgradeBoard() {
        boardUp.SetActive(true);
        board.interactable = false;
        u1p = true;
    }

    public void upgradeLights() {
        lightsUp.SetActive(true);
        lights.interactable = false;
        u2p = true;
    }
}
