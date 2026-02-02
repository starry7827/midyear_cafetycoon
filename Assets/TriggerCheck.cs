using UnityEngine;

public class TriggerCheck : MonoBehaviour
{

    private IInteractable currentInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable != null && interactable.CanInteract())
        {
            currentInteractable = interactable;
            currentInteractable.Glow();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable != null && interactable == currentInteractable)
        {
            interactable.Close();
            interactable.Regular();
            currentInteractable = null;
        }
    }

}
