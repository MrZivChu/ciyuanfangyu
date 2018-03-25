using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverBatteryDataBase : MonoBehaviour
{
    //坑
    public List<GameObject> batteryHoleList = new List<GameObject>();
    //索引对应坑
    public Dictionary<int, GameObject> holeDic = new Dictionary<int, GameObject>();
    //坑对应炮塔类型
    public Dictionary<int, BatteryType> hasHoleWithTypeDic = new Dictionary<int, BatteryType>();

    public void BaseStart()
    {
        HandleHoleList();
        HandleRecoverData();
    }

    public virtual void HandleHoleList()
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

    public virtual void HandleRecoverData()
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
                            SpawnBattery(info, hole.transform);
                        }
                    }
                }
            }
        }
    }

    public GameObject SpawnGroup(List<GameObject> bList, BatteryConfigInfo info, Transform hole, int level = 1)
    {
        GameObject go = SpawnBattery(info, hole, level);
        go.GetComponent<BatteryParent>().singleHoleList = bList;
        return go;
    }

    public virtual GameObject SpawnBattery(BatteryConfigInfo info, Transform hole, int level = 1)
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

    public virtual GameObject InstanceManagerBatteryObj(BatteryConfigInfo info, Transform parent, int level = 1)
    {
        print(info.battleType + "Lv" + level);
        Object obj = Resources.Load(info.battleType + "Lv" + level);
        if (obj != null)
        {
            GameObject tempGO = Instantiate(obj) as GameObject;
            tempGO.transform.parent = parent;
            //tempGO.transform.localScale = Vector3.one;
            tempGO.transform.localPosition = Vector3.zero;
            BatteryParent bp = tempGO.GetComponent<BatteryParent>();
            bp.batteryName = info.batteryName;
            bp.attackValue = info.attack;
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

    public GameObject SpawnSingleRange(ServerBatteryData bd, Transform parent)
    {
        GameObject go = null;
        //if (holeDic.ContainsKey(bd.index))
        {
            if (BatteryDataConfigTable.dic.ContainsKey(bd.batteryType))
            {
                Object obj = null;
                if (BatteryDataConfigTable.dic[bd.batteryType].maxAttackDistance == 45)
                {
                    obj = Resources.Load("range1");
                }
                else if (BatteryDataConfigTable.dic[bd.batteryType].maxAttackDistance == 65)
                {
                    obj = Resources.Load("range2");
                }
                else if (BatteryDataConfigTable.dic[bd.batteryType].maxAttackDistance == 80)
                {
                    obj = Resources.Load("range3");
                }
                if (obj != null)
                {
                    go = (GameObject)Instantiate(obj);
                    go.transform.parent = parent;
                    go.transform.localScale = Vector3.one;

                    float angle = 0;
                    if (bd.index > 0 && bd.index < 9)
                    {
                        angle = 25 + 45 * (bd.index - 1);
                    }
                    else if (bd.index >= 9 && bd.index < 25)
                    {
                        int tIndex = bd.index - 8;
                        angle = 25 + 22.5f * (tIndex - 1);
                    }
                    else if (bd.index >= 25 && bd.index < 49)
                    {
                        int tIndex = bd.index - 24;
                        angle = -65 + 15 * (tIndex - 1);
                    }
                    else
                    {
                        int tIndex = bd.index - 48;
                        angle = 25 + 11.25f * (tIndex - 1);
                    }
                    go.transform.Rotate(0, 0, angle);

                    Range rangeScript = go.GetComponent<Range>();
                    if (rangeScript != null)
                    {
                        List<Range> list = RangeCheckManager.rangeList;
                        if (!list.Contains(rangeScript))
                        {
                            list.Add(rangeScript);
                        }
                    }
                }
            }
        }
        return go;
    }

    public void DestoryBattery(Transform hold)
    {
        if (hold != null)
        {
            BuildConfig tBuildConfig = hold.GetComponent<BuildConfig>();
            if (tBuildConfig != null && hasHoleWithTypeDic.ContainsKey(tBuildConfig.index))
            {
                hasHoleWithTypeDic.Remove(tBuildConfig.index);

                GameObject tBattleTarget = hold.GetChild(2).gameObject;
                GameObject tRangeTarget = tBattleTarget.transform.GetChild(tBattleTarget.transform.childCount - 1).gameObject;
                if (tRangeTarget != null)
                {
                    Range range = tRangeTarget.GetComponent<Range>();
                    if (range != null && RangeCheckManager.rangeList.Contains(range))
                    {
                        RangeCheckManager.rangeList.Remove(range);
                    }
                    DestroyImmediate(tRangeTarget);
                }
                DestroyImmediate(tBattleTarget);

                ServerBatteryData bd = new ServerBatteryData();
                bd.batteryLevel = 1;
                bd.index = tBuildConfig.index;
                ServerDataHelper.DeleteSingleServerBatteryData(bd);

                CheckGroup();
                CheckRange();
            }
        }
    }

    public void DestoryGroupBattery(GameObject groupObj)
    {
        if (groupObj != null)
        {
            print("之前的组合炮塔销毁");
            int count = groupObj.transform.childCount;
            if (count > 0)
            {
                Transform range = groupObj.transform.GetChild(count - 1);
                if (range != null)
                {
                    Range rangeScript = range.GetComponent<Range>();
                    if (rangeScript != null)
                    {
                        if (RangeCheckManager.rangeList.Contains(rangeScript))
                            RangeCheckManager.rangeList.Remove(rangeScript);
                    }
                }
            }
            Destroy(groupObj);
        }
    }

    public RangeCheckManager rangeCheckManager;
    public void CheckRange()
    {
        rangeCheckManager.Check();
    }

    public List<GroupCheck> groupCheckList = new List<GroupCheck>();
    public void CheckGroup()
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
