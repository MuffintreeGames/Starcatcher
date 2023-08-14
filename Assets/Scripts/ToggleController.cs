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
    public PlayerController player;
    public BlackHoleController blackHole;
    public WhiteHoleController whiteHole;
    

    int placeInCycle = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (toggleEvent == null)
        {
            toggleEvent = new ToggleEvent();
        }
        toggleEvent.AddListener(TriggerToggle);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void TriggerToggle()    //swap object that player is controlling
    {
        switch (placeInCycle)
        {
            case 1: if (blackHole != null) { Debug.Log("deactivating black hole!"); blackHole.DeactivateBlackHole(); } break;
            case 2: if (whiteHole != null) { Debug.Log("deactivating white hole!"); whiteHole.DeactivateWhiteHole(); } break;
            case 0: if (player != null) { Debug.Log("deactivating player!"); player.DeactivatePlayer(); } break; 
        }
        placeInCycle++;
        if (placeInCycle > 2)
        {
            placeInCycle = 0;
        }
        switch (placeInCycle)
        {
            case 1: if (blackHole != null) { Debug.Log("activating black hole!"); blackHole.ActivateBlackHole(); break; } else { placeInCycle = 2; goto case 2; };
            case 2: if (whiteHole != null) { Debug.Log("activating white hole!"); whiteHole.ActivateWhiteHole(); break; } else { placeInCycle = 0; goto case 0; };
            case 0: if (player != null) { Debug.Log("activating player!"); player.ActivatePlayer(); } break;
        }
    }
}
