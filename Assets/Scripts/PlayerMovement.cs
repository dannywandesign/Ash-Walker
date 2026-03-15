using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 6f;
    public float jumpForce = 12f;
    public float attackDuration = 0.5f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;

    private float horizontal;
    private bool isGrounded;
    private bool isAttacking;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump Logic (Still allowed while attacking!)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("isJumping");
        }

        // Attack Logic
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }

        // --- MOVEMENT WHILE ATTACKING ---
        // We no longer wrap this in "if (!isAttacking)"
        // This allows Peter to flip and run even if he's swinging
        bool isMoving = Mathf.Abs(horizontal) > 0.1f;
        anim.SetBool("isRunning", isMoving);

        if (horizontal > 0) 
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (horizontal < 0) 
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    void FixedUpdate()
    {
        // --- REMOVED THE ATTACK LOCK ---
        // We removed the code that set velocity to 0 during an attack.
        // Now physics movement happens every frame regardless of animation state.
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    public GameObject attackHitbox; // This was missing!
    public AttackHitbox hitboxScript; // For the delayed kill logic

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        
        attackHitbox.SetActive(true);

        // Keep the box active for a short window
        yield return new WaitForSeconds(0.3f); 
        
        // Check for any enemies that entered during that window
        if (hitboxScript != null) hitboxScript.CheckForKills();

        yield return new WaitForSeconds(attackDuration - 0.3f);

        attackHitbox.SetActive(false);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}