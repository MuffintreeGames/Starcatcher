using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public static float movementSpeed = 5f;

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
        if (controlling)
        {
            float horDirection = Input.GetAxis("Horizontal");
            float vertDirection = Input.GetAxis("Vertical");
            Vector3 movementDirection = new Vector3(horDirection, vertDirection, 0) * movementSpeed * Time.deltaTime;
            transform.position += movementDirection;
        }
    }

    public void ActivateBlackHole()
    {
        Debug.Log("black hole received activation message");
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
