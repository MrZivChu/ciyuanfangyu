using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//恢复设置的炮塔数据
public class RecoverBatteryData : MonoBehaviour
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
                            InstanceBatteryObj(info, hole.transform);
                            hasDic[bd.index] = bd.batteryType;
                        }
                    }
                }
            }
        }
    }

    public GameObject InstanceBatteryObj(BatteryConfigInfo info, Transform parent, int level = 1)
    {
        Object obj = Resources.Load(info.battleType + "Lv" + level);
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
            bp.battleType = info.battleType;
            bp.starLevel = info.starLevel;
            bp.wood = info.wood;

            Vector3 direction = tempGO.transform.position - Vector3.zero;
            direction.z = tempGO.transform.position.z;
            tempGO.transform.LookAt(-direction);

            return tempGO;
        }
        return null;
    }
}
