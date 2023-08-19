using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisappearOnNotSelected : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                if (GetComponent<Image>().enabled != true)
                {
                    GameObject.Find("MusicController").GetComponent<MenuSfx>().ButtonHighlighted();
                }
                GetComponent<Image>().enabled = true;
            }
            else
            {
                GetComponent<Image>().enabled = false;
            }
        }
    }

    public void PlaySelectedSound()
    {
        GameObject.Find("MusicController").GetComponent<MenuSfx>().ButtonSelected();
    }
}
