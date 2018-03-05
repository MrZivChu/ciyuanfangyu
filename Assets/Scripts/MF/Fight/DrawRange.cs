using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRange : MonoBehaviour
{
    //坑
    public List<GameObject> batteryHoleList = new List<GameObject>();
    //索引对应坑
    public Dictionary<int, GameObject> holeDic = new Dictionary<int, GameObject>();
    void Start()
    {
        HandleHoleList();
        SpawnRange();
        InvokeRepeating("ShowHideRange", 0, 0.2f);
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

    void SpawnRange()
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
                        Object obj = null;
                        if (BatteryDataConfigTable.dic.ContainsKey(bd.batteryType))
                        {
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
                        }
                    }
                }
            }
        }
    }

    void ShowHideRange()
    {

    }
}
