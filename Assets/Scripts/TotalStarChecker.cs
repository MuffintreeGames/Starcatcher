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
    public static int firstLevelIndex = 3;
    public static float advanceTime = 1.5f;
    public static bool levelCleared = false;

    public AudioSource gotStar;
    public AudioSource levelWin;

    int starsInLevel = 1;

    float advanceTimeLeft = 10000f;
    bool advancing = false;
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
        levelCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (advancing)
        {
            advanceTimeLeft -= Time.deltaTime;
        }
    }

    void CheckForLevelEnd()
    {
        starsInLevel--;
        if (starsInLevel <= 0)
        {
            Debug.Log("Level done!");
            levelWin.Play();
            advanceTimeLeft = advanceTime;
            advancing = true;
            levelCleared = true;
            ProgressTracker.LevelCleared(SceneManager.GetActiveScene().buildIndex - firstLevelIndex);
            StartCoroutine(GoToNextLevel());
        } else
        {
            gotStar.Play();
        }
    }

    IEnumerator GoToNextLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (advanceTimeLeft <= 0f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
