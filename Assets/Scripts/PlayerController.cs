using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Crushable
{
    static float maxHorizontalSpeed = 6f;
    static float jumpForce = 20f;
    static float jumpCooldown = 0.2f;
    static float toggleCooldown = 0.2f;

    public LayerMask floorLayers;
    public Transform rightFoot;
    public Transform leftFoot;
    public Transform center;
    public PhysicsMaterial2D noFrictionMaterial;

    Rigidbody2D rb;
    bool justJumped = false;
    float remainingJumpCooldown = 0f;
    Animator animator;
    SpriteRenderer sprite;

    bool controlling;   //true when controlling character, false when controlling black/white hole
    bool justToggled = false;
    float remainingToggleCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        controlling = true;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(rightFoot.position, -Vector3.up, 0.1f, floorLayers.value) || Physics2D.Raycast(leftFoot.position, -Vector3.up, 0.1f, floorLayers.value) || Physics2D.Raycast(center.position, -Vector3.up, 0.1f, floorLayers.value);
    }

    private void Update()
    {
        if (justJumped)
        {
            remainingJumpCooldown -= Time.deltaTime;
            if (remainingJumpCooldown < 0f)
            {
                justJumped = false;
            }
        }

        if (justToggled)
        {
            remainingToggleCooldown -= Time.deltaTime;
            if (remainingToggleCooldown < 0f)
            {
                justToggled = false;
            }
        }

        if (Input.GetAxis("ToggleControl") > 0 && !justToggled)
        {
            Debug.Log("toggling control");
            justToggled = true;
            remainingToggleCooldown = toggleCooldown;
            if (controlling)
            {
                controlling = false;
                animator.SetBool("inactive", true);
            }
            ToggleController.toggleEvent.Invoke();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!controlling)
        {
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

        if (Input.GetAxis("Jump") > 0 && grounded && !justJumped)
        {
            Debug.Log("jumping");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            remainingJumpCooldown = jumpCooldown;
            justJumped = true;
            animator.SetBool("grounded", false);
        }
        animator.SetFloat("verticalSpeed", rb.velocity.y);
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

    public override void Crush()
    {
        Debug.Log("player is crushed, level failed");
        Destroy(gameObject);
    }
}
