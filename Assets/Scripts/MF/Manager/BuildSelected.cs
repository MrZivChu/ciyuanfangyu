using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uTools;

public class BuildSelected : MonoBehaviour
{
    public List<Toggle> toggleList;
    public Button closeBtn;
    public GameObject cellParent;
    public Text titleText;

    public HUM humScript;

    GameObject BatteryCell;
    void Start()
    {
        BatteryCell = Resources.Load("UI/Manager/BatteryCell") as GameObject;
        RegisterEventForToggle();
        FirstToggleClick();

        closeBtn.onClick.AddListener(CloseThisPanel);
    }

    void CloseThisPanel()
    {
        Destroy(gameObject);
        HUM.uiIsShow = false;
    }

    void RegisterEventForToggle()
    {
        if (toggleList != null && toggleList.Count > 0)
        {
            for (int i = 0; i < toggleList.Count; i++)
            {
                EventTriggerListener.Get(toggleList[i].gameObject, i).onClick = Selected;
            }
        }
    }

    void FirstToggleClick()
    {
        toggleList[0].isOn = true;
        Selected(toggleList[0].gameObject, 0);
    }

    List<string> titleList = new List<string>() { "所有", "加农炮", "加特林枪", "导弹", "暂时", "特殊", "公共" };
    void Selected(GameObject obj, object param)
    {
        Toggle toggle = obj.GetComponent<Toggle>();
        if (toggle.isOn)
        {
            ClearChildren(cellParent.transform);
            int index = (int)param;
            titleText.text = "单位（" + titleList[index] + "）";
            if (index == 0)
            {
                SpawnCell(BatteryDataConfigTable.allList);
            }
            else if (index == 1)
            {
                SpawnCell(BatteryDataConfigTable.cannonList);
            }
            else if (index == 2)
            {
                SpawnCell(BatteryDataConfigTable.gatlinGunList);
            }
            else if (index == 3)
            {
                SpawnCell(BatteryDataConfigTable.missileList);
            }
            else if (index == 4)
            {
                SpawnCell(BatteryDataConfigTable.specialList);
            }
            else if (index == 5)
            {
                SpawnCell(BatteryDataConfigTable.publicList);
            }
        }
    }

    void ClearChildren(Transform tParent)
    {
        int childCount = tParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(tParent.GetChild(i).gameObject);
        }
    }

    void SpawnCell(List<BatteryConfigInfo> infoList)
    {
        for (int i = 0; i < infoList.Count; i++)
        {
            GameObject go = Instantiate(BatteryCell);
            go.transform.SetParent(cellParent.transform);
            go.transform.localScale = Vector3.one;
            BatteryConfigInfo info = infoList[i];
            BatteryCell batteryCell = go.GetComponent<BatteryCell>();
            batteryCell.batteryInfo = info;

            GameObject okBtn = go.transform.GetChild(14).gameObject;
            EventTriggerListener.Get(okBtn, info).onClick = PlaceBattery;
        }
    }

    void PlaceBattery(GameObject go, object param)
    {
        BatteryConfigInfo info = (BatteryConfigInfo)param;
        humScript.StartSpawnBattery(info, humScript.currentHitHoleObj);
        CloseThisPanel();
    }
}


