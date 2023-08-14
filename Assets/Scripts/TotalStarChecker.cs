using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollectStarEvent: UnityEvent
{

}
public class TotalStarChecker : MonoBehaviour
{
    public static CollectStarEvent CollectStar;
    public static int firstLevelIndex = 4;

    int starsInLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (CollectStar == null)
        {
            CollectStar = new CollectStarEvent();
        }
        CollectStar.AddListener(CheckForLevelEnd);

        GameObject[] starArray = GameObject.FindGameObjectsWithTag("Star");
        starsInLevel = starArray.Length;
        if (starsInLevel == 0)
        {
            Debug.LogError("No stars in level!");
        }
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
            ProgressTracker.LevelCleared(SceneManager.GetActiveScene().buildIndex - firstLevelIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
