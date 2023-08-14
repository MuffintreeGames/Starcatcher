using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPoint : MonoBehaviour
{
    public Crushable parent;
    public CrushPoint[] oppositePoints; 
    bool inContact = false;
    int contactingObjects = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetInContact() { return inContact; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            inContact = true;
            contactingObjects++;
            //CheckForCrush();
            if (collision.gameObject.name == "WhiteHole")
            {
                Debug.Log("hit by white hole!");
                CheckForCrush();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            contactingObjects--;
            if (contactingObjects == 0)
            {
                inContact = false;
            }
        }
    }
    private void CheckForCrush()
    {
        for (int i = 0; i < oppositePoints.Length; i++)
        {
            if (!oppositePoints[i].GetInContact())
            {
                return;
            }
        }
        Debug.Log(gameObject.name + " being crushed! Should kill object!");
        parent.Crush();
    }
}
