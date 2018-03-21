using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//恢复设置的炮塔数据
public class RecoverFightBatteryData : RecoverBatteryDataBase
{
    void Start()
    {
        base.BaseStart();

        Invoke("ChangeAttackBattle", 2);
    }

    void ChangeAttackBattle()
    {
        foreach (Transform item in tempHoleList)
        {
            int count = item.childCount;
            if (count > 3)
            {
                GameObject groupObj = item.GetChild(count - 1).gameObject;
                if (groupObj != null)
                {
                    int num = groupObj.transform.childCount;
                    if (num > 0)
                    {
                        Transform tt = groupObj.transform.GetChild(num - 1);
                        if (tt != null)
                        {
                            Range range = tt.GetComponent<Range>();
                            if (range != null)
                            {
                                if (RangeCheckManager.rangList.Contains(range))
                                    RangeCheckManager.rangList.Remove(range);
                            }
                        }
                    }
                    DestroyImmediate(groupObj);
                }
            }
            DestoryBattery(item);
        }
        print("*************开始换战斗炮塔*************");
        HandleRecoverData();
    }

    public List<Transform> tempHoleList = new List<Transform>();
    public override GameObject SpawnBattery(BatteryConfigInfo info, Transform hole, int level = 1)
    {
        GameObject go = null;
        if (hole != null && info != null)
        {
            go = InstanceManagerBatteryObj(info, hole, level);
            if (go != null)
            {
                int index = hole.GetComponent<BuildConfig>().index;

                ServerBatteryData bd = new ServerBatteryData();
                bd.batteryLevel = level;
                bd.batteryType = info.battleType;
                bd.index = index;

                if (level == 1)
                {
                    tempHoleList.Add(hole);
                    hasHoleWithTypeDic[index] = info.battleType;
                    ServerDataHelper.AddSingleServerBatteryData(bd);

                    SpawnSingleRange(bd, go.transform);

                    CheckGroup();
                    CheckRange();
                }
                else
                {
                    if (level == 2)
                    {
                        go.transform.Translate(go.transform.forward * 3, Space.World);
                    }

                    SpawnSingleRange(bd, go.transform);
                    CheckRange();
                }
            }
        }
        return go;
    }

    bool startChangeAttackBattle = false;
    public override GameObject InstanceManagerBatteryObj(BatteryConfigInfo info, Transform parent, int level = 1)
    {
        Object obj = null;
        if (startChangeAttackBattle)
        {
            obj = Resources.Load(info.battleType + "AttackLv" + level);
        }
        else
        {
            startChangeAttackBattle = true;
            obj = Resources.Load(info.battleType + "DeformationLv" + level);
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
