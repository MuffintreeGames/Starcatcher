using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : Crushable
{
    static float maxHorizontalSpeed = 6f;
    static float jumpForce = 14f;
    static float jumpCooldown = 0.2f;
    static float toggleCooldown = 0.2f;
    static float timeUntilPoofOut = 0.8f;

    public AudioSource jumpSound;
    public AudioSource poofSound;
    public LayerMask floorLayers;
    public Transform rightFoot;
    public Transform leftFoot;
    public Transform center;
    public PhysicsMaterial2D noFrictionMaterial;
    public GameObject particle;

    Rigidbody2D rb;
    //bool justJumped = false;
    float remainingJumpCooldown = 0f;
    float remainingPoofOutTime = 0f;
    Animator animator;
    SpriteRenderer sprite;

    bool inputsDisabled = false;
    bool controlling;   //true when controlling character, false when controlling black/white hole
    bool justToggled = false;
    float remainingToggleCooldown = 0f;
    bool groundedLastFrame = true;  //used to check if we landed this frame
    bool hasWonOrLost = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        controlling = true;
        inputsDisabled = false;
        Physics2D.queriesHitTriggers = false;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(rightFoot.position, -Vector3.up, 0.1f, floorLayers.value) || Physics2D.Raycast(leftFoot.position, -Vector3.up, 0.1f, floorLayers.value) || Physics2D.Raycast(center.position, -Vector3.up, 0.1f, floorLayers.value);
    }

    void IsRidingWhiteHole()
    {
        WhiteHoleController whiteHole = null;   //if we're standing on a white hole, set this to that white hole and get its velocity
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, -Vector3.up, 0.2f, floorLayers.value);
        if (rightHit.collider != null)
        {
            whiteHole = rightHit.collider.GetComponent<WhiteHoleController>();
        }

        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, -Vector3.up, 0.2f, floorLayers.value);
        if (leftHit.collider != null)
        {
            whiteHole = leftHit.collider.GetComponent<WhiteHoleController>();
        }

        RaycastHit2D centerHit = Physics2D.Raycast(center.position, -Vector3.up, 0.2f, floorLayers.value);
        if (centerHit.collider != null)
        {
            whiteHole = centerHit.collider.GetComponent<WhiteHoleController>();
        }

        if (whiteHole != null)
        {
            Debug.Log("I am standing on a white hole!");
            transform.position += whiteHole.GetMovementDirection();
        }
    }

    private void Update()
    {
        if (LevelController.levelFailed || TotalStarChecker.levelCleared)
        {
            inputsDisabled = true;
        }

        if (remainingPoofOutTime > 0)
        {
            remainingPoofOutTime -= Time.deltaTime;
            if (remainingPoofOutTime <= 0)
            {
                PoofOut();
            }
        }

        if (!hasWonOrLost)
        {
            if (LevelController.levelFailed)
            {
                hasWonOrLost = true;
                animator.SetBool("lost", true);
            }
            else if (TotalStarChecker.levelCleared)
            {
                hasWonOrLost = true;
                Debug.Log("player has won level! Dance time!");
                animator.SetBool("won", true);
                remainingPoofOutTime = timeUntilPoofOut;
            }
        } else
        {
            animator.SetBool("lost", false);
            animator.SetBool("won", false);
        }

        if (justToggled)
        {
            remainingToggleCooldown -= Time.deltaTime;
            if (remainingToggleCooldown < 0f)
            {
                justToggled = false;
            }
        }

        if (!inputsDisabled && Input.GetButtonDown("ToggleControl") && !justToggled && (IsGrounded() || !controlling))
        {
            Debug.Log("toggling control");
            justToggled = true;
            remainingToggleCooldown = toggleCooldown;
            ToggleController.toggleEvent.Invoke();
        }

        if (!LevelController.levelFailed && !TotalStarChecker.levelCleared)
        {
            IsRidingWhiteHole();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LevelController.levelFailed || TotalStarChecker.levelCleared)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.gravityScale = 0;
            //animator.enabled = false;
            return;
        }

        if (!controlling || inputsDisabled)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        bool grounded = IsGrounded();
        animator.SetBool("grounded", grounded);
        float direction = Input.GetAxis("Horizontal");
        if (direction != 0)
        {
            rb.velocity = new Vector2(maxHorizontalSpeed * direction, rb.velocity.y);
            if (direction > 0)
            {
                sprite.flipX = false;
            } else
            {
                sprite.flipX = true;
            }
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        animator.SetFloat("horizontalSpeed", Mathf.Abs(direction));

        if (Input.GetAxis("Jump") > 0 && grounded && groundedLastFrame)
        {
            Debug.Log("trying to jump");
            if (grounded)
            {
                Debug.Log("jumping");
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                remainingJumpCooldown = jumpCooldown;
                //justJumped = true;
                animator.SetBool("grounded", false);
                jumpSound.Play();
            }
        }
        animator.SetFloat("verticalSpeed", rb.velocity.y);
        groundedLastFrame = grounded;
    }


    public void ActivatePlayer()
    {
        controlling = true;
        animator.SetBool("inactive", false);
        rb.sharedMaterial = noFrictionMaterial;
    }

    public void DeactivatePlayer()
    {
        controlling = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("inactive", true);
        rb.sharedMaterial = null;
    }

    public override void Kill()
    {
        LevelController.failLevelEvent.Invoke("Player died!");
        Destroy(gameObject);
    }

    void PoofOut()
    {
        sprite.enabled = false;
        poofSound.Play();

        GameObject leftParticle = GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        leftParticle.GetComponent<Rigidbody2D>().AddForce(new Vector3(-100f, 200f, 0f));
        leftParticle.GetComponent<Rigidbody2D>().AddTorque(20f);

        GameObject rightParticle = GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        rightParticle.GetComponent<Rigidbody2D>().AddForce(new Vector3(100f, 200f, 0f));
        rightParticle.GetComponent<Rigidbody2D>().AddTorque(20f);
    }
}
