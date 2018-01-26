using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//恢复设置的炮塔数据
public class RecoverBatteryData : MonoBehaviour
{
    public List<GameObject> batteryHoleList;
    public Dictionary<int, GameObject> holeDic = new Dictionary<int, GameObject>();

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
        List<BatteryData> localDataList = GameJsonDataHelper.ReadBatteryData();
        if (batteryHoleList != null && batteryHoleList.Count > 0)
        {
            if (localDataList != null && localDataList.Count > 0)
            {
                for (int i = 0; i < localDataList.Count; i++)
                {
                    BatteryData bd = localDataList[i];
                    if (holeDic.ContainsKey(bd.index))
                    {
                        if (BatteryDataConfigTable.dic.ContainsKey(bd.batteryType))
                        {
                            BatteryConfigInfo info = BatteryDataConfigTable.dic[bd.batteryType];
                            GameObject hole = holeDic[bd.index];
                            InstanceObj(info, hole);
                        }
                    }
                }
            }
        }
    }

    void InstanceObj(BatteryConfigInfo info, GameObject parent)
    {
        UnityEngine.Object obj = Resources.Load(info.battleType.ToString() + "Lv1");
        if (obj != null)
        {
            GameObject tempGO = Instantiate(obj) as GameObject;
            tempGO.transform.parent = parent.transform;
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
            BuildConfig bc = parent.transform.GetComponent<BuildConfig>();
            bc.currentBP = bp;
            bc.batteryConfigInfo = info;
        }
    }
   
}
