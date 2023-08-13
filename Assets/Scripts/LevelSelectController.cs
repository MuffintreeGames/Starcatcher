using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectController : MonoBehaviour
{
    public TextMeshProUGUI LevelName;
    public int levelUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        LevelName.enabled = false;
        levelUnlocked = 1; // set this from GameState to be latest level unlocked.
    }

    // Update is called once per frame
    void Update()
    {
        // code for when levelUnlocked is high enough to unlock/enable next level
        // if mouse hovers over a level, update level name object
        // otherwise, load direct links below
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene("Level5");
    }

    public void LoadConstellation1()
    {
        SceneManager.LoadScene("Constellation1");
    }

    public void LoadLevel6()
    {
        SceneManager.LoadScene("Level6");
    }

    public void LoadLevel7()
    {
        SceneManager.LoadScene("Level7");
    }

    public void LoadLevel8()
    {
        SceneManager.LoadScene("Level8");
    }

    public void LoadLevel9()
    {
        SceneManager.LoadScene("Level9");
    }

    public void LoadLevel10()
    {
        SceneManager.LoadScene("Level10");
    }

    public void LoadConstellation2()
    {
        SceneManager.LoadScene("Constellation2");
    }

    public void LoadLevel11()
    {
        SceneManager.LoadScene("Level11");
    }

    public void LoadLevel12()
    {
        SceneManager.LoadScene("Level12");
    }

    public void LoadLevel13()
    {
        SceneManager.LoadScene("Level13");
    }

    public void LoadLevel14()
    {
        SceneManager.LoadScene("Level14");
    }

    public void LoadLevel15()
    {
        SceneManager.LoadScene("Level15");
    }

    public void LoadConstellation3()
    {
        SceneManager.LoadScene("Constellation3");
    }

    public void LoadLevel16()
    {
        SceneManager.LoadScene("Level16");
    }

    public void LoadLevel17()
    {
        SceneManager.LoadScene("Level17");
    }

    public void LoadLevel18()
    {
        SceneManager.LoadScene("Level18");
    }

    public void LoadLevel19()
    {
        SceneManager.LoadScene("Level19");
    }

    public void LoadLevel20()
    {
        SceneManager.LoadScene("Level20");
    }

    public void LoadConstellation4()
    {
        SceneManager.LoadScene("Constellation4");
    }
}
