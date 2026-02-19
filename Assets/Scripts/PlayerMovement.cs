using UnityEngine;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;
    Vector2 moveInput;
    public Animator animator;
    public SpriteRenderer rend;
    private IInteractable currentInteractable;
    private bool npcinteractable = false;
    private NPCOrder npcOrder;
    public Tray trayScript;
    [SerializeField] private Collider2D interactionTrigger;
    [SerializeField] private Collider2D physics;
    // private GameObject interactable;

    public static PlayerMovement Instance;

    void Awake()
    {
        Instance = this;
    }

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
        if (moveInput.x > 0)
        {
            trayScript.SetDirection(true);
        }
        else if (moveInput.x < 0)
        {
            trayScript.SetDirection(false);
        }
        if (animator != null)
        {
            if (moveInput != Vector2.zero)
            {
                animator.SetBool("Moving", true);
                animator.SetFloat("MoveX", moveInput.x);
                animator.SetFloat("MoveY", moveInput.y);

            }
            else
            {
                animator.SetBool("Moving", false);
            }
        }
        if (currentInteractable != null)
        {
            KeyCode key = currentInteractable.GetInteractKey();
            if (Input.GetKeyDown(key))
            {
                Debug.Log("trying to interact: " + currentInteractable);
                currentInteractable.Interact();
            }
        }
        if (npcinteractable)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                NPCManager.Instance.DeliverToNPC(npcOrder);
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

        npcOrder = other.GetComponent<NPCOrder>(); // i hope i didnt f this up but basically it checks if it has an order and if it doesnt it goes on to the other part that u had to differenciate npc interaction and the other stuff

        if (npcOrder != null && npcOrder.orderTaken)
        {
            Debug.Log("Click E to deliver order to customer!!!!!!!");
            npcinteractable = true;
            NPCOrderPopUpController popup = other.GetComponent<NPCOrderPopUpController>();
            popup.Display();
        }


        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null || !interactable.CanInteract()) return;

        currentInteractable = interactable;
        currentInteractable.Glow();
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        NPCOrderPopUpController popup = other.GetComponent<NPCOrderPopUpController>();
        if (popup != null)
        {
            popup.HidePopup();
            npcOrder = null;
            npcinteractable = false;
        }
        if (!other.isTrigger || interactionTrigger.IsTouching(other)) return;
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
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
