using UnityEngine;

public class CounterDoor : MonoBehaviour, IInteractable
{
    public SpriteRenderer rend;
    public Sprite openDoor;
    public Sprite closedDoor;
    public GameObject collision1;
    public GameObject collision2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact() {
        changeSprite();
        if (collision1 != null && collision2 != null) {
            collision1.SetActive(false);
            collision2.SetActive(false);
        }
    }

    public void changeSprite() {
        if (rend != null && openDoor != null) {
            rend.sprite = openDoor;
        }
    }

    public bool CanInteract() {
        return true;
    }

    public void Close() {
        if (closedDoor != null) {
            rend.sprite = closedDoor;
            collision1.SetActive(true);
            collision2.SetActive(true);
        }

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
