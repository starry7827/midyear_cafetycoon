using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
{
    public SpriteRenderer rend;
    public Sprite trashcan;
    public Sprite glowing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact() {
        PlayerManager pm = PlayerManager.Instance;
        if (pm == null)
        {
            Debug.Log("The PlayerManager is missing!");
        }
        pm.ClearInventory();
        Debug.Log("Inventory Cleared!");
    }

    public void Glow() {
        if (glowing != null) {
            rend.sprite = glowing;
        }
    }

    public void Regular() {
        if (trashcan != null) {
            rend.sprite = trashcan;
        }
    }

    public bool CanInteract() {
        return true;
    }

    public void Close() {
        if (trashcan != null)
            rend.sprite = trashcan;

    }

    public KeyCode GetInteractKey() {
        return KeyCode.T;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
