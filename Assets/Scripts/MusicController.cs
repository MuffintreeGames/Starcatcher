using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    int currentMusicChoice = 0; //set to 0 to play menu music, 1 for level music, or 2 for constellation music

    public AudioSource menuMusic;
    public AudioSource levelMusic;
    public AudioSource constellationMusic;

    AudioSource currentAudioSource;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += UpdateMusic;
        UpdateMusic(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());  //call once immediately to set music for current scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateMusic(Scene current, Scene next)
    {
        int newMusicChoice;
        if (next.name.Contains("Level") && next.name != "LevelSelect")
        {
            Debug.Log("entering level, should play level music");
            newMusicChoice = 1;
        } else if (next.name.Contains("Constellation"))
        {
            Debug.Log("entering constellation, should play constellation music");
            newMusicChoice = 2;
        } else
        {
            Debug.Log("exhausted other options, play menu music");
            newMusicChoice = 0;
        }

        if (currentAudioSource != null)
        {
            if (currentMusicChoice == newMusicChoice)
            {
                return;
            } else
            {
                currentAudioSource.Stop();
            }
        }

        switch (newMusicChoice)
        {
            case 0: currentAudioSource = menuMusic; break;
            case 1: currentAudioSource = levelMusic; break;
            case 2: currentAudioSource = constellationMusic; break;
        }
        currentMusicChoice = newMusicChoice;
        currentAudioSource.Play();
    }
}
