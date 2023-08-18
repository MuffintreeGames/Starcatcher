using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetSelected : MonoBehaviour
{
    EventSystem events;
    GameObject lastSelected;
    // Start is called before the first frame update
    void Start()
    {
        events = GetComponent<EventSystem>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        events.SetSelectedGameObject(lastSelected);
    }

    // Update is called once per frame
    void Update()
    {
        if (events.currentSelectedGameObject == null)
        {
            events.SetSelectedGameObject(lastSelected);
        } else
        {
            lastSelected = events.currentSelectedGameObject;
        }
    }
}
