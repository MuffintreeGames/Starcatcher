using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public static float movementSpeed = 5f;
    public static float upperBound = 4.55f;
    public static float lowerBound = -4.65f;
    public static float leftBound = -8.5f;
    public static float rightBound = 8.5f;

    Animator animator;
    bool controlling = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlling && !LevelController.levelFailed)
        {
            float horDirection = Input.GetAxis("Horizontal");
            float vertDirection = Input.GetAxis("Vertical");
            Vector3 movementDirection = new Vector3(horDirection, vertDirection, 0) * movementSpeed * Time.deltaTime;

            if (movementDirection.y + transform.position.y > upperBound)
            {
                movementDirection.y = upperBound - transform.position.y;
            } else if (movementDirection.y + transform.position.y < lowerBound)
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
