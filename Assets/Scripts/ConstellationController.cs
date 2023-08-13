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
    public GameObject CreateStars;
    public GameObject CreateLines;
    public GameObject Cursor;
    public GameObject Cursor2; // for Lines
    public GameObject StarPrefab;
    public List<GameObject> Stars;
    public List<GameObject> Lines;
    public bool mode;
    public int starsMax;


    // Start is called before the first frame update
    void Start()
    {
        mode = true;
        CreateStars.SetActive(false);
        starsMax = int.Parse(StarsLeft.text);
        Stars = new List<GameObject>();
        Lines = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // keybinding to place object
        { 
            if (mode)
            {
                if (int.Parse(StarsLeft.text) != 0)
                {
                    PlaceStar();
                }
            } else
            {
                // if Cursor2 does not exist, and Cursor1 is hovering over a Star, Save Cursor2 as Cursor1
                // if Cursor2 does exist and Cursor1 is hovering over a Star, PlaceLine() and Reset Cursor2
                PlaceLine();
            }
        }
        
    }

    public void PlaceStar()
    {
        StarsLeft.text = (int.Parse(StarsLeft.text) - 1).ToString();
        Stars.Add(Instantiate(StarPrefab, Cursor.transform.position, Quaternion.identity));
        // place Star object on Graph or Image given Cursor position
    }

    public void PlaceLine()
    {
        // Place Line Object between Cursor1 and Cursor2 on Graph or Image given Cursor/Cursor2 position
    }

    public void ToggleMode()
    {
        mode = !mode;
        if (mode)
        {
            CreateStars.SetActive(false);
            CreateLines.SetActive(true);
        } else
        {
            CreateStars.SetActive(true);
            CreateLines.SetActive(false);
        }

    }

    public void ClearAll()
    {
        StarsLeft.text = starsMax.ToString();
        foreach (var obj in Stars)
        {
            Destroy(obj);
        }
        foreach (var obj in Lines)
        {
            Destroy(obj);
        }
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
