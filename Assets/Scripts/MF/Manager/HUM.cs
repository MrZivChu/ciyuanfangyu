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
        GameJsonDataHelper.WriteBatteryData();
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }

    void InitMercenary()
    {
        if (BaseDataLibrary.battleMercenaryList != null && BaseDataLibrary.battleMercenaryList.Count > 0)
        {
            for (int i = 0; i < BaseDataLibrary.battleMercenaryList.Count; i++)
            {
                Mercenary mer = MercenaryDataConfigTable.MercenaryList.Find(it => it.ID == BaseDataLibrary.battleMercenaryList[i]);
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

        int count = BaseDataLibrary.battleMercenaryList == null ? mercenaryGameobjectList.Count : BaseDataLibrary.battleMercenaryList.Count;
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

    public bool isChangePosition = false;
    public Transform currentHitObj;
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
                                if (isChangePosition)
                                {
                                    isChangePosition = false;
                                    ChangePosition(currentHitObj, hit.transform);
                                    currentHitObj = hit.transform;
                                }
                                else
                                {
                                    currentHitObj = hit.transform;
                                    if (bc.currentBP == null)
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
                                        buildOperateScript.currentBattery = bc.currentBP;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    public void SpawnBattery(BatteryConfigInfo info, Transform hole)
    {
        if (hole != null && info != null)
        {
            Object obj = Resources.Load(info.battleType.ToString() + "Lv1");
            if (obj != null)
            {
                GameObject tempGO = Instantiate(obj) as GameObject;
                tempGO.transform.parent = hole;
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
                bp.starLevel = info.starLevel;
                bp.wood = info.wood;
                BuildConfig bc = hole.GetComponent<BuildConfig>();
                bc.currentBP = bp;
                bc.batteryConfigInfo = info;

                BatteryData bd = new BatteryData();
                bd.batteryLevel = 1;
                bd.batteryType = info.battleType;
                bd.index = bc.index;
                GameJsonDataHelper.AddBatteryData(bd);
            }
        }
    }

    public void DestoryBattery(Transform hold)
    {
        if (hold != null)
        {
            BuildConfig tBuildConfig = hold.GetComponent<BuildConfig>();
            if (tBuildConfig != null && tBuildConfig.currentBP != null)
            {
                GameObject tBattleTarget = hold.GetChild(0).gameObject;
                tBuildConfig.currentBP = null;
                Destroy(tBattleTarget);
                BatteryData bd = new BatteryData();
                bd.batteryLevel = 1;
                bd.index = tBuildConfig.index;
                GameJsonDataHelper.DeleteBatteryData(bd);
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
        bool needSpawn1 = b1.currentBP != null;
        bool needSpawn2 = b2.currentBP != null;

        DestoryBattery(firstHole);
        DestoryBattery(secondHole);

        if (needSpawn2)
            SpawnBattery(b2.batteryConfigInfo, firstHole);
        if (needSpawn1)
            SpawnBattery(b1.batteryConfigInfo, secondHole);
    }
}