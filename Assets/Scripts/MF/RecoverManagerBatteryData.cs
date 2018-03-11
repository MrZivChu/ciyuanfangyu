using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//恢复设置的炮塔数据
public class RecoverManagerBatteryData : MonoBehaviour
{
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
                            SpawnBattery(info, hole.transform);
                        }
                    }
                }
            }
        }
    }


    public void DestoryBattery(Transform hold)
    {
        if (hold != null)
        {
            BuildConfig tBuildConfig = hold.GetComponent<BuildConfig>();
            if (tBuildConfig != null && hasDic.ContainsKey(tBuildConfig.index))
            {
                hasDic.Remove(tBuildConfig.index);

                GameObject tBattleTarget = hold.GetChild(2).gameObject;
                Destroy(tBattleTarget);

                GameObject tRangeTarget = hold.GetChild(3).gameObject;
                if (tRangeTarget != null)
                {
                    Range range = tRangeTarget.GetComponent<Range>();
                    if (range != null && CircleCheckManager.rangList.Contains(range))
                    {
                        CircleCheckManager.rangList.Remove(range);
                    }
                    Destroy(tRangeTarget);
                }

                ServerBatteryData bd = new ServerBatteryData();
                bd.batteryLevel = 1;
                bd.index = tBuildConfig.index;
                ServerDataHelper.DeleteSingleServerBatteryData(bd);

                CheckGroup();
                CheckCircle();
            }
        }
    }

    public GameObject SpawnBattery(BatteryConfigInfo info, Transform hole, int level = 1)
    {
        GameObject go = null;
        if (hole != null && info != null)
        {
            go = InstanceManagerBatteryObj(info, hole, level);

            int index = hole.GetComponent<BuildConfig>().index;
            hasDic[index] = info.battleType;

            ServerBatteryData bd = new ServerBatteryData();
            bd.batteryLevel = 1;
            bd.batteryType = info.battleType;
            bd.index = index;
            ServerDataHelper.AddSingleServerBatteryData(bd);

            SpawnSingleRange(bd);

            CheckGroup();
            CheckCircle();
        }
        return go;
    }

    GameObject InstanceManagerBatteryObj(BatteryConfigInfo info, Transform parent, int level = 1)
    {
        Object obj = Resources.Load(info.battleType + "Lv" + level);
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

    public CircleCheckManager circleCheckManager;
    void CheckCircle()
    {
        circleCheckManager.Check();
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

    public void SpawnSingleRange(ServerBatteryData bd)
    {
        if (holeDic.ContainsKey(bd.index))
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
                    GameObject go = (GameObject)Instantiate(obj);
                    go.transform.parent = holeDic[bd.index].transform;
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
                    if (!CircleCheckManager.rangList.Contains(rangeScript))
                    {
                        CircleCheckManager.rangList.Add(rangeScript);
                    }
                }
            }
        }
    }
}
