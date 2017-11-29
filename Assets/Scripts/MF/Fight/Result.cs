﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Button backBaseBtn;
    public Button backHeadquartersBtn;
    void Start()
    {
        EventTriggerListener.Get(backBaseBtn.gameObject).onClick = BackClick;
        EventTriggerListener.Get(backHeadquartersBtn.gameObject).onClick = BackClick;
    }

    void BackClick(GameObject go, object data)
    {
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }
}
