using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    [Header("UI & Canvas References")]
    public GameObject popupCanvas;      
    public TextMeshProUGUI notificationText; 
    public TextMeshProUGUI dashText;
    public TextMeshProUGUI glideText;
    public TextMeshProUGUI doubleJumpText;

    [Header("Gamble Settings")]
    [Range(0f, 1f)] public float abilityChance = 0.5f;

    [Header("Temporary Debuffs")]
    public float tempDebuffDuration = 5f;
    public float tempSpeedMultiplier = 0.6f;
    public float tempJumpMultiplier = 0.7f;
    public float tempGravityIncrease = 1.5f;
    public Color debuffColor = new Color(0.4f, 0.4f, 0.4f);

    [Header("Permanent Buffs")]
    public float permSpeedBoost = 0.1f;        
    public float permJumpBoost = 0.25f;         
    public float permDashBoost = 1.0f;         
    public float permGravityDecrease = 0.05f;  

    [Header("Base Stats")]
    public float baseSpeed;
    public float baseJump;
    public float baseGravity;

    [Header("State")]
    public bool isDashing = false;
    public int jumpsRemaining;
    private int activeGrowthCycles = 0; 

    [Header("Abilities Status")]
    public bool canDash = false;
    public bool canGlide = false;
    public bool canDoubleJump = false;

    [Header("Physics")]
    public float dashForce = 20f;
    public float dashTime = 0.2f;
    public float glideDrag = 10f;
    private float originalDrag;

    private PlayerMovement movement;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Awake() { instance = this; }

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
        baseSpeed = movement.speed;
        baseJump = movement.jumpForce;
        baseGravity = rb.gravityScale;
        originalDrag = rb.linearDamping;

        if (popupCanvas != null) popupCanvas.SetActive(false);
    }

    void Update()
    {
        if (movement.isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            jumpsRemaining = canDoubleJump ? 2 : 1;
        }

        if (!isDashing)
        {
            HandleDash();
            HandleGlide();
            HandleJump();
        }
    }

    public void GrowPlayer()
    {
        baseSpeed += permSpeedBoost;
        baseJump += permJumpBoost;
        dashForce += permDashBoost;
        baseGravity = Mathf.Max(0.5f, baseGravity - permGravityDecrease);

        string unlockedAbilityName = "";
        if (Random.value <= abilityChance)
        {
            unlockedAbilityName = TryUnlockRandomAbility();
        }

        StartCoroutine(EvolutionCycle(unlockedAbilityName));
    }

    private string TryUnlockRandomAbility()
    {
        List<string> locked = new List<string>();
        if (!canDash) locked.Add("Dash");
        if (!canGlide) locked.Add("Glide");
        if (!canDoubleJump) locked.Add("Double Jump");

        if (locked.Count > 0)
        {
            string picked = locked[Random.Range(0, locked.Count)];

            if (picked == "Dash") { canDash = true; if(dashText) dashText.color = Color.green; }
            else if (picked == "Glide") { canGlide = true; if(glideText) glideText.color = Color.green; }
            else if (picked == "Double Jump") { canDoubleJump = true; if(doubleJumpText) doubleJumpText.color = Color.green; }
            
            return picked;
        }
        return "";
    }

    IEnumerator EvolutionCycle(string unlockedAbility)
    {
        activeGrowthCycles++;
        
        if (popupCanvas != null) popupCanvas.SetActive(true);

        movement.speed = baseSpeed * tempSpeedMultiplier;
        movement.jumpForce = baseJump * tempJumpMultiplier;
        rb.gravityScale = baseGravity + tempGravityIncrease;
        sr.color = debuffColor;

        if (notificationText != null)
        {
            notificationText.color = Color.red;
            notificationText.text = "GROWING PAINS...\nYou feel heavy.";
        }

        yield return new WaitForSeconds(tempDebuffDuration);

        activeGrowthCycles--;

        if (activeGrowthCycles <= 0)
        {
            activeGrowthCycles = 0;
            movement.speed = baseSpeed;
            movement.jumpForce = baseJump;
            rb.gravityScale = baseGravity;
            sr.color = Color.white;
        }

        if (notificationText != null)
        {
            notificationText.color = Color.cyan;
            string resultMsg = "MATURED!\nStats Permanently Up";

            if (!string.IsNullOrEmpty(unlockedAbility))
            {
                notificationText.color = Color.green; 
                resultMsg += "\nUNLOCKED: " + unlockedAbility.ToUpper() + "!";
            }

            notificationText.text = resultMsg;

            yield return new WaitForSeconds(3f);
            
            if (activeGrowthCycles <= 0)
            {
                popupCanvas.SetActive(false);
            }
        }
    }

    void HandleJump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")) && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, movement.jumpForce);
            if(anim) anim.SetTrigger("isJumping");
        }
    }

    void HandleDash()
    {
        if (canDash && Input.GetKeyDown(KeyCode.Q) && !isDashing)
        {
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        isDashing = true;
        float dashDir = transform.localScale.x;
        rb.linearVelocity = new Vector2(dashDir * dashForce, 0);
        float currentGrav = rb.gravityScale;
        rb.gravityScale = 0; 
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = currentGrav; 
        isDashing = false;
    }

    void HandleGlide()
    {
        if (canGlide && Input.GetKey(KeyCode.Space) && rb.linearVelocity.y < 0)
        {
            rb.linearDamping = glideDrag;
        }
        else
        {
            rb.linearDamping = originalDrag;
        }
    }
}