using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;
    Vector2 moveInput;
    public Animator animator;
    public SpriteRenderer rend;
    private IInteractable currentInteractable;
    [SerializeField] private Collider2D interactionTrigger;
    [SerializeField] private Collider2D physics;
    // private GameObject interactable;

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
        if (animator != null) {
            if (moveInput != Vector2.zero) {
                animator.SetBool("Moving", true);
                animator.SetFloat("MoveX", moveInput.x);
                animator.SetFloat("MoveY", moveInput.y);
            }
            else {
                animator.SetBool("Moving", false);
            }
        }
        if (currentInteractable != null) {
            KeyCode key = currentInteractable.GetInteractKey();
            if (Input.GetKeyDown(key)) {
                Debug.Log("trying to interact: " + currentInteractable);
                currentInteractable.Interact();
            }
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
        if (!interactionTrigger.IsTouching(other) || !other.isTrigger || physics.IsTouching(other)) return;

        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null || !interactable.CanInteract()) return;

        currentInteractable = interactable;
        currentInteractable.Glow();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger || interactionTrigger.IsTouching(other)) return;
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null) {
            interactable.Close();
            interactable.Regular();
            if (interactable == currentInteractable)
            {
                currentInteractable = null;

            }
        } 
    }

    /* private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit called. currentInteractable = " + currentInteractable);
        if (currentInteractable == null) return;
        if (interactionTrigger.IsTouching(other)) return;

        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != currentInteractable) return;

        Debug.Log("Exit called. currentInteractable = " + currentInteractable);
        currentInteractable.Close();
        currentInteractable.Regular();
        currentInteractable = null;
    } */
}
