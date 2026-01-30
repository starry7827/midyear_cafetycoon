using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;
    Vector2 moveInput;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
        if (moveInput != Vector2.zero) {
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", moveInput.x);
        }
        else {
            animator.SetBool("Moving", false);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
}
