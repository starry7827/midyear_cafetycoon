using UnityEngine;

public class OrderManager : MonoBehaviour, IInteractable
{
    public NPCManager npcManager;
    public KeyCode key = KeyCode.E;

    public bool CanInteract()
    {
        if (npcManager == null) return false;
        return npcManager.npcReady();
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        npcManager.takeNPCOrder();
    }

    public KeyCode GetInteractKey() => key;

    public void Glow() { }
    public void Regular() { }
    public void Close() { }
}
