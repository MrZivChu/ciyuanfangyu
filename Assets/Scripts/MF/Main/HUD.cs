using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Button technologyDevelopmentBtn;
    public Button recruitBtn;
    public Button urgentDefenseBtn;
    public Button fortressBtn;

    public GameObject TechnologyDevelopmentPanel;
    public GameObject RecruitPanel;
    public GameObject FortressPanel;

    void Start()
    {
        TechnologyDevelopmentPanel.SetActive(false);
        RecruitPanel.SetActive(false);
        EventTriggerListener.Get(technologyDevelopmentBtn.gameObject).onClick = TechnologyDevelopmentClick;
        EventTriggerListener.Get(recruitBtn.gameObject).onClick = RecruitClick;
        EventTriggerListener.Get(urgentDefenseBtn.gameObject).onClick = UrgentDefenseClick;
        EventTriggerListener.Get(fortressBtn.gameObject).onClick = FortressClick;
    }

    void TechnologyDevelopmentClick(GameObject go, object data)
    {
        TechnologyDevelopmentPanel.SetActive(true);
        RecruitPanel.SetActive(false);
    }

    void RecruitClick(GameObject go, object data)
    {
        TechnologyDevelopmentPanel.SetActive(false);
        RecruitPanel.SetActive(true);
    }

    void UrgentDefenseClick(GameObject go, object data)
    {
        Loading.sceneName = "Fight";
        SceneManager.LoadScene("Loading");
    }

    void FortressClick(GameObject go, object data)
    {
        FortressPanel.SetActive(true);
    }
}
