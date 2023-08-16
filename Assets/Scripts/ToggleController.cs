using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleEvent : UnityEvent
{

}

public class ToggleController : MonoBehaviour
{
    public static ToggleEvent toggleEvent;
    PlayerController player;
    BlackHoleController blackHole;
    WhiteHoleController whiteHole;

    public AudioSource toggleSound;
    

    int placeInCycle = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (toggleEvent == null)
        {
            toggleEvent = new ToggleEvent();
        }
        toggleEvent.AddListener(TriggerToggle);

        GameObject playerObject = GameObject.Find("Starmond");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerController>();
        } else
        {
            Debug.LogError("Failed to find player character");
        }

        GameObject blackHoleObject = GameObject.Find("BlackHole");
        if (blackHoleObject != null)
        {
            blackHole = blackHoleObject.GetComponent<BlackHoleController>();
        }

        GameObject whiteHoleObject = GameObject.Find("WhiteHole");
        if (whiteHoleObject != null)
        {
            whiteHole = whiteHoleObject.GetComponent<WhiteHoleController>();
        }

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void TriggerToggle()    //swap object that player is controlling
    {
        if (blackHole == null && whiteHole == null)
        {
            return;
        }
        switch (placeInCycle)
        {
            case 1: if (blackHole != null) { blackHole.DeactivateBlackHole(); } break;
            case 2: if (whiteHole != null) { whiteHole.DeactivateWhiteHole(); } break;
            case 0: if (player != null) { player.DeactivatePlayer(); } break; 
        }
        placeInCycle++;
        if (placeInCycle > 2)
        {
            placeInCycle = 0;
        }
        switch (placeInCycle)
        {
            case 1: if (blackHole != null) { blackHole.ActivateBlackHole(); break; } else { placeInCycle = 2; goto case 2; };
            case 2: if (whiteHole != null) { whiteHole.ActivateWhiteHole(); break; } else { placeInCycle = 0; goto case 0; };
            case 0: if (player != null) { player.ActivatePlayer(); } break;
        }
        toggleSound.Play();
    }
}
