using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUM : MonoBehaviour
{
    public Button backBtn;
    public GameObject buildSelected;

    BuildSelected BuildSelectedScript;
    private void Start()
    {
        BuildSelectedScript = buildSelected.GetComponent<BuildSelected>();
        backBtn.onClick.AddListener(BackClick);
    }

    void BackClick()
    {
        GameJsonDataHelper.WriteData();
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }

    private float t1;
    private float t2;
    bool isClickUI = false;
    private void Update()
    {
        if (!BuildSelectedScript.isOpen)
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
                        if (Physics.Raycast(ray, out hit, layMask))
                        {
                            if (hit.transform.CompareTag("build"))
                            {
                                BuildConfig bc = hit.transform.GetComponent<BuildConfig>();
                                if (bc != null)
                                {
                                    if (bc.currentBP == null)
                                    {
                                        BuildSelectedScript.targetBuild = hit.transform.gameObject;
                                        buildSelected.SetActive(true);
                                        BuildSelectedScript.PlayBuildSelectedTween(false);
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
