using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uTools;

public class HUD : MonoBehaviour
{
    public Button technologyDevelopmentBtn;
    public Button recruitBtn;
    public Button urgentDefenseBtn;
    public Button worldMapBtn;
    public Button headBtn;
    public Button settingBtn;

    public Button leftBtn;
    public Button rightBtn;
    public Scrollbar activityScrollbar;

    Object recruitResource;
    Object worldMapResource;
    Object personInfoResource;
    Object settingResource;
    Object mercenaryBagResource;
    public Transform parent;

    void Start()
    {
        recruitResource = Resources.Load("UI/Main/Recruit");
        worldMapResource = Resources.Load("UI/Main/WorldMap");
        personInfoResource = Resources.Load("UI/Main/PersonInfo");
        settingResource = Resources.Load("UI/Main/Setting");
        mercenaryBagResource = Resources.Load("UI/Mercenary/MercenaryBag");

        EventTriggerListener.Get(technologyDevelopmentBtn.gameObject).onClick = TechnologyDevelopmentClick;
        EventTriggerListener.Get(recruitBtn.gameObject).onClick = RecruitClick;
        EventTriggerListener.Get(urgentDefenseBtn.gameObject).onClick = UrgentDefenseClick;
        EventTriggerListener.Get(worldMapBtn.gameObject).onClick = WorldMapClick;
        EventTriggerListener.Get(headBtn.gameObject).onClick = HeadBtnClick;
        EventTriggerListener.Get(settingBtn.gameObject).onClick = SettingBtnClick;

        EventTriggerListener.Get(leftBtn.gameObject).onClick = LeftBtnClick;
        EventTriggerListener.Get(rightBtn.gameObject).onClick = RightBtnClick;

        GetTween();
    }


    public GameObject girlTweenAlphaObj;
    public GameObject topTweenPositionObj;
    public GameObject topRightTweenPositionObj;
    public GameObject shopTweenPositionObj;
    public GameObject levelTweenPositionObj;
    public GameObject leftTweenPositionObj;
    public GameObject rightTweenPositionObj;
    public GameObject bottomBtnsTweenPositionObj;
    public GameObject advertisementTweenScaleObj;

    uTweenAlpha[] girlTweenAlpha;
    uTweenPosition[] girlTweenPosition;
    uTweenPosition[] topTweenPosition;
    uTweenPosition[] topRightTweenPosition;
    uTweenPosition[] shopTweenPosition;
    uTweenPosition[] levelTweenPosition;
    uTweenPosition[] leftTweenPosition;
    uTweenAlpha[] leftTweenAlpha;
    uTweenPosition[] rightTweenPosition;
    uTweenAlpha[] rightTweenAlpha;
    uTweenPosition[] bottomBtnsTweenPosition;
    void GetTween()
    {
        girlTweenAlpha = girlTweenAlphaObj.GetComponents<uTweenAlpha>();
        girlTweenPosition = girlTweenAlphaObj.GetComponents<uTweenPosition>();
        topTweenPosition = topTweenPositionObj.GetComponents<uTweenPosition>();
        topRightTweenPosition = topRightTweenPositionObj.GetComponents<uTweenPosition>();
        shopTweenPosition = shopTweenPositionObj.GetComponents<uTweenPosition>();
        levelTweenPosition = levelTweenPositionObj.GetComponents<uTweenPosition>();
        leftTweenPosition = leftTweenPositionObj.GetComponents<uTweenPosition>();
        leftTweenAlpha = leftTweenPositionObj.GetComponents<uTweenAlpha>();
        rightTweenPosition = rightTweenPositionObj.GetComponents<uTweenPosition>();
        rightTweenAlpha = rightTweenPositionObj.GetComponents<uTweenAlpha>();
        bottomBtnsTweenPosition = bottomBtnsTweenPositionObj.GetComponents<uTweenPosition>();
    }

    void TechnologyDevelopmentClick(GameObject go, object data)
    {
        GameObject obj = Instantiate(mercenaryBagResource) as GameObject;
        Utils.SpawnUIObj(obj.transform, parent);
        obj.GetComponent<MercenaryBag>().hudScript = this;
        obj.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
        ResetTweenToBack();
    }

    void RecruitClick(GameObject go, object data)
    {
        GameObject obj = Instantiate(recruitResource) as GameObject;
        Utils.SpawnUIObj(obj.transform, parent);
        obj.GetComponent<Recruit>().hudScript = this;
        obj.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
        ResetTweenToBack();
    }

    void UrgentDefenseClick(GameObject go, object data)
    {
        Loading.sceneName = "Fight";
        SceneManager.LoadScene("Loading");
    }

    void WorldMapClick(GameObject go, object data)
    {
        GameObject obj = Instantiate(worldMapResource) as GameObject;
        Utils.SpawnUIObj(obj.transform, parent);
        obj.GetComponent<WorldMap>().hudScript = this;
        obj.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
        ResetTweenToBack();
    }

    void HeadBtnClick(GameObject go, object data)
    {
        GameObject obj = Instantiate(personInfoResource) as GameObject;
        Utils.SpawnUIObj(obj.transform, parent);
        obj.GetComponent<PersonInfo>().hudScript = this;
        obj.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
        ResetTweenToBack();
    }

    void SettingBtnClick(GameObject go, object data)
    {
        GameObject obj = Instantiate(settingResource) as GameObject;
        Utils.SpawnUIObj(obj.transform, parent);
        obj.GetComponent<Setting>().hudScript = this;
        obj.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
        ResetTweenToBack();
    }

    void LeftBtnClick(GameObject go, object data)
    {
        activityScrollbar.value = 0;
    }

    void RightBtnClick(GameObject go, object data)
    {
        activityScrollbar.value = 1;
    }

    public void ResetTweenToBack()
    {
        girlTweenAlpha[1].Reset();
        girlTweenPosition[1].Reset();
        topTweenPosition[1].Reset();
        topRightTweenPosition[1].Reset();
        shopTweenPosition[1].Reset();
        levelTweenPosition[1].Reset();
        leftTweenPosition[1].Reset();
        leftTweenAlpha[1].Reset();
        rightTweenPosition[1].Reset();
        rightTweenAlpha[1].Reset();
        bottomBtnsTweenPosition[1].Reset();
    }

    public void ResetTweenPlay()
    {
        girlTweenAlpha[0].Reset();
        girlTweenPosition[0].Reset();
        topTweenPosition[0].Reset();
        topRightTweenPosition[0].Reset();
        shopTweenPosition[0].Reset();
        levelTweenPosition[0].Reset();
        leftTweenPosition[0].Reset();
        leftTweenAlpha[0].Reset();
        rightTweenPosition[0].Reset();
        rightTweenAlpha[0].Reset();
        bottomBtnsTweenPosition[0].Reset();
    }
}
