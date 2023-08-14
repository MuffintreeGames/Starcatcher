using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ConstellationController : MonoBehaviour
{
    // Objects for Constellation Stars/Lines, Graph or Image.
    public GameObject Constellation;
    bool isCapturing = false;
    Texture2D currentCapture;

    // UI
    public TextMeshProUGUI ConstellationName;
    public TextMeshProUGUI StarsLeft;
    public GameObject CreateStars;
    public GameObject CreateLines;
    public GameObject Cursor;
    public GameObject Cursor2; // for Lines
    public GameObject StarPrefab;
    public LineRenderer LineRenderer;
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
                // PlaceLine();
            }
        }
        
    }

    public void PlaceStar()
    {
        StarsLeft.text = (int.Parse(StarsLeft.text) - 1).ToString();
        Stars.Add(Instantiate(StarPrefab, Cursor.transform.position, Quaternion.identity));
        PlaceLine();
        Cursor2.transform.position = Cursor.transform.position;

        // place Star object on Graph or Image given Cursor position
        if (Stars.Count == starsMax)
        {
            SaveConstellation();
        }
    }

    public void PlaceLine()
    {
        if (Stars.Count > 1)
        {
            CreateLine();
        }
    }

    public void CreateLine()
    {
        
        GameObject newLine = new GameObject();
        newLine.AddComponent<LineRenderer>();
        LineRenderer = newLine.GetComponent<LineRenderer>();
        LineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        LineRenderer.startWidth = 0.05f;
        LineRenderer.endWidth = 0.05f;
        LineRenderer.startColor = Color.white;
        LineRenderer.endColor = Color.white;
        LineRenderer.SetPosition(0, Cursor2.transform.position);
        LineRenderer.SetPosition(1, Cursor.transform.position);
        Lines.Add(newLine);
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
        Stars = new List<GameObject>();
        foreach (var obj in Lines)
        {
            Destroy(obj);
        }
        Lines = new List<GameObject>();
        // clear saved Graph or Image
    }

    public void SaveConstellation()
    {
        // save Graph or Image of Constellation locally
        ScreenshotToImage();
    }


    public void LoadNextLevel()
    {
        if (Stars.Count == starsMax)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadLevelSelect()
    {
        if (Stars.Count == starsMax)
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }

        IEnumerator CaptureRoutine()
        {
            yield return new WaitForEndOfFrame();
            try
            {
                isCapturing = true;
                currentCapture = new Texture2D(800, 800, TextureFormat.RGB24, false);
                currentCapture.ReadPixels(new Rect(600, 25, 800, 800), 0, 0, false);
                currentCapture.Apply();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Screen capture failed!");
                Debug.LogError(e.ToString());
                isCapturing = false;
            }
        }

        void LateUpdate()
        {
            if (isCapturing && currentCapture != null)
            {
                Sprite sprite = Sprite.Create(currentCapture, new Rect(0, 0, 800, 800), new Vector2(0, 0));
                // SpriteRenderer spriteRenderer = Constellation.GetComponent<SpriteRenderer>();
                //spriteRenderer.sprite = sprite;
                Image constellationImage = Constellation.GetComponent<Image>();
                constellationImage.sprite = sprite;
                isCapturing = false;
            }
        }

        public void ScreenshotToImage()
        {
            StartCoroutine(CaptureRoutine());
        }
    }
