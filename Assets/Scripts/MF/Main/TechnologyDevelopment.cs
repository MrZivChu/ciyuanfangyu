using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyDevelopment : MonoBehaviour
{
    public HUD hudScript;
    public Button backBtn;
    void Start()
    {
        EventTriggerListener.Get(backBtn.gameObject).onClick = BackClick;
    }

    void BackClick(GameObject go, object data)
    {
        hudScript.ResetTweenPlay();
        Destroy(gameObject);
    }
}
