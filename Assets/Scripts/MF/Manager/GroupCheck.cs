using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCheck : MonoBehaviour
{
    public RecoverBatteryData recoverBatteryDataScript;
    public List<Transform> list = new List<Transform>();

    private void Start()
    {
    }


    public static bool canCheck = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkGroup"))
        {
            Transform hole = other.transform.parent;
            if (!list.Contains(hole))
            {
                list.Add(hole);
                //print("触发器" + gameObject.name);
                if (canCheck)
                {
                    Check();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("checkGroup"))
        {
            Transform hole = other.transform.parent;
            if (list.Contains(hole))
            {
                //print(other.name + "离开了");
                if (hole.childCount > 2)
                {
                    hole.GetChild(2).gameObject.SetActive(true);
                }
                list.Remove(hole);
                Check();
            }
        }
    }

    GameObject groupObj;
    Transform battery1;
    Transform battery2;
    Transform battery3;
    public void Check()
    {
        if (list.Count == 3)
        {
            Transform hole1 = list[0];
            Transform hole2 = list[1];
            Transform hole3 = list[2];
            int index1 = hole1.GetComponent<BuildConfig>().index;
            int index2 = hole2.GetComponent<BuildConfig>().index;
            int index3 = hole3.GetComponent<BuildConfig>().index;

            List<int> indexList = new List<int>() { index1, index2, index3 };
            OrderIndex(indexList);
            index1 = indexList[0];
            index2 = indexList[1];
            index3 = indexList[2];

            Dictionary<int, BatteryType> dic = recoverBatteryDataScript.hasDic;
            if (dic != null && dic.Count > 0)
            {
                hole1 = recoverBatteryDataScript.holeDic[index1].transform;
                hole2 = recoverBatteryDataScript.holeDic[index2].transform;
                hole3 = recoverBatteryDataScript.holeDic[index3].transform;

                //之前的组合炮塔销毁
                DestoryGroupBattery();
                if (hole1.childCount > 2)
                {
                    battery1 = hole1.GetChild(2);
                    battery1.gameObject.SetActive(true);
                }
                if (hole2.childCount > 2)
                {
                    battery2 = hole2.GetChild(2);
                    battery2.gameObject.SetActive(true);
                }
                if (hole3.childCount > 2)
                {
                    battery3 = hole3.GetChild(2);
                    battery3.gameObject.SetActive(true);
                }

                bool hasBattery1 = dic.ContainsKey(index1);
                bool hasBattery2 = dic.ContainsKey(index2);
                bool hasBattery3 = dic.ContainsKey(index3);
                //三个坑都有炮塔
                if (hasBattery1 && hasBattery2 && hasBattery3)
                {
                    if (dic[index1] == dic[index2] && dic[index2] == dic[index3])
                    {
                        //print("所有炮塔类型都一样");
                        //生成Lv3的炮塔
                        groupObj = recoverBatteryDataScript.InstanceBatteryObj(BatteryDataConfigTable.dic[dic[index1]], hole2, 3);
                        //三个炮塔隐藏                          
                        battery1.gameObject.SetActive(false);
                        battery2.gameObject.SetActive(false);
                        battery3.gameObject.SetActive(false);
                    }
                    else if (dic[index1] == dic[index2] && dic[index2] != dic[index3])
                    {
                        //print("第一个炮塔和第二个炮塔类型一样");
                        //生成Lv2的炮塔
                        groupObj = recoverBatteryDataScript.InstanceBatteryObj(BatteryDataConfigTable.dic[dic[index1]], hole1, 2);
                        groupObj.transform.Translate(-groupObj.transform.forward * 3, Space.World);
                        //第一个和第二个的炮塔隐藏
                        battery1.gameObject.SetActive(false);
                        battery2.gameObject.SetActive(false);
                    }
                    else if (dic[index2] == dic[index3] && dic[index1] != dic[index2])
                    {
                        //print("第二个炮塔和第三个炮塔类型一样");
                        //生成Lv2的炮塔
                        groupObj = recoverBatteryDataScript.InstanceBatteryObj(BatteryDataConfigTable.dic[dic[index2]], hole2, 2);
                        groupObj.transform.Translate(-groupObj.transform.forward * 3, Space.World);
                        //第二个和第三个的炮塔隐藏
                        battery2.gameObject.SetActive(false);
                        battery3.gameObject.SetActive(false);
                    }
                    else
                    {
                        //print("第一个和第三个的炮塔无法进行组合");
                    }

                }
                else if (hasBattery1 && hasBattery2)
                {
                    if (dic[index1] == dic[index2])
                    {
                        //print("第一个炮塔和第二个炮塔类型一样");
                        //生成Lv2的炮塔
                        groupObj = recoverBatteryDataScript.InstanceBatteryObj(BatteryDataConfigTable.dic[dic[index1]], hole1, 2);
                        groupObj.transform.Translate(-groupObj.transform.forward * 3, Space.World);
                        //第一个和第二个的炮塔隐藏
                        battery1.gameObject.SetActive(false);
                        battery2.gameObject.SetActive(false);
                    }
                    else
                    {
                        //print("第一个和第二个的炮塔类型不一样");
                    }
                }
                else if (hasBattery1 && hasBattery3)
                {
                    //print("第一个和第三个的炮塔无法进行组合");
                }
                else if (hasBattery2 && hasBattery3)
                {
                    if (dic[index2] == dic[index3])
                    {
                        //print("第二个炮塔和第三个炮塔类型一样");
                        //生成Lv2的炮塔
                        groupObj = recoverBatteryDataScript.InstanceBatteryObj(BatteryDataConfigTable.dic[dic[index2]], hole2, 2);
                        groupObj.transform.Translate(-groupObj.transform.forward * 3, Space.World);
                        //第二个和第三个的炮塔隐藏
                        battery2.gameObject.SetActive(false);
                        battery3.gameObject.SetActive(false);
                    }
                    else
                    {
                        //print("第二个和第三个的炮塔类型不一样");
                    }
                }
                else
                {
                    //print("少于两个炮塔不需要进行任何判定");
                }
            }
            else
            {
                //print("没有任何炮塔");
            }
        }
        else if (list.Count == 2)
        {
            Transform hole1 = list[0];
            Transform hole2 = list[1];
            int index1 = hole1.GetComponent<BuildConfig>().index;
            int index2 = hole2.GetComponent<BuildConfig>().index;

            List<int> indexList = new List<int>() { index1, index2 };
            OrderIndex(indexList);
            index1 = indexList[0];
            index2 = indexList[1];

            Dictionary<int, BatteryType> dic = recoverBatteryDataScript.hasDic;
            if (dic != null && dic.Count > 0)
            {
                hole1 = recoverBatteryDataScript.holeDic[index1].transform;
                hole2 = recoverBatteryDataScript.holeDic[index2].transform;

                //之前的组合炮塔销毁
                DestoryGroupBattery();
                if (hole1.childCount > 2)
                {
                    battery1 = hole1.GetChild(2);
                    battery1.gameObject.SetActive(true);
                }
                if (hole2.childCount > 2)
                {
                    battery2 = hole2.GetChild(2);
                    battery2.gameObject.SetActive(true);
                }

                bool hasBattery1 = dic.ContainsKey(index1);
                bool hasBattery2 = dic.ContainsKey(index2);
                //两个坑都有炮塔
                if (hasBattery1 && hasBattery2)
                {
                    if (dic[index1] == dic[index2])
                    {
                        if ((index1 >= 1 && index1 <= 8 && index2 >= 9 && index2 <= 24) || (index1 >= 9 && index1 <= 24 && index2 >= 25 && index2 <= 48))
                        {
                            //print("所有炮塔类型都一样");
                            //生成Lv2的炮塔
                            groupObj = recoverBatteryDataScript.InstanceBatteryObj(BatteryDataConfigTable.dic[dic[index1]], hole1, 2);
                            groupObj.transform.Translate(-groupObj.transform.forward * 3, Space.World);
                            //两个炮塔隐藏                          
                            battery1.gameObject.SetActive(false);
                            battery2.gameObject.SetActive(false);
                        }
                        else
                        {
                            //print("两个炮塔不相邻");
                        }
                    }
                    else
                    {
                        //print("两个炮塔类型不一样");
                    }
                }
                else
                {
                    //print("只有一个坑有炮塔");
                }
            }
        }
        else if (list.Count == 1)
        {
            //print("只有一个坑");
            Transform hole1 = list[0];
            int index1 = hole1.GetComponent<BuildConfig>().index;

            Dictionary<int, BatteryType> dic = recoverBatteryDataScript.hasDic;
            if (dic != null && dic.Count > 0)
            {
                hole1 = recoverBatteryDataScript.holeDic[index1].transform;
                //之前的组合炮塔销毁
                DestoryGroupBattery();
                if (hole1.childCount > 2)
                {
                    battery1 = hole1.GetChild(2);
                    battery1.gameObject.SetActive(true);
                }
            }
        }
    }

    void DestoryGroupBattery()
    {
        if (groupObj != null)
        {
            Destroy(groupObj);
        }
    }

    void OrderIndex(List<int> indexList)
    {
        indexList.Sort((a, b) => { return a < b ? -1 : (a > b ? 1 : 0); });
    }
}
