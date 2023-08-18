using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ConstellationController : MonoBehaviour
{
    public GameObject Constellation;
    bool isCapturing = false;
    Texture2D currentCapture;
    public TextMeshProUGUI ConstellationName;
    public TextMeshProUGUI StarsLeft;
    public TextMeshProUGUI Finished;
    public Image Finished1;
    public Image Finished2;
    public Image Finished3;
    public GameObject Cursor;
    public GameObject Cursor2; // for Lines
    public GameObject StarPrefab;
    public LineRenderer LineRenderer;
    public List<GameObject> Stars;
    public List<GameObject> Lines;
    public int starsMax;

    // Start is called before the first frame update
    void Start()
    {
        starsMax = int.Parse(StarsLeft.text);
        Stars = new List<GameObject>();
        Lines = new List<GameObject>();
        Finished.enabled = false;
        Finished1.enabled = false;
        Finished2.enabled = false;
        Finished3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // keybinding to place object
        { 
            if (Stars.Count < starsMax)
            {
                PlaceStar();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // keybinding to place object
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.X)) // keybinding to place object
        {
            ClearAll();
        }

    }

    public void PlaceStar()
    {
        StarsLeft.text = (int.Parse(StarsLeft.text) - 1).ToString();
        Stars.Add(Instantiate(StarPrefab, Cursor.transform.position, Quaternion.identity));
        PlaceLine();
        Cursor2.transform.position = Cursor.transform.position;

        if (Stars.Count == starsMax)
        {
            SaveConstellation();
            Finished.enabled = true;
            Finished1.enabled = true;
            Finished2.enabled = true;
            Finished3.enabled = true;
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
        Finished.enabled = false;
        Finished1.enabled = false;
        Finished2.enabled = false;
        Finished3.enabled = false;
    }

    public void SaveConstellation()
    {
        ScreenshotToImage();
    }


    public void LoadNextLevel()
    {
        if (Stars.Count == starsMax)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

        IEnumerator CaptureRoutine()
        {
            yield return new WaitForEndOfFrame();
            try
            {
                isCapturing = true;
                currentCapture = new Texture2D(400, 400, TextureFormat.RGB24, false);
                currentCapture.ReadPixels(new Rect(275, 10, 400, 400), 0, 0, false);
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
                Sprite sprite = Sprite.Create(currentCapture, new Rect(0, 0, 400, 400), new Vector2(0, 0));
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
