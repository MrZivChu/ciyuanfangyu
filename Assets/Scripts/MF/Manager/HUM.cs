using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uTools;

public class HUM : MonoBehaviour
{
    public Button backBtn;

    public Transform uiParent;
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

    public RecoverBatteryDataBase recoverBatteryDataBase;

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
                            if (bc != null && bc.index < 49)
                            {
                                currentHitHoleObj = hit.transform;
                                if (isChangePosition)
                                {
                                    isChangePosition = false;
                                    ChangePosition(preHitHoleObj, currentHitHoleObj);
                                }
                                else
                                {
                                    if (!recoverBatteryDataBase.hasHoleWithTypeDic.ContainsKey(bc.index))
                                    {
                                        uiIsShow = true;
                                        GameObject go = Instantiate(buildSelectedResource) as GameObject;
                                        Utils.SpawnUIObj(go.transform, uiParent);
                                        BuildSelected buildSelected = go.GetComponent<BuildSelected>();
                                        buildSelected.humScript = this;
                                    }
                                    else
                                    {
                                        uiIsShow = true;
                                        GameObject go = Instantiate(buildOperateResource) as GameObject;
                                        Utils.SpawnUIObj(go.transform, uiParent);
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

    GameObject preBatteryObj;
    GameObject currentBatteryObj;
    void PlaceBattery(BatteryConfigInfo info, Transform hole)
    {
        if (hole != null && info != null)
        {
            currentBatteryObj = recoverBatteryDataBase.SpawnBattery(info, hole);

            //currentBatteryObj = hole.GetChild(2).gameObject;
            ChangeHighLight();
        }
    }

    public void StartSpawnBattery(BatteryConfigInfo info, Transform hole)
    {
        PlayBuildOverAnima(hole);
        tempInfo = info;
        tempHole = hole;
        Invoke("buildOVerPlaceBattery", 2f);
    }

    public void buildOVerPlaceBattery()
    {
        if (tempInfo != null && tempHole != null)
        {
            currentBatteryObj = recoverBatteryDataBase.SpawnBattery(tempInfo, tempHole);
            //currentBatteryObj = tempHole.GetChild(2).gameObject;

            BatteryParent bp = currentBatteryObj.GetComponent<BatteryParent>();
            if (bp != null)
            {
                bp.ShowBuildOverCanvas();
            }
            ChangeHighLight();
        }
        if (tempBuildOverTarget != null)
        {
            Destroy(tempBuildOverTarget);
        }
    }

    GameObject tempBuildOverTarget;
    BatteryConfigInfo tempInfo;
    Transform tempHole;
    void PlayBuildOverAnima(Transform hole)
    {
        Object obj = Resources.Load("BuildOver");
        tempBuildOverTarget = Instantiate(obj) as GameObject;
        tempBuildOverTarget.transform.parent = hole;
        tempBuildOverTarget.transform.localPosition = Vector3.zero;

        Vector3 vv = Vector3.zero;
        vv.y = tempBuildOverTarget.transform.position.y;
        tempBuildOverTarget.transform.LookAt(vv);
        tempBuildOverTarget.transform.Rotate(Vector3.up, 180);
    }

    void ChangeHighLight()
    {
        if (preBatteryObj != null)
        {
            Utils.SetObjectHighLight(preBatteryObj, false, Color.clear, Color.clear);
        }
        Utils.SetObjectHighLight(currentBatteryObj, true, new Color(1, 0, 0, 1), new Color(1, 1, 0, 1));
        preBatteryObj = currentBatteryObj;
    }

    public void DestoryBattery(Transform hold)
    {
        if (hold != null)
        {
            recoverBatteryDataBase.DestoryBattery(hold);
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
        bool needSpawn1 = recoverBatteryDataBase.hasHoleWithTypeDic.ContainsKey(b1.index);
        bool needSpawn2 = recoverBatteryDataBase.hasHoleWithTypeDic.ContainsKey(b2.index);

        BatteryType bt1 = needSpawn1 ? recoverBatteryDataBase.hasHoleWithTypeDic[b1.index] : BatteryType.None;
        BatteryType bt2 = needSpawn2 ? recoverBatteryDataBase.hasHoleWithTypeDic[b2.index] : BatteryType.None;

        DestoryBattery(firstHole);
        DestoryBattery(secondHole);

        if (needSpawn2)
            PlaceBattery(BatteryDataConfigTable.dic[bt2], firstHole);
        if (needSpawn1)
            PlaceBattery(BatteryDataConfigTable.dic[bt1], secondHole);

    }
}