using System.Collections;
using System.Collections.Generic;
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
    public List<Image> Constellations = new();

    void Start()
    {
        Constellations.Add(Constellation1);
        Constellations.Add(Constellation2);
        Constellations.Add(Constellation3);
        Constellations.Add(Constellation4);
        Constellations.Add(Constellation5);
        Constellations.Add(Constellation6);

        for (int i = 0; i < ConstellationController.ListConstellations.Count; i++)
            {
                Constellations[i].sprite = ConstellationController.ListConstellations[i];
            }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currently selected object: " + EventSystem.current.currentSelectedGameObject);
    }

    public void LoadContinue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
