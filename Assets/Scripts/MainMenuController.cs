using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currently selected object: " + EventSystem.current.currentSelectedGameObject);
    }

    public void LoadGame()
    {
        ProgressTracker.levelsClearedInt = 0;
        ConstellationController.ListConstellations = new();
        SceneManager.LoadScene("Level1");
    }

    public void LoadContinue()
    {
        int cleared = ProgressTracker.levelsClearedInt;
        if (cleared == 0)
        {
            SceneManager.LoadScene("Level1");
            return;
        }
        if (cleared < ProgressTracker.numOfLevels)
        {
            SceneManager.LoadScene("Level" + (cleared+1).ToString());
            return;
        }
        SceneManager.LoadScene("Ending");
    }

    public void LoadCredits()
    {

        SceneManager.LoadScene("Credits");
    }
}
