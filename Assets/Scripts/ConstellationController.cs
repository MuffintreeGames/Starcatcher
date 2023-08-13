using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ConstellationController : MonoBehaviour
{
    // Objects for Constellation Stars/Lines, Graph or Image.

    // UI
    public TextMeshProUGUI ConstellationName;
    public TextMeshProUGUI StarsLeft;
    public TextMeshProUGUI CreateStars;
    public TextMeshProUGUI CreateLines;
    public GameObject EditorArea;
    public GameObject Cursor;
    public GameObject Cursor2; // for Lines?
    public bool mode;
    public int starsMax;
    

    // Start is called before the first frame update
    void Start()
    {
        mode = true;
        CreateStars.enabled = false;
        starsMax = int.Parse(StarsLeft.text);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCursor();
        if (Input.GetKeyDown(KeyCode.Space)) // keybinding to place object
        { 
            if (mode)
            {
                if (int.Parse(StarsLeft.text) != 0)
                {
                    PlaceStar();
                }
                else
                {
                    // display error/negative response
                }
            }
            else
            {
                PlaceLine();
            }
        }
        
    }

    public void UpdateCursor()
    {
        float horDirection = Input.GetAxis("Horizontal");
        float verDirection = Input.GetAxis("Vertical");
        if (horDirection != 0)
        {
            // adjust horizontal Cursor velocity/position
            // prevent from going outside of bounds of EditorArea
        }
        if (verDirection != 0)
        {
            // adjust vertical Cursor velocity/position
            // prevent from going outside of bounds of EditorArea
        }
    }

    public void PlaceStar()
    {
        StarsLeft.text = (int.Parse(StarsLeft.text) - 1).ToString();
        // place Star object on Graph or Image given Cursor position
    }

    public void PlaceLine()
    {
        // Place Line Object between two points on Graph or Image given Cursor/Cursor2 position
    }

    public void ToggleMode()
    {
        mode = !mode;
        if (mode)
        {
            CreateStars.enabled = false;
            CreateLines.enabled = true;
        } else
        {
            CreateStars.enabled = true;
            CreateLines.enabled = false;
        }

    }

    public void ClearAll()
    {
        StarsLeft.text = starsMax.ToString();
        // clear saved Graph or Image
    }

    public void SaveConstellation()
    {
        // save Graph or Image of Constellation locally
    }


    public void LoadNextLevel()
    {
        SaveConstellation();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevelSelect()
    {
        SaveConstellation();
        SceneManager.LoadScene("LevelSelect");
    }
}
