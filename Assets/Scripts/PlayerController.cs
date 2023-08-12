using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static float maxHorizontalSpeed = 6f;
    static float jumpForce = 20f;
    static float jumpCooldown = 0.2f;

    public LayerMask floorLayers;
    public Transform feet;
    Rigidbody2D rb;
    bool justJumped = false;
    float remainingJumpCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(feet.position, -Vector3.up, 0.1f, floorLayers.value);
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal");
        if (direction != 0)
        {
            rb.velocity = new Vector2(maxHorizontalSpeed * direction, rb.velocity.y);
        }

        Debug.Log("grounded: " + IsGrounded());
        if (Input.GetAxis("Jump") > 0 && IsGrounded() && !justJumped)
        {
            Debug.Log("jumping");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            remainingJumpCooldown = jumpCooldown;
            justJumped = true;
        }
    }
}
