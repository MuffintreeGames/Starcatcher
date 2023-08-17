using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : Crushable
{
    public LayerMask floorLayers;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsRidingWhiteHole();
    }

    void IsRidingWhiteHole()
    {
        WhiteHoleController whiteHole = null;   //if we're standing on a white hole, set this to that white hole and get its velocity
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1.3f, floorLayers.value);
        if (hit.collider != null)
        {
            whiteHole = hit.collider.GetComponent<WhiteHoleController>();
        }

        if (whiteHole != null)
        {
            Debug.Log("star is sitting on a white hole!");
            transform.position += whiteHole.GetMovementDirection();
        }
    }


    public override void Kill()
    {
        LevelController.failLevelEvent.Invoke("Star lost!");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player hit star!");
            TotalStarChecker.CollectStar.Invoke();
            GameObject spawnedParticle = Instantiate(particle, transform.position, Quaternion.identity);
            spawnedParticle.GetComponent<Rigidbody2D>().AddForce(new Vector3(100f, 200f, 0f));
            spawnedParticle.GetComponent<Rigidbody2D>().AddTorque(20f);
            Destroy(gameObject);
        }
    }
}
