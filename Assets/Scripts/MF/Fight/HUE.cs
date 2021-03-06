﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uTools;

public class HUE : MonoBehaviour
{
    public Button leftBtn;
    public Button rightBtn;


    public List<GameObject> mercenaryGameobjectList;
    private void Start()
    {
        EventTriggerListener.Get(leftBtn.gameObject).onClick = LeftClick;
        EventTriggerListener.Get(leftBtn.gameObject).onDown = LeftDown;
        EventTriggerListener.Get(leftBtn.gameObject).onUp = LeftUp;

        EventTriggerListener.Get(rightBtn.gameObject).onClick = RightClick;
        EventTriggerListener.Get(rightBtn.gameObject).onDown = RightDown;
        EventTriggerListener.Get(rightBtn.gameObject).onUp = RightUp;

        InitMercenary();

        AudioSource mainAudioSource = GameObject.Find("DontDestroyOnLoad").GetComponent<AudioSource>();
        mainAudioSource.enabled = false;
    }

    public List<uTweenPosition> tweenPositions;
    public void PlayHumAnima()
    {
        if (tweenPositions != null && tweenPositions.Count > 0)
        {
            for (int i = 0; i < tweenPositions.Count; i++)
            {
                tweenPositions[i].Reset();
            }
        }
    }

    //是否长按
    bool isPressing = false;
    bool isClockwise = false;
    public NewRotateWheel newRotateWheel;
    void LeftClick(GameObject go, object data)
    {
        GroupCheck.needTriggerEnterCheck = true;
        if (!isPressing)
        {
            newRotateWheel.Rotate(isClockwise);
        }
        newRotateWheel.clickOver = true;
    }

    void LeftDown(GameObject go, object data)
    {
        MusicManager.Play(MusicType.diskUpDown);
        GroupCheck.needTriggerEnterCheck = false;
        isClockwise = true;
        isPressing = false;
        newRotateWheel.clickOver = false;
        Invoke("RepeatLeftClick", 0.2f);
    }

    void RepeatLeftClick()
    {
        isPressing = true;
        InvokeRepeating("SingleClick", 0, 0.6f);
    }

    void SingleClick()
    {
        newRotateWheel.Rotate(isClockwise);
    }

    void LeftUp(GameObject go, object data)
    {
        CancelInvoke();
    }

    void RightClick(GameObject go, object data)
    {
        GroupCheck.needTriggerEnterCheck = true;
        if (!isPressing)
        {
            newRotateWheel.Rotate(isClockwise);
        }
        newRotateWheel.clickOver = true;
    }

    void RightDown(GameObject go, object data)
    {
        MusicManager.Play(MusicType.diskUpDown);
        GroupCheck.needTriggerEnterCheck = false;
        isClockwise = false;
        isPressing = false;
        newRotateWheel.clickOver = false;
        Invoke("RepeatLeftClick", 0.2f);
    }

    void RightUp(GameObject go, object data)
    {
        CancelInvoke();
    }

    void InitMercenary()
    {
        if (LocalBaseData.battleMercenaryList != null && LocalBaseData.battleMercenaryList.Count > 0)
        {
            for (int i = 0; i < LocalBaseData.battleMercenaryList.Count; i++)
            {
                MercenaryConfigInfo mer = MercenaryDataConfigTable.MercenaryList.Find(it => it.ID == LocalBaseData.battleMercenaryList[i]);
                if (mer != null)
                {
                    GameObject go = mercenaryGameobjectList[i];
                    Sprite sprite = Resources.Load<Sprite>(mer.iconPath.Replace("zm", "zd"));
                    go.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                    go.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "LV" + mer.star;
                    go.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = mer.mercenaryName;
                    go.SetActive(true);
                }
            }
        }

        int count = LocalBaseData.battleMercenaryList == null ? mercenaryGameobjectList.Count : LocalBaseData.battleMercenaryList.Count;
        for (int i = count; i < mercenaryGameobjectList.Count; i++)
        {
            mercenaryGameobjectList[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RecoverManagerBatteryData.fromFightScene = true;
            Loading.sceneName = "Manager";
            SceneManager.LoadScene("Loading");
        }
    }
}


