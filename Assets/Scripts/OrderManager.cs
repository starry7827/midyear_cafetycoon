using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public NPCManager npcManager;
    public Transform player;
    public KeyCode key = KeyCode.E;

    public Transform boxCenter;
    public float boxWidth = 15f;
    public float boxHeight = 10f;
    private float waitTimer = 0;
    private bool isWaiting = false;

    private NPCOrderPopUpController currentPopup;
    private bool orderTakenForCurrentNPC = false; // Tracks if we already pressed E

    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime; 
            if (waitTimer <= 0)
            {
                isWaiting = false;
                npcManager.takeNPCOrder();
            }
            return; 
        }

        if (npcManager == null || player == null || boxCenter == null) return;

        Vector2 p = player.position;
        Vector2 c = boxCenter.position;
        float halfW = boxWidth / 2f;
        float halfH = boxHeight / 2f;

        bool playerInBox = p.x >= c.x - halfW && p.x <= c.x + halfW &&
                           p.y >= c.y - halfH && p.y <= c.y + halfH;

        if (!playerInBox || !npcManager.npcReady())
        {
            if (currentPopup != null)
            {
                currentPopup.HidePopup();
                currentPopup = null;
                orderTakenForCurrentNPC = false; 
            }
            return;
        }

        NPCMovement front = npcManager.GetFrontNPC();
        if (front == null) return;

        NPCOrderPopUpController popup = front.GetComponent<NPCOrderPopUpController>();
        NPCOrder orderData = front.GetComponent<NPCOrder>();
        
        if (popup == null || orderData == null) return;

        if (popup != currentPopup)
        {
            if (currentPopup != null) currentPopup.HidePopup();
            currentPopup = popup;
            orderTakenForCurrentNPC = false;
            currentPopup.ShowTakeOrder();
        }

        if (Input.GetKeyDown(key) && !orderTakenForCurrentNPC)
        {
            orderTakenForCurrentNPC = true;
            currentPopup.ShowOrderIcons(orderData.drink, orderData.food);
            waitTimer = 2.0f; 
            isWaiting = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (boxCenter == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCenter.position, new Vector3(boxWidth, boxHeight, 0.1f));
    }
}