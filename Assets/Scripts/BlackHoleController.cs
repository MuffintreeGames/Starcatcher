using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackHoleController : MonoBehaviour
{
    public static float movementSpeed = 5f;
    public static float upperBound = 4.55f;
    public static float lowerBound = -4.65f;
    public static float leftBound = -8.5f;
    public static float rightBound = 8.5f;
    public static float distanceToCenter = 0.5f;
    public static float marginForError = 0.00001f;

    CircleCollider2D collider;
    Animator animator;
    bool controlling = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlling && !LevelController.levelFailed && !TotalStarChecker.levelCleared)
        {
            float horDirection = Input.GetAxis("Horizontal");
            float vertDirection = Input.GetAxis("Vertical");

            Physics2D.queriesHitTriggers = true;    //temporarily activate ability to hit triggers, turn it off again by end of function

            float horDistance = movementSpeed * Time.deltaTime * horDirection;
            float vertDistance = movementSpeed * Time.deltaTime * vertDirection;

            RaycastHit2D rightHit = Physics2D.Raycast((Vector2)transform.position + collider.offset, Vector2.right, movementSpeed, 1 << LayerMask.NameToLayer("Nogo"));
            if (rightHit)
            {
                if ((rightHit.distance - (distanceToCenter * transform.lossyScale.x)) < Mathf.Max(horDistance, 0))
                {
                    horDistance = rightHit.distance - ((distanceToCenter + marginForError) * transform.lossyScale.x);
                }
            }

            RaycastHit2D leftHit = Physics2D.Raycast((Vector2)transform.position + collider.offset, Vector2.left, movementSpeed, 1 << LayerMask.NameToLayer("Nogo"));
            if (leftHit)
            {
                if ((leftHit.distance - (distanceToCenter * transform.lossyScale.x)) < Mathf.Abs(Mathf.Min(horDistance, 0)))
                {
                    horDistance = (leftHit.distance - ((distanceToCenter + marginForError) * transform.lossyScale.x)) * -1;
                }
            }

            RaycastHit2D upHit = Physics2D.Raycast((Vector2)transform.position + collider.offset, Vector2.up, movementSpeed, 1 << LayerMask.NameToLayer("Nogo"));
            if (upHit)
            {
                if ((upHit.distance - (distanceToCenter * transform.lossyScale.y)) < Mathf.Max(0, vertDistance))
                {
                    vertDistance = upHit.distance - ((distanceToCenter + marginForError) * transform.lossyScale.y);
                }
            }

            RaycastHit2D downHit = Physics2D.Raycast((Vector2)transform.position + collider.offset, Vector2.down, movementSpeed, 1 << LayerMask.NameToLayer("Nogo"));
            if (downHit)
            {
                if ((downHit.distance - (distanceToCenter * transform.lossyScale.y)) < Mathf.Abs(Mathf.Min(vertDistance, 0)))
                {
                    vertDistance = (downHit.distance - ((distanceToCenter + marginForError) * transform.lossyScale.y)) * -1;
                }
            }

            Physics2D.queriesHitTriggers = false;

            Vector3 movementDirection = new Vector3(horDistance, vertDistance, 0);

            if (movementDirection.y + transform.position.y > upperBound)
            {
                movementDirection.y = upperBound - transform.position.y;
            }
            else if (movementDirection.y + transform.position.y < lowerBound)
            {
                movementDirection.y = lowerBound - transform.position.y;
            }

            if (movementDirection.x + transform.position.x > rightBound)
            {
                movementDirection.x = rightBound - transform.position.x;
            }
            else if (movementDirection.x + transform.position.x < leftBound)
            {
                movementDirection.x = leftBound - transform.position.x;
            }
            transform.position += movementDirection;
        }
    }

    public void ActivateBlackHole()
    {
        controlling = true;
        animator.SetBool("active", true);
    }

    public void DeactivateBlackHole()
    {
        controlling = false;
        animator.SetBool("active", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("blackhole has detected trigger: " + collision.tag);
        if (collision.tag == "Particle")
        {
           // Debug.Log("disabling a particle");
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Particle")
        {
           // Debug.Log("re-enabling a particle");
            collision.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
