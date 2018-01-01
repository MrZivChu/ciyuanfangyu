using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInfo : MonoBehaviour
{
    public HUD hudScript;
    public GameObject clickMask;
    void Start()
    {
        EventTriggerListener.Get(clickMask).onClick = ClosePanel;
    }

    void ClosePanel(GameObject go, object obj)
    {
        hudScript.ResetTweenPlay();
        Destroy(gameObject);
    }
}
