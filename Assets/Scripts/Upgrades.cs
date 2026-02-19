using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private Button board;
    private bool isOpened;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeMenu.SetActive(false);
        board.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        PlayerManager pm = PlayerManager.Instance;
        if (pm.money >= 300) {
            board.interactable = true;
        } else {
            board.interactable = false;
        }
    }
}
