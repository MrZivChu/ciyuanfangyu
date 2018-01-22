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
        buildSelectedResource = Resources.Load("UI/BuildSelected/BuildSelected");
        buildOperateResource = Resources.Load("UI/BuildSelected/BuildOperate");
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

    bool isChangePosition = false;
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
                                if (bc.currentBP == null)
                                {
                                    if (isChangePosition)
                                    {
                                        //BuildSelectedScript.SpawnBattery();
                                        isChangePosition = false;
                                    }
                                    else
                                    {
                                        uiIsShow = true;
                                        GameObject go = Instantiate(buildSelectedResource) as GameObject;
                                        Utils.SpawnUIObj(go.transform, parent);
                                        go.GetComponent<BuildSelected>().targetBuild = hit.transform.gameObject;
                                    }
                                }
                                else
                                {
                                    if (isChangePosition)
                                    {

                                        isChangePosition = false;
                                    }
                                    else
                                    {
                                        uiIsShow = true;
                                        GameObject go = Instantiate(buildOperateResource) as GameObject;
                                        Utils.SpawnUIObj(go.transform, parent);
                                        BuildOperate BuildOperateScript = go.GetComponent<BuildOperate>();
                                        BuildOperateScript.currentBattery = bc.currentBP;
                                        BuildOperateScript.buildConfig = bc;
                                        BuildOperateScript.battleTarget = hit.transform.GetChild(0).gameObject;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
