using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uTools;

public class WorldMap : MonoBehaviour
{
    public HUD hudScript;
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;

    public Button backbtn;

    GameObject preMap;

    private void Start()
    {
        GameObject title1 = map1.transform.GetChild(1).gameObject;
        GameObject title2 = map2.transform.GetChild(1).gameObject;
        GameObject title3 = map3.transform.GetChild(1).gameObject;
        GameObject title4 = map4.transform.GetChild(1).gameObject;

        GameObject icon1 = map1.transform.GetChild(0).gameObject;
        GameObject icon2 = map2.transform.GetChild(0).gameObject;
        GameObject icon3 = map3.transform.GetChild(0).gameObject;
        GameObject icon4 = map4.transform.GetChild(0).gameObject;

        EventTriggerListener.Get(title1).onClick = ClickMap;
        EventTriggerListener.Get(title2).onClick = ClickMap;
        EventTriggerListener.Get(title3).onClick = ClickMap;
        EventTriggerListener.Get(title4).onClick = ClickMap;

        EventTriggerListener.Get(icon1).onClick = EnterMap;
        EventTriggerListener.Get(icon2).onClick = EnterMap;
        EventTriggerListener.Get(icon3).onClick = EnterMap;
        EventTriggerListener.Get(icon4).onClick = EnterMap;

        backbtn.onClick.AddListener(()=> {
            hudScript.ResetTweenPlay();
            Destroy(gameObject);
        });

    }

    void ClickMap(GameObject obj, object param)
    {
        if (obj != preMap)
        {
            if (preMap != null)
            {
                Transform ticon = preMap.transform.parent.GetChild(0);
                ticon.GetComponent<uTweenAlpha>().enabled = false;
                ticon.GetComponent<Image>().color = Color.white;

                Transform tdata = preMap.transform.parent.GetChild(2);
                tdata.localScale = Vector3.zero;
                tdata.gameObject.SetActive(false);
                Vector3 pos = Vector3.zero;
                pos.x = 160;
                tdata.GetComponent<RectTransform>().anchoredPosition = pos;
            }

            Transform icon = obj.transform.parent.GetChild(0);
            icon.GetComponent<uTweenAlpha>().enabled = true;
            Transform data = obj.transform.parent.GetChild(2);
            data.localScale = Vector3.one;
            data.gameObject.SetActive(true);
            uTweenAlpha tuTweenAlpha = data.GetComponent<uTweenAlpha>();
            uTweenPosition tuTweenPosition = data.GetComponent<uTweenPosition>();
            tuTweenAlpha.Reset();
            tuTweenPosition.Reset();
            preMap = obj;
        }
    }

    void EnterMap(GameObject obj, object param)
    {
        Loading.sceneName = "Manager";
        SceneManager.LoadScene("Loading");
    }

}
