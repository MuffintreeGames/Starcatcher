using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BadgeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Constellation1;
    public Image Constellation2;
    public Image Constellation3;
    public Image Constellation4;
    public Image Constellation5;
    public Image Constellation6;
    public TextMeshProUGUI Title1;
    public TextMeshProUGUI Title2;
    public TextMeshProUGUI Title3;
    public TextMeshProUGUI Title4;
    public TextMeshProUGUI Title5;
    public TextMeshProUGUI Title6;
    public List<Image> Constellations = new();
    public List<TextMeshProUGUI> ConstellationTitles = new();

    void Start()
    {
        Title1.enabled = false;
        Title2.enabled = false;
        Title3.enabled = false;
        Title4.enabled = false;
        Title5.enabled = false;
        Title6.enabled = false;
        Constellations.Add(Constellation1);
        Constellations.Add(Constellation2);
        Constellations.Add(Constellation3);
        Constellations.Add(Constellation4);
        Constellations.Add(Constellation5);
        Constellations.Add(Constellation6);
        ConstellationTitles.Add(Title1);
        ConstellationTitles.Add(Title2);
        ConstellationTitles.Add(Title3);
        ConstellationTitles.Add(Title4);
        ConstellationTitles.Add(Title5);
        ConstellationTitles.Add(Title6);

        for (int i = 0; i < ConstellationController.ListConstellations.Count; i++)
            {
                Constellations[i].sprite = ConstellationController.ListConstellations[i];
                Constellations[i].color = new Color(255, 255, 255, 1);
                ConstellationTitles[i].enabled = true;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) // keybinding to place object
        {
            LoadContinue();
        }
    }

    public void LoadContinue()
    {
        GameObject.Find("MusicController").GetComponent<MenuSfx>().ButtonSelected();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
