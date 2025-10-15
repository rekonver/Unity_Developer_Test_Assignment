using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float coyoteTime = 0.12f;

    [Header("References")]
    [SerializeField] private MonoBehaviour inputProviderComponent;

    private IPlayerInput input;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float linearDrag = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private int maxAirJumps = 0;
    [SerializeField] private float fallGravityMultiplier = 2f;
    [SerializeField] private float jumpCutGravityMultiplier = 3f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.12f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Forgiveness")]

    [SerializeField] private float jumpBufferTime = 0.12f;

    private bool isGrounded;
    private float lastGroundedTime;
    private float lastJumpInputTime;
    private int jumpsUsed;

    private float targetVelocityX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (inputProviderComponent != null && inputProviderComponent is IPlayerInput)
            input = inputProviderComponent as IPlayerInput;
        else
            input = GetComponentInChildren<IPlayerInput>();

        if (input == null)
            Debug.LogWarning("PlayerController: Input provider not found.");
    }

    void Update()
    {
        isGrounded = groundCheck != null && Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            if (rb.linearVelocity.y <= 0f) jumpsUsed = 0;
        }


        if (input != null && input.LastJumpPressedTime > lastJumpInputTime)
        {
            lastJumpInputTime = input.LastJumpPressedTime;
        }

        float move = input != null ? input.Move : 0f;
        targetVelocityX = move * moveSpeed;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        HandleGravity();
    }

    private void HandleMovement()
    {
        float velocityX = rb.linearVelocity.x;
        float newVelocityX = Mathf.MoveTowards(velocityX, targetVelocityX, acceleration * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);

        if (Mathf.Approximately(targetVelocityX, 0f) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * (1f - linearDrag * Time.fixedDeltaTime), rb.linearVelocity.y);
        }
    }

    private void HandleJump()
    {
        bool canUseCoyote = Time.time - lastGroundedTime <= coyoteTime;
        bool hasJumpInput = Time.time - lastJumpInputTime <= jumpBufferTime;
        bool canJump = (isGrounded || canUseCoyote || jumpsUsed <= maxAirJumps);

        if (hasJumpInput && canJump)
        {
            ExecuteJump();
            lastJumpInputTime = float.NegativeInfinity;
        }
    }

    private void ExecuteJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpsUsed++;
    }

    private void HandleGravity()
    {
        if (input != null && !input.JumpHeld && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (jumpCutGravityMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallGravityMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}