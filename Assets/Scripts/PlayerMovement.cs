using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;
    Vector2 moveInput;
    public Animator animator;
    public SpriteRenderer rend;
    private IInteractable currentInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
        if (moveInput != Vector2.zero) {
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
        }
        else {
            animator.SetBool("Moving", false);
        }
        if (Input.GetKeyDown(KeyCode.C) && currentInteractable != null) {
            Debug.Log("trying to interact: " + currentInteractable);
            currentInteractable.Interact();
        }
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null) {
            Debug.Log("trying to interact: " + currentInteractable);
            currentInteractable.Interact();
        }
    }

    /* void LateUpdate() {
        Vector3 worldPos = transform.position;
        if (worldPos.x <= -4 && worldPos.x >= -90 && worldPos.y > -9) {
            rend.sortingOrder = Mathf.RoundToInt((-worldPos.y+20) * 100);
        } else {
            rend.sortingOrder = 100;
        }

        //Order in Layer = -Y position * some number
    } */

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger) return;
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null && interactable.CanInteract())
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger) return;
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null) {
            interactable.Close();
            if (interactable == currentInteractable)
            {
                currentInteractable = null;

            }
        }
    }


}
