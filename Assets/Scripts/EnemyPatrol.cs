using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float jumpForce = 5f;
    public float hopDelay = 1.5f; 
    public float patrolDistance = 4f;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;

    private Vector3 startPos;
    private bool movingRight = true;
    private float nextHopTime;
    private Vector3 originalScale;

    void Start()
    {
        startPos = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        float rightBound = startPos.x + patrolDistance;
        float leftBound = startPos.x - patrolDistance;

        if (Time.time >= nextHopTime)
        {
            Hop();
            nextHopTime = Time.time + hopDelay;
        }

        if (movingRight && transform.position.x >= rightBound)
        {
            Flip();
        }
        else if (!movingRight && transform.position.x <= leftBound)
        {
            Flip();
        }

        if (rb.linearVelocity.y > 0.1f)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isFalling", false);
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
    }

    void Hop()
    {
        float direction = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * speed, jumpForce);
    }

    void Flip()
    {
        movingRight = !movingRight;
        float newX = movingRight ? originalScale.x : -originalScale.x;
        transform.localScale = new Vector3(newX, originalScale.y, originalScale.z);
    }
}