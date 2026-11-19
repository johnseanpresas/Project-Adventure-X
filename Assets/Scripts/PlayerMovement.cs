
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontal = 0f;

        // Detect A/D or Left/Right arrow keys
#if ENABLE_INPUT_SYSTEM
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed ||
                keyboard.leftArrowKey.isPressed)
                horizontal -= 1f;

            if (keyboard.dKey.isPressed ||
                keyboard.rightArrowKey.isPressed)
                horizontal += 1f;
        }
#else
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow))
            horizontal -= 1f;

        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
            horizontal += 1f;
#endif

        // Switch between Idle and Running
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Make the Knight face the movement direction
        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0;
        }
    }

    void FixedUpdate()
    {
        // Move the Knight horizontally
        rb.linearVelocity = new Vector2(
            horizontal * moveSpeed,
            rb.linearVelocity.y
        );
    }
}
