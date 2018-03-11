using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//恢复设置的炮塔数据
public class RecoverFightBatteryData : MonoBehaviour
{
    public bool isFight = false;
    bool startChangeAttackBattle = false;

    //坑
    public List<GameObject> batteryHoleList = new List<GameObject>();
    //索引对应坑
    public Dictionary<int, GameObject> holeDic = new Dictionary<int, GameObject>();
    //坑对应炮塔类型
    public Dictionary<int, BatteryType> hasDic = new Dictionary<int, BatteryType>();

    void Start()
    {
        HandleHoleList();
        HandleRecoverData();
        CheckGroup();

        if (isFight)
        {
            Invoke("ChangeAttackBattle", 2);
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

    void ChangeAttackBattle()
    {
        foreach (GameObject item in tempObjList)
        {
            Destroy(item);
        }
        HandleRecoverData();
    }

    void HandleHoleList()
    {
        if (batteryHoleList != null && batteryHoleList.Count > 0)
        {
            for (int i = 0; i < batteryHoleList.Count; i++)
            {
                BuildConfig bc = batteryHoleList[i].GetComponent<BuildConfig>();
                if (!holeDic.ContainsKey(bc.index))
                {
                    holeDic[bc.index] = batteryHoleList[i];
                }
            }
        }
    }
    public CircleCheckManager circleCheckManager;

    List<GameObject> tempObjList = new List<GameObject>();
    void HandleRecoverData()
    {
        List<ServerBatteryData> serverDataList = ServerDataHelper.GetServerBatteryData();
        if (batteryHoleList != null && batteryHoleList.Count > 0)
        {
            if (serverDataList != null && serverDataList.Count > 0)
            {
                for (int i = 0; i < serverDataList.Count; i++)
                {
                    ServerBatteryData bd = serverDataList[i];
                    if (holeDic.ContainsKey(bd.index))
                    {
                        if (BatteryDataConfigTable.dic.ContainsKey(bd.batteryType))
                        {
                            BatteryConfigInfo info = BatteryDataConfigTable.dic[bd.batteryType];
                            GameObject hole = holeDic[bd.index];
                            GameObject go = InstanceManagerBatteryObj(info, hole.transform);
                            if (isFight)
                            {
                                tempObjList.Add(go);
                            }
                            hasDic[bd.index] = bd.batteryType;
                        }
                    }
                }
            }
        }
        if (circleCheckManager != null)
            circleCheckManager.Check();
    }

    public GameObject InstanceManagerBatteryObj(BatteryConfigInfo info, Transform parent, int level = 1)
    {
        Object obj = null;
        if (isFight)
        {
            if (startChangeAttackBattle)
            {
                obj = Resources.Load(info.battleType + "AttackLv" + level);
            }
            else
            {
                startChangeAttackBattle = true;
                obj = Resources.Load(info.battleType + "DeformationLv" + level);
            }
        }
        else
        {
            obj = Resources.Load(info.battleType + "Lv" + level);
        }
        if (obj != null)
        {
            GameObject tempGO = Instantiate(obj) as GameObject;
            tempGO.transform.parent = parent;
            //tempGO.transform.localScale = Vector3.one;
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
            bp.battleType = info.battleType;
            bp.starLevel = info.starLevel;
            bp.wood = info.wood;

            Vector3 vv = Vector3.zero;
            vv.y = tempGO.transform.position.y;
            tempGO.transform.LookAt(vv);
            tempGO.transform.Rotate(Vector3.up, 180);


            return tempGO;
        }
        return null;
    }


}
