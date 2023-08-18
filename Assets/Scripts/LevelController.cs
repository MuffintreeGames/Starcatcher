using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FailLevelEvent : UnityEvent <string>
{
}

public class LevelController : MonoBehaviour
{
    public static FailLevelEvent failLevelEvent;
    public static float resetTime = 1.5f;
    public static bool levelFailed = false;

    public AudioSource levelFail;

    float resetTimeLeft = 10000f;
    bool resetting = false;
    //AsyncOperation asyncLoad;
    // Start is called before the first frame update
    void Start()
    {
        if (failLevelEvent == null) { failLevelEvent = new FailLevelEvent();}
        failLevelEvent.AddListener(FailLevel);
        levelFailed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (resetting)
        {
            resetTimeLeft -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Reset") && !TotalStarChecker.levelCleared)
        {
            resetTimeLeft = 0f;
            StartCoroutine(ResetLevel());
        }

        if (Input.GetButtonDown("Cancel") && !TotalStarChecker.levelCleared)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void FailLevel(string message)
    {
        if (!resetting && !TotalStarChecker.levelCleared) //avoid failing level more than once
        {
            levelFailed = true;
            Debug.Log("Failed level: " + message);
            levelFail.Play();
            resetTimeLeft = resetTime;
            resetting = true;
            StartCoroutine(ResetLevel());
        }
    }

    IEnumerator ResetLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (resetTimeLeft <= 0f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
