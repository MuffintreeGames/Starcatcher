using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : Crushable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Crush()
    {
        Debug.Log("star has been crushed, level failed!");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player hit star!");
            TotalStarChecker.CollectStar.Invoke();
            Destroy(gameObject);
        }
    }
}
