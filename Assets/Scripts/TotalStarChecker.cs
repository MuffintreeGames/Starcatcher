using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectStarEvent: UnityEvent
{

}
public class TotalStarChecker : MonoBehaviour
{
    public static CollectStarEvent CollectStar;

    public int starsInLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (CollectStar == null)
        {
            CollectStar = new CollectStarEvent();
        }
        CollectStar.AddListener(CheckForLevelEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckForLevelEnd()
    {
        starsInLevel--;
        if (starsInLevel <= 0)
        {
            Debug.Log("Level done!");
        }
    }
}
