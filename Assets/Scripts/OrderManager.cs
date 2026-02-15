using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public NPCManager npcManager;
    public Transform player;

    public KeyCode key = KeyCode.E;

    public Transform boxCenter;
    public float boxWidth = 15f;
    public float boxHeight = 10f;

    private NPCOrderPopUpController currentPopup;

    void Update()
    {
        if (npcManager == null || player == null || boxCenter == null) return;

        Vector2 p = player.position;
        Vector2 c = boxCenter.position;

        float halfW = boxWidth / 2f;
        float halfH = boxHeight / 2f;

        bool playerInBox =
            p.x >= c.x - halfW && p.x <= c.x + halfW &&
            p.y >= c.y - halfH && p.y <= c.y + halfH;

        // If player isn't in the box OR npc isn't ready -> hide popup
        if (!playerInBox || !npcManager.npcReady())
        {
            if (currentPopup != null)
            {
                currentPopup.HidePopup();
                currentPopup = null;
            }
            return;
        }
        NPCMovement front = npcManager.GetFrontNPC();
        if (front == null) return;

        NPCOrderPopUpController popup = front.GetComponent<NPCOrderPopUpController>();
        if (popup == null) return;

        // Only show if this is a new front NPC
        if (popup != currentPopup)
        {
            if (currentPopup != null) currentPopup.HidePopup();
            currentPopup = popup;
            currentPopup.ShowTakeOrder();
        }

        if (Input.GetKeyDown(key))
        {
            npcManager.takeNPCOrder();

            // hide immediately after taking order
            if (currentPopup != null)
            {
                currentPopup.HidePopup();
                currentPopup = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (boxCenter == null) return;

        Gizmos.color = Color.yellow;
        Vector3 center = boxCenter.position;
        Vector3 size = new Vector3(boxWidth, boxHeight, 0.1f);
        Gizmos.DrawWireCube(center, size);
    }
}
