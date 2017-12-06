using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uTools;

public class BuildOperate : MonoBehaviour
{
    public BatteryParent currentBattery;
    public Button closeBtn;
    public Button strengthenBtn;//demo版本强化先不做
    public Button levelupBtn;
    public Button modifyBtn;
    public Button destoryBtn;
    public Button moveBtn;

    public Text nameText;
    public GameObject starParent;
    public Image icon;

    bool isOpen = false;
    void Start()
    {
        closeBtn.onClick.AddListener(CloseClick);
        strengthenBtn.onClick.AddListener(StrengthenClick);
        levelupBtn.onClick.AddListener(LevelUpClick);
        modifyBtn.onClick.AddListener(ModifyClick);
        destoryBtn.onClick.AddListener(DestoryClick);
        moveBtn.onClick.AddListener(MoveClick);

        InitData();
    }

    void InitData()
    {
        if (currentBattery != null)
        {
            nameText.text = currentBattery.batteryName;
            int star = currentBattery.starLevel;
            for (int i = 0; i < star; i++)
            {
                starParent.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = star; i < starParent.transform.childCount; i++)
            {
                starParent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void CloseClick()
    {
        if (leftInfoPanel.activeSelf)
        {
            uTweenPosition[] tuTweenPosition = leftInfoPanel.GetComponents<uTweenPosition>();
            tuTweenPosition[1].Reset();
        }
        else
        {
            PlayBuildOperateTween(true);
        }
    }

    void StrengthenClick()
    {
        CloseClick();
    }

    void LevelUpClick()
    {
        CloseClick();
    }

    void ModifyClick()
    {
        CloseClick();
    }

    void DestoryClick()
    {
        CloseClick();
    }

    void MoveClick()
    {
        CloseClick();
    }

    public void PlayBuildOperateTween(bool isHide)
    {
        uTweenPosition[] tuTweenPosition = transform.GetComponents<uTweenPosition>();
        if (isHide)
        {
            //退出
            tuTweenPosition[2].Reset();
            tuTweenPosition[3].Reset();
        }
        else
        {
            //进入
            isOpen = true;
            tuTweenPosition[0].Reset();
            tuTweenPosition[1].Reset();
        }
    }

    public void BuildOperatePanelHideOver()
    {
        isOpen = false;
        leftInfoPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void BuildOperatePanelShowOver()
    {
        LookInfo(null, null);
    }

    public void LeftInfoPanelHideOver()
    {
        PlayBuildOperateTween(true);
    }

    public GameObject leftInfoPanel;
    void LookInfo(GameObject go, object param)
    {
        if (!leftInfoPanel.activeSelf)
        {
            leftInfoPanel.SetActive(true);
            uTweenPosition[] tuTweenPosition = leftInfoPanel.GetComponents<uTweenPosition>();
            tuTweenPosition[0].Reset();
        }
    }


}
