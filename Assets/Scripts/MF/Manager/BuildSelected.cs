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
    [HideInInspector]
    public GameObject targetBuild;

    GameObject BatteryCell;

    void Start()
    {
        BatteryCell = Resources.Load("UI/BuildSelected/BatteryCell") as GameObject;
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

    void ClearChildren(Transform tParent)
    {
        int childCount = tParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(tParent.GetChild(i).gameObject);
        }
    }

    void FirstToggleClick()
    {
        toggleList[0].isOn = true;
        Selected(toggleList[0].gameObject, 0);
    }

    void SpawnCell(List<BatteryInfo> infoList)
    {
        for (int i = 0; i < infoList.Count; i++)
        {
            GameObject go = Instantiate(BatteryCell);
            go.transform.parent = cellParent.transform;
            go.transform.localScale = Vector3.one;
            BatteryInfo info = infoList[i];
            BatteryCell batteryCell = go.GetComponent<BatteryCell>();
            batteryCell.batteryInfo = info;

            GameObject okBtn = go.transform.GetChild(14).gameObject;
            EventTriggerListener.Get(okBtn, info).onClick = PlaceBattery;
        }
    }

    void PlaceBattery(GameObject go, object param)
    {
        BatteryInfo info = (BatteryInfo)param;
        SpawnBattery(info, targetBuild.transform);
        CloseThisPanel();
    }

    public void SpawnBattery(BatteryInfo info, Transform parent)
    {
        UnityEngine.Object obj = Resources.Load(info.battleType.ToString() + "Lv1");
        if (obj != null)
        {
            GameObject tempGO = Instantiate(obj) as GameObject;
            tempGO.transform.parent = parent;
            tempGO.transform.localScale = Vector3.one;
            tempGO.transform.localPosition = Vector3.zero;
            BatteryParent bp = tempGO.GetComponent<BatteryParent>();
            bp.batteryName = info.batteryName;
            bp.attack = info.attack;
            bp.attackRepeatRateTime = 2;
            bp.blood = info.blood;
            bp.desc = info.desc;
            bp.icon = info.icon;
            bp.maxAttackDistance = info.maxAttackDistance;
            bp.model = info.model;
            bp.MW = info.MW;
            bp.starLevel = info.starLevel;
            bp.wood = info.wood;
            BuildConfig bc = parent.GetComponent<BuildConfig>();
            bc.currentBP = bp;

            BatteryData bd = new BatteryData();
            bd.batteryLevel = 1;
            bd.batteryType = info.battleType;
            bd.index = bc.index;
            GameJsonDataHelper.AddBatteryData(bd);
        }
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
}


