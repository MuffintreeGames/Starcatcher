using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSfx : MonoBehaviour
{
    public AudioSource highlightSound;
    public AudioSource selectSound;
    public AudioSource levelWin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonHighlighted()
    {
        highlightSound.Play();
    }

    public void ButtonSelected()
    {
        selectSound.Play();
    }

    public void LevelWin()
    {
        if (levelWin != null) levelWin.Play();
    }
}
