using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyDevelopment : MonoBehaviour
{
    public Button backBtn;
    void Start()
    {
        EventTriggerListener.Get(backBtn.gameObject).onClick = BackClick;
    }

    void BackClick(GameObject go, object data)
    {
        gameObject.SetActive(false);
    }
}
