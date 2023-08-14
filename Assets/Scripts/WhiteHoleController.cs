using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHoleController : MonoBehaviour
{
    public static float movementSpeed = 5f;

    bool controlling;
    Animator animator;
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

    public void ActivateWhiteHole()
    {
        Debug.Log("white hole received activation message");
        controlling = true;
        animator.SetBool("active", true);
    }

    public void DeactivateWhiteHole()
    {
        controlling = false;
        animator.SetBool("active", false);
    }
}
