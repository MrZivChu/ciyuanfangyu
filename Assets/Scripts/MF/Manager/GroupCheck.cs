using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroupCheck : MonoBehaviour
{
    public RecoverBatteryDataBase recoverBatteryDataBase;
    public List<Transform> list = new List<Transform>();

    void Start()
    {
    }

    public static bool needTriggerEnterCheck = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkGroup"))
        {
            Transform hole = other.transform.parent;
            if (!list.Contains(hole))
            {
                list.Add(hole);
                //print("触发器" + gameObject.name);

                if (needTriggerEnterCheck)
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
                    Transform battery1 = hole.GetChild(2);
                    //battery1.gameObject.SetActive(true);
                    SetBatteryState(battery1, true);
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
        //print(gameObject.name);
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
            Dictionary<int, BatteryType> dic = recoverBatteryDataBase.hasHoleWithTypeDic;
            if (dic != null && dic.Count > 0)
            {
                hole1 = recoverBatteryDataBase.holeDic[index1].transform;
                hole2 = recoverBatteryDataBase.holeDic[index2].transform;
                hole3 = recoverBatteryDataBase.holeDic[index3].transform;

                //print("之前的组合炮塔销毁");
                recoverBatteryDataBase.DestoryGroupBattery(groupObj);

                if (hole1.childCount > 2)
                {
                    //print("显示第一个炮塔");
                    battery1 = hole1.GetChild(2);
                    //battery1.gameObject.SetActive(true);
                    SetBatteryState(battery1, true);
                }
                if (hole2.childCount > 2)
                {
                    //print("显示第二个炮塔");
                    battery2 = hole2.GetChild(2);
                    //battery2.gameObject.SetActive(true);
                    SetBatteryState(battery2, true);
                }
                if (hole3.childCount > 2)
                {
                    //print("显示第三个炮塔");
                    battery3 = hole3.GetChild(2);
                    //battery3.gameObject.SetActive(true);
                    SetBatteryState(battery3, true);
                }

                bool hasBattery1 = dic.ContainsKey(index1);
                bool hasBattery2 = dic.ContainsKey(index2);
                bool hasBattery3 = dic.ContainsKey(index3);
                if (hasBattery1 && hasBattery2 && hasBattery3)
                {
                    //print("三个坑都有炮塔");
                    if (dic[index1] == dic[index2] && dic[index2] == dic[index3])
                    {
                        //print("所有炮塔类型都一样");
                        //生成Lv3的炮塔
                        List<GameObject> list = new List<GameObject>() { hole1.gameObject, hole2.gameObject, hole3.gameObject };
                        groupObj = recoverBatteryDataBase.SpawnGroup(list, BatteryDataConfigTable.dic[dic[index1]], hole2, 3);
                        //三个炮塔隐藏                          
                        //battery1.gameObject.SetActive(false);
                        //battery2.gameObject.SetActive(false);
                        //battery3.gameObject.SetActive(false);
                        SetBatteryState(battery1, false);
                        SetBatteryState(battery2, false);
                        SetBatteryState(battery3, false);
                    }
                    else if (dic[index1] == dic[index2] && dic[index2] != dic[index3])
                    {
                        //print("第一个炮塔和第二个炮塔类型一样");
                        //生成Lv2的炮塔
                        List<GameObject> list = new List<GameObject>() { hole1.gameObject, hole2.gameObject };
                        groupObj = recoverBatteryDataBase.SpawnGroup(list, BatteryDataConfigTable.dic[dic[index1]], hole1, 2);
                        //groupObj.transform.Translate(groupObj.transform.forward * 3, Space.World);
                        //第一个和第二个的炮塔隐藏
                        //battery1.gameObject.SetActive(false);
                        //battery2.gameObject.SetActive(false);
                        SetBatteryState(battery1, false);
                        SetBatteryState(battery2, false);
                    }
                    else if (dic[index2] == dic[index3] && dic[index1] != dic[index2])
                    {
                        //print("第二个炮塔和第三个炮塔类型一样");
                        //生成Lv2的炮塔
                        List<GameObject> list = new List<GameObject>() { hole2.gameObject, hole3.gameObject };
                        groupObj = recoverBatteryDataBase.SpawnGroup(list, BatteryDataConfigTable.dic[dic[index2]], hole2, 2);
                        //groupObj.transform.Translate(groupObj.transform.forward * 3, Space.World);
                        //第二个和第三个的炮塔隐藏
                        //battery2.gameObject.SetActive(false);
                        //battery3.gameObject.SetActive(false);
                        SetBatteryState(battery2, false);
                        SetBatteryState(battery3, false);
                    }
                    else
                    {
                        //print("第一个和第三个的炮塔无法进行组合");
                    }
                }
                else if (hasBattery1 && hasBattery2)
                {
                    //print("只有第一个坑和第二个坑有炮塔");
                    if (dic[index1] == dic[index2])
                    {
                        //print("第一个炮塔和第二个炮塔类型一样");
                        //生成Lv2的炮塔
                        List<GameObject> list = new List<GameObject>() { hole1.gameObject, hole2.gameObject };
                        groupObj = recoverBatteryDataBase.SpawnGroup(list, BatteryDataConfigTable.dic[dic[index1]], hole1, 2);
                        //groupObj.transform.Translate(groupObj.transform.forward * 3, Space.World);
                        //第一个和第二个的炮塔隐藏
                        //battery1.gameObject.SetActive(false);
                        //battery2.gameObject.SetActive(false);
                        SetBatteryState(battery1, false);
                        SetBatteryState(battery2, false);
                    }
                    else
                    {
                        //print("第一个和第二个的炮塔类型不一样");
                    }
                }
                else if (hasBattery1 && hasBattery3)
                {
                    //print("只有第一个坑和第三个坑有炮塔");
                    //print("第一个和第三个的炮塔无法进行组合");
                }
                else if (hasBattery2 && hasBattery3)
                {
                    //print("只有第二个坑和第三个坑有炮塔");
                    if (dic[index2] == dic[index3])
                    {
                        //print("第二个炮塔和第三个炮塔类型一样");
                        //生成Lv2的炮塔
                        List<GameObject> list = new List<GameObject>() { hole2.gameObject, hole3.gameObject };
                        groupObj = recoverBatteryDataBase.SpawnGroup(list, BatteryDataConfigTable.dic[dic[index2]], hole2, 2);
                        //groupObj.transform.Translate(groupObj.transform.forward * 3, Space.World);
                        //第二个和第三个的炮塔隐藏
                        //battery2.gameObject.SetActive(false);
                        //battery3.gameObject.SetActive(false);
                        SetBatteryState(battery2, false);
                        SetBatteryState(battery3, false);
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

            Dictionary<int, BatteryType> dic = recoverBatteryDataBase.hasHoleWithTypeDic;
            if (dic != null && dic.Count > 0)
            {
                hole1 = recoverBatteryDataBase.holeDic[index1].transform;
                hole2 = recoverBatteryDataBase.holeDic[index2].transform;

                //print("之前的组合炮塔销毁");
                recoverBatteryDataBase.DestoryGroupBattery(groupObj);
                if (hole1.childCount > 2)
                {
                    //print("显示第一个炮塔");
                    battery1 = hole1.GetChild(2);
                    //battery1.gameObject.SetActive(true);
                    SetBatteryState(battery1, true);
                }
                if (hole2.childCount > 2)
                {
                    //print("显示第二个炮塔");
                    battery2 = hole2.GetChild(2);
                    //battery2.gameObject.SetActive(true);
                    SetBatteryState(battery2, true);
                }

                bool hasBattery1 = dic.ContainsKey(index1);
                bool hasBattery2 = dic.ContainsKey(index2);
                if (hasBattery1 && hasBattery2)
                {
                    //print("两个坑都有炮塔");
                    if (dic[index1] == dic[index2])
                    {
                        if ((index1 >= 1 && index1 <= 8 && index2 >= 9 && index2 <= 24) || (index1 >= 9 && index1 <= 24 && index2 >= 25 && index2 <= 48))
                        {
                            //print("两个炮塔类型都一样");
                            //生成Lv2的炮塔
                            List<GameObject> list = new List<GameObject>() { hole1.gameObject, hole2.gameObject };
                            groupObj = recoverBatteryDataBase.SpawnGroup(list, BatteryDataConfigTable.dic[dic[index1]], hole1, 2);
                            //groupObj.transform.Translate(groupObj.transform.forward * 3, Space.World);
                            //两个炮塔隐藏                          
                            //battery1.gameObject.SetActive(false);
                            //battery2.gameObject.SetActive(false);
                            SetBatteryState(battery1, false);
                            SetBatteryState(battery2, false);
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
                else if (hasBattery1 || hasBattery2)
                {
                    //print("只有一个坑有炮塔");
                }
                else
                {
                    //print("没有一个坑有炮塔");
                }
            }
        }
        else if (list.Count == 1)
        {
            //print("只有一个坑");
            Transform hole1 = list[0];
            int index1 = hole1.GetComponent<BuildConfig>().index;

            Dictionary<int, BatteryType> dic = recoverBatteryDataBase.hasHoleWithTypeDic;
            if (dic != null && dic.Count > 0)
            {
                hole1 = recoverBatteryDataBase.holeDic[index1].transform;
                //print("之前的组合炮塔销毁");
                recoverBatteryDataBase.DestoryGroupBattery(groupObj);
                if (hole1.childCount > 2)
                {
                    //print("显示第一个炮塔");
                    battery1 = hole1.GetChild(2);
                    //battery1.gameObject.SetActive(true);
                    SetBatteryState(battery1, true);
                }
            }
        }
    }

    void OrderIndex(List<int> indexList)
    {
        indexList.Sort((a, b) => { return a < b ? -1 : (a > b ? 1 : 0); });
    }

    void SetBatteryState(Transform battery, bool show)
    {
        if (battery)
        {
            BatteryParent bp = battery.GetComponent<BatteryParent>();
            if (bp != null)
            {
                if (!show)
                {
                    battery.localScale = Vector3.zero;
                    //Scene currentScene = SceneManager.GetActiveScene();
                    //if (currentScene.name.Equals("Fight"))
                    //{
                    //    Animator anima = battery.GetComponent<Animator>();
                    //    if (anima != null)
                    //    {
                    //        AnimatorStateInfo animatorStateInfo = anima.GetCurrentAnimatorStateInfo(0);
                    //        if (!animatorStateInfo.IsName("Base Layer.houseBX"))
                    //        {
                    //            anima.SetTrigger("houseBX");
                    //        }
                    //    }
                    //}
                    bp.enabled = false;
                }
                else
                {
                    battery.localScale = Vector3.one;
                    //Scene currentScene = SceneManager.GetActiveScene();
                    //if (currentScene.name.Equals("Fight"))
                    //{
                    //    Animator anima = battery.GetComponent<Animator>();
                    //    if (anima != null)
                    //    {
                    //        AnimatorStateInfo animatorStateInfo = anima.GetCurrentAnimatorStateInfo(0);
                    //        if (!animatorStateInfo.IsName("Base Layer.batteryBX"))
                    //        {
                    //            anima.SetTrigger("batteryBX");
                    //        }
                    //    }
                    //}
                    bp.enabled = true;
                }
            }
        }
    }
}
