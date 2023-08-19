using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshowerController : MonoBehaviour
{

    public GameObject star;

    static float timeBetweenStars = 0.5f;
    //static float offScreenX = 10.1f;
    static float minX = -4f;
    static float maxX = 15.4f;
    static float Y = 6f;

    float timeLeft = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            timeLeft = timeBetweenStars;
            SpawnStar();
        } else
        {
            timeLeft -= Time.deltaTime;
        }
    }

    void SpawnStar()
    {
        float randomX = Random.Range(minX, maxX);
        GameObject newStar = Instantiate(star, new Vector2(randomX, Y), Quaternion.identity);
        newStar.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2f, -1f), ForceMode2D.Impulse);
        newStar.GetComponent<Rigidbody2D>().AddTorque(8f, ForceMode2D.Impulse);
    }
}
