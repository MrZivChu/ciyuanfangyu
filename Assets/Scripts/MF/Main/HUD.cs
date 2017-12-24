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
    public Button fortressBtn;
    public Button headBtn;
    public Button settingBtn;

    public GameObject TechnologyDevelopmentPanel;
    public GameObject RecruitPanel;
    public GameObject FortressPanel;
    public GameObject PersonInfoPanel;
    public GameObject SettingPanel;

    void Start()
    {
        TechnologyDevelopmentPanel.SetActive(false);
        RecruitPanel.SetActive(false);
        EventTriggerListener.Get(technologyDevelopmentBtn.gameObject).onClick = TechnologyDevelopmentClick;
        EventTriggerListener.Get(recruitBtn.gameObject).onClick = RecruitClick;
        EventTriggerListener.Get(urgentDefenseBtn.gameObject).onClick = UrgentDefenseClick;
        EventTriggerListener.Get(fortressBtn.gameObject).onClick = FortressClick;
        EventTriggerListener.Get(headBtn.gameObject).onClick = HeadBtnClick;
        EventTriggerListener.Get(settingBtn.gameObject).onClick = SettingBtnClick;

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
        TechnologyDevelopmentPanel.SetActive(true);
        RecruitPanel.SetActive(false);
        ResetTweenToBack();
    }

    void RecruitClick(GameObject go, object data)
    {
        TechnologyDevelopmentPanel.SetActive(false);
        RecruitPanel.SetActive(true);
        ResetTweenToBack();
    }

    void UrgentDefenseClick(GameObject go, object data)
    {
        Loading.sceneName = "Fight";
        SceneManager.LoadScene("Loading");
    }

    void FortressClick(GameObject go, object data)
    {
        FortressPanel.SetActive(true);
        ResetTweenToBack();
    }

    void HeadBtnClick(GameObject go, object data)
    {
        PersonInfoPanel.SetActive(true);
        ResetTweenToBack();
    }

    void SettingBtnClick(GameObject go, object data)
    {
        SettingPanel.SetActive(true);
        ResetTweenToBack();
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
