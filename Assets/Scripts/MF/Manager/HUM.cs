using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uTools;

public class HUM : MonoBehaviour
{
    public Button backBtn;

    public Transform parent;
    Object buildSelectedResource;
    Object buildOperateResource;

    BuildSelected BuildSelectedScript;
    BuildOperate BuildOperateScript;

    public List<GameObject> mercenaryGameobjectList;
    private void Start()
    {
        buildSelectedResource = Resources.Load("UI/Manager/BuildSelected");
        buildOperateResource = Resources.Load("UI/Manager/BuildOperate");
        backBtn.onClick.AddListener(BackClick);
        InitMercenary();
    }

    void BackClick()
    {
        ServerDataHelper.UpdateServerBatteryData();
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }

    void InitMercenary()
    {
        if (LocalBaseData.battleMercenaryList != null && LocalBaseData.battleMercenaryList.Count > 0)
        {
            for (int i = 0; i < LocalBaseData.battleMercenaryList.Count; i++)
            {
                MercenaryConfigInfo mer = MercenaryDataConfigTable.MercenaryList.Find(it => it.ID == LocalBaseData.battleMercenaryList[i]);
                if (mer != null)
                {
                    GameObject go = mercenaryGameobjectList[i];
                    Sprite sprite = Resources.Load<Sprite>(mer.iconPath.Replace("zm", "zd"));
                    go.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                    go.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "LV" + mer.star;
                    go.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = mer.mercenaryName;
                    go.SetActive(true);
                }
            }
        }

        int count = LocalBaseData.battleMercenaryList == null ? mercenaryGameobjectList.Count : LocalBaseData.battleMercenaryList.Count;
        for (int i = count; i < mercenaryGameobjectList.Count; i++)
        {
            mercenaryGameobjectList[i].SetActive(false);
        }
    }

    public List<uTweenPosition> tweenPositions;
    public void PlayHumAnima()
    {
        if (tweenPositions != null && tweenPositions.Count > 0)
        {
            for (int i = 0; i < tweenPositions.Count; i++)
            {
                tweenPositions[i].Reset();
            }
        }
    }

    private float t1;
    private float t2;
    bool isClickUI = false;

    public static bool uiIsShow = false;

    public RecoverBatteryData recoverBatteryDataScript;

    public bool isChangePosition = false;
    public Transform currentHitHoleObj;
    public Transform preHitHoleObj;
    private void Update()
    {
        if (uiIsShow)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isClickUI = Utils.CheckGuiRaycastObjects();
            t1 = Time.realtimeSinceStartup;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isClickUI)
            {
                t2 = Time.realtimeSinceStartup;
                if (t2 - t1 < 0.15f)
                {
                    RaycastHit hit;
                    Vector2 mousePosition = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                    int layMask = 1 << LayerMask.NameToLayer("Build");
                    if (Physics.Raycast(ray, out hit, 1000, layMask))
                    {
                        if (hit.transform.CompareTag("build"))
                        {
                            BuildConfig bc = hit.transform.GetComponent<BuildConfig>();
                            if (bc != null)
                            {
                                currentHitHoleObj = hit.transform;
                                if (isChangePosition)
                                {
                                    isChangePosition = false;
                                    ChangePosition(preHitHoleObj, currentHitHoleObj);
                                }
                                else
                                {
                                    if (!recoverBatteryDataScript.hasDic.ContainsKey(bc.index))
                                    {
                                        uiIsShow = true;
                                        GameObject go = Instantiate(buildSelectedResource) as GameObject;
                                        Utils.SpawnUIObj(go.transform, parent);
                                        BuildSelected buildSelected = go.GetComponent<BuildSelected>();
                                        buildSelected.humScript = this;
                                    }
                                    else
                                    {
                                        uiIsShow = true;
                                        GameObject go = Instantiate(buildOperateResource) as GameObject;
                                        Utils.SpawnUIObj(go.transform, parent);
                                        BuildOperate buildOperateScript = go.GetComponent<BuildOperate>();
                                        buildOperateScript.humScript = this;
                                        buildOperateScript.currentBattery = currentHitHoleObj.GetChild(2).gameObject.GetComponent<BatteryParent>();

                                        if (preBatteryObj != null)
                                        {
                                            Utils.SetObjectHighLight(preBatteryObj, false, Color.clear, Color.clear);
                                        }
                                        currentBatteryObj = currentHitHoleObj.GetChild(2).gameObject;
                                        preBatteryObj = currentBatteryObj;
                                        Utils.SetObjectHighLight(currentBatteryObj, true, new Color(1, 0, 0, 1), new Color(1, 1, 0, 1));
                                    }
                                }
                                preHitHoleObj = currentHitHoleObj;
                            }
                        }
                    }
                }
            }
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

    GameObject preBatteryObj;
    GameObject currentBatteryObj;
    public void SpawnBattery(BatteryConfigInfo info, Transform hole)
    {
        if (hole != null && info != null)
        {
            recoverBatteryDataScript.InstanceBatteryObj(info, hole);
            int index = hole.GetComponent<BuildConfig>().index;
            recoverBatteryDataScript.hasDic[index] = info.battleType;

            ServerBatteryData bd = new ServerBatteryData();
            bd.batteryLevel = 1;
            bd.batteryType = info.battleType;
            bd.index = index;
            ServerDataHelper.AddSingleServerBatteryData(bd);

            if (preBatteryObj != null)
            {
                Utils.SetObjectHighLight(preBatteryObj, false, Color.clear, Color.clear);
            }
            currentBatteryObj = hole.GetChild(2).gameObject;
            preBatteryObj = currentBatteryObj;
            Utils.SetObjectHighLight(currentBatteryObj, true, new Color(1, 0, 0, 1), new Color(1, 1, 0, 1));
            CheckGroup();
        }
    }

    public void DestoryBattery(Transform hold)
    {
        if (hold != null)
        {
            BuildConfig tBuildConfig = hold.GetComponent<BuildConfig>();
            if (tBuildConfig != null && recoverBatteryDataScript.hasDic.ContainsKey(tBuildConfig.index))
            {
                recoverBatteryDataScript.hasDic.Remove(tBuildConfig.index);

                GameObject tBattleTarget = hold.GetChild(2).gameObject;
                Destroy(tBattleTarget);
                ServerBatteryData bd = new ServerBatteryData();
                bd.batteryLevel = 1;
                bd.index = tBuildConfig.index;
                ServerDataHelper.DeleteSingleServerBatteryData(bd);
                CheckGroup();
            }
        }
    }

    public void ChangePosition(Transform firstHole, Transform secondHole)
    {
        if (firstHole == secondHole)
        {
            return;
        }
        BuildConfig b1 = firstHole.GetComponent<BuildConfig>();
        BuildConfig b2 = secondHole.GetComponent<BuildConfig>();
        bool needSpawn1 = recoverBatteryDataScript.hasDic.ContainsKey(b1.index);
        bool needSpawn2 = recoverBatteryDataScript.hasDic.ContainsKey(b2.index);

        BatteryType bt1 = needSpawn1 ? recoverBatteryDataScript.hasDic[b1.index] : BatteryType.None;
        BatteryType bt2 = needSpawn2 ? recoverBatteryDataScript.hasDic[b2.index] : BatteryType.None;

        DestoryBattery(firstHole);
        DestoryBattery(secondHole);

        if (needSpawn2)
            SpawnBattery(BatteryDataConfigTable.dic[bt2], firstHole);
        if (needSpawn1)
            SpawnBattery(BatteryDataConfigTable.dic[bt1], secondHole);
        CheckGroup();
    }
}