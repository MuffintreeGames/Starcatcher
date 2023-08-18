using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static int numOfLevels = 40;
    public static int levelsClearedInt = 0;
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
        levelsClearedInt = level;
    }
}
