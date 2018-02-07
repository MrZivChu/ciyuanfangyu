﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUE : MonoBehaviour
{
    public GameObject result;
    public Button okBtn;
    public Button leftBtn;
    public Button rightBtn;


    public List<GameObject> mercenaryGameobjectList;

    private void Start()
    {
        EventTriggerListener.Get(okBtn.gameObject).onClick = FightOverClick;
        EventTriggerListener.Get(leftBtn.gameObject).onClick = LeftClick;
        EventTriggerListener.Get(leftBtn.gameObject).onDown = LeftDown;
        EventTriggerListener.Get(leftBtn.gameObject).onUp = LeftUp;
        EventTriggerListener.Get(rightBtn.gameObject).onClick = RightClick;

        InitMercenary();
    }

    void FightOverClick(GameObject go, object data)
    {
        result.SetActive(true);
    }

    //是否长按
    bool isPressing = false;
    public NewRotateWheel newRotateWheel;
    void LeftClick(GameObject go, object data)
    {
        if (!isPressing)
        {
            print("click");
            newRotateWheel.Rotate(true);
        }
        newRotateWheel.isStop = true;
    }

    void LeftDown(GameObject go, object data)
    {
        print("down");
        isPressing = false;
        newRotateWheel.isStop = false;
        Invoke("RepeatLeftClick", 0.2f);
    }

    void RepeatLeftClick()
    {
        print("pressing");
        isPressing = true;
        InvokeRepeating("SingleClick", 0, 0.5f);
    }

    void SingleClick()
    {
        newRotateWheel.Rotate(true);
    }

    void LeftUp(GameObject go, object data)
    {
        print("up");
        CancelInvoke();
    }

    void RightClick(GameObject go, object data)
    {
        newRotateWheel.Rotate(false);
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

    public List<GroupCheck> groupCheckList = new List<GroupCheck>();
    void CheckGroup()
    {
        if (groupCheckList != null && groupCheckList.Count > 0)
        {
            for (int i = 0; i < groupCheckList.Count; i++)
            {
                groupCheckList[i].Check();
            }
        }
    }
}


