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
    [HideInInspector]
    public GameObject targetBuild;

    public BatteryDataLibrary batteryDataLibrary;

    public bool isOpen = false;
    GameObject BatteryCell;

    void Start()
    {
        BatteryCell = Resources.Load("BatteryCell") as GameObject;
        RegisterEventForToggle();
        FirstToggleClick();

        closeBtn.onClick.AddListener(CloseThisPanel);
    }

    void CloseThisPanel()
    {
        if (leftInfoPanel.activeSelf)
        {
            uTweenPosition[] tuTweenPosition = leftInfoPanel.GetComponents<uTweenPosition>();
            tuTweenPosition[1].Reset();
        }
        else
        {
            PlayBuildSelectedTween(true);
        }
    }

    public void PlayBuildSelectedTween(bool isHide)
    {
        uTweenPosition[] tuTweenPosition = transform.GetComponents<uTweenPosition>();
        if (isHide)
        {
            //退出
            tuTweenPosition[2].Reset();
            tuTweenPosition[3].Reset();
        }
        else
        {
            //进入
            isOpen = true;
            tuTweenPosition[0].Reset();
            tuTweenPosition[1].Reset();
        }
    }

    public void BuildSelectedPanelHideOver()
    {
        isOpen = false;
        leftInfoPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void LeftInfoPanelHideOver()
    {
        PlayBuildSelectedTween(true);
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

            GameObject okBtn = go.transform.GetChild(7).gameObject;
            EventTriggerListener.Get(okBtn, info).onClick = PlaceBattery;

            EventTriggerListener.Get(go, info).onClick = LookInfo;
        }
    }

    public GameObject leftInfoPanel;
    void LookInfo(GameObject go, object param)
    {
        BatteryInfo info = (BatteryInfo)param;
        LeftInfo leftInfoScript = leftInfoPanel.GetComponent<LeftInfo>();
        leftInfoScript.SetData(info);
        if (!leftInfoPanel.activeSelf)
        {
            leftInfoPanel.SetActive(true);
            uTweenPosition[] tuTweenPosition = leftInfoPanel.GetComponents<uTweenPosition>();
            tuTweenPosition[0].Reset();
        }
    }

    void PlaceBattery(GameObject go, object param)
    {
        BatteryInfo info = (BatteryInfo)param;
        UnityEngine.Object obj = Resources.Load(info.battleType.ToString() + "Lv1");
        if (obj != null)
        {
            GameObject tempGO = Instantiate(obj) as GameObject;
            tempGO.transform.parent = targetBuild.transform;
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
            BuildConfig bc = targetBuild.transform.GetComponent<BuildConfig>();
            bc.currentBP = bp;

            BatteryData bd = new BatteryData();
            bd.batteryLevel = 1;
            bd.batteryType = info.battleType;
            bd.index = bc.index;
            GameJsonDataHelper.AddBatteryData(bd);
        }
        CloseThisPanel();
    }

    void Selected(GameObject obj, object param)
    {
        Toggle toggle = obj.GetComponent<Toggle>();
        if (toggle.isOn)
        {
            ClearChildren(cellParent.transform);
            int index = (int)param;
            if (index == 0)
            {
                SpawnCell(batteryDataLibrary.allList);
            }
            else if (index == 1)
            {
                SpawnCell(batteryDataLibrary.cannonList);
            }
            else if (index == 2)
            {
                SpawnCell(batteryDataLibrary.gatlinGunList);
            }
            else if (index == 3)
            {
                SpawnCell(batteryDataLibrary.missileList);
            }
            else if (index == 4)
            {
                SpawnCell(batteryDataLibrary.specialList);
            }
            else if (index == 5)
            {
                SpawnCell(batteryDataLibrary.publicList);
            }
        }
    }
}


