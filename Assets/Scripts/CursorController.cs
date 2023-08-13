using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static float movementSpeed = 5f;
    public GameObject Cursor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        float horDirection = Input.GetAxis("Horizontal");
        float verDirection = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horDirection, verDirection, 0) * movementSpeed * Time.deltaTime;
        transform.position += movementDirection;

        if (transform.position.x > 2.75f)
        {
            transform.position = new Vector3(2.75f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -2.75f)
        {
            transform.position = new Vector3(-2.75f, transform.position.y, transform.position.z);
        }
        if (transform.position.y > 1.75f)
        {
            transform.position = new Vector3(transform.position.x, 1.75f, transform.position.z);
        }
        if (transform.position.y < -3.75f)
        {
            transform.position = new Vector3(transform.position.x, -3.75f, transform.position.z);
        }
    }
}
