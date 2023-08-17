using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLifetime : MonoBehaviour
{
    public float lifetime;

    float lifetimeLeft;
    // Start is called before the first frame update
    void Start()
    {
        lifetimeLeft = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeLeft -= Time.deltaTime;
        if (lifetimeLeft < 0)
        {
            Destroy(gameObject);
        }
    }
}
