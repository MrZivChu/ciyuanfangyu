using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uTools;

public class BuildOperate : MonoBehaviour
{
    public GameObject battleTarget;
    public BuildConfig buildConfig;
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

    public bool isOpen = false;
    void Start()
    {
        closeBtn.onClick.AddListener(CloseClick);
        strengthenBtn.onClick.AddListener(StrengthenClick);
        levelupBtn.onClick.AddListener(LevelUpClick);
        modifyBtn.onClick.AddListener(ModifyClick);
        destoryBtn.onClick.AddListener(DestoryClick);
        moveBtn.onClick.AddListener(MoveClick);
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
        if (LeftPanelIsOpen)
        {
            LeftPanelIsOpen = false;
            uTweenPosition[] tuTweenPosition = leftInfoPanel.GetComponents<uTweenPosition>();
            tuTweenPosition[1].Reset();
            return;
        }
        if (isOpen)
        {
            isOpen = false;
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
        DestoryBattery(buildConfig, battleTarget, currentBattery);
        CloseClick();
    }

    public void DestoryBattery(BuildConfig tBuildConfig, GameObject tBattleTarget, BatteryParent tCurrentBattery)
    {
        tBuildConfig.currentBP = null;
        Destroy(tBattleTarget);
        BatteryData bd = new BatteryData();
        bd.batteryLevel = 1;
        bd.batteryType = tCurrentBattery.battleType;
        bd.index = tBuildConfig.index;
        GameJsonDataHelper.DeleteBatteryData(bd);
    }

    void MoveClick()
    {

        CloseClick();
    }

    bool LeftPanelIsOpen = false;
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
            InitData();
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
        if (!LeftPanelIsOpen)
        {
            LeftPanelIsOpen = true;
            leftInfoPanel.SetActive(true);
            uTweenPosition[] tuTweenPosition = leftInfoPanel.GetComponents<uTweenPosition>();
            tuTweenPosition[0].Reset();
        }
    }


}
