using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static int numOfLevels = 20;
    public static bool[] levelsCleared;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LevelCleared(int level)
    {
        if (levelsCleared == null)
        {
            levelsCleared = new bool[numOfLevels];
        }
        levelsCleared[level] = true;
        for (int i = 0; i < numOfLevels; i++)
        {
            Debug.Log(i + ": " + levelsCleared[i]);
        }
    }
}
