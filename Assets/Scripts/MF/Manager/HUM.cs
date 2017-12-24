using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUM : MonoBehaviour
{
    public Button backBtn;
    public GameObject buildSelectedPanel;
    public GameObject buildOperatePanel;

    BuildSelected BuildSelectedScript;
    BuildOperate BuildOperateScript;
    private void Start()
    {
        BuildSelectedScript = buildSelectedPanel.GetComponent<BuildSelected>();
        BuildOperateScript = buildOperatePanel.GetComponent<BuildOperate>();
        backBtn.onClick.AddListener(BackClick);
    }

    void BackClick()
    {
        GameJsonDataHelper.WriteBatteryData();
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }

    private float t1;
    private float t2;
    bool isClickUI = false;

    bool isChangePosition = false;
    private void Update()
    {
        if (!BuildSelectedScript.isOpen && !BuildOperateScript.isOpen)
        {
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
                                            BuildSelectedScript.targetBuild = hit.transform.gameObject;
                                            buildSelectedPanel.SetActive(true);
                                            BuildSelectedScript.PlayBuildSelectedTween(false);
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
                                            BuildOperateScript.currentBattery = bc.currentBP;
                                            BuildOperateScript.buildConfig = bc;
                                            BuildOperateScript.battleTarget = hit.transform.GetChild(0).gameObject;
                                            buildOperatePanel.SetActive(true);
                                            BuildOperateScript.PlayBuildOperateTween(false);
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
}
