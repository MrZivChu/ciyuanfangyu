using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uTools;

public class BuildOperate : MonoBehaviour
{

    public Button closeBtn;
    public Button strengthenBtn;//demo版本强化先不做
    public Button levelupBtn;
    public Button modifyBtn;
    public Button destoryBtn;
    public Button moveBtn;

    public BatteryParent currentBattery;
    public HUM humScript;

    public Text nameText;
    public Text descText;
    public Text bloodText;
    public Text attackText;
    public Text rangeText;
    public GameObject starParent;
    public Image icon;

    void Start()
    {
        InitData();
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
            descText.text = currentBattery.desc;
            bloodText.text = currentBattery.blood.ToString();
            attackText.text = currentBattery.attackValue.ToString();
            rangeText.text = currentBattery.maxAttackDistance.ToString();
            icon.sprite = Resources.Load<Sprite>(currentBattery.icon + "01");
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
        Destroy(gameObject);
        HUM.uiIsShow = false;
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
        humScript.DestoryBattery(humScript.currentHitHoleObj);
        CloseClick();
    }

    void MoveClick()
    {
        humScript.isChangePosition = true;
        CloseClick();
    }
}
