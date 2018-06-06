using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryCell : MonoBehaviour
{

    public BatteryConfigInfo batteryInfo;

    public Text nameText;
    public Text bloodText;
    public Text attackText;
    public Text rangeText;
    public Text mwText;

    public Image shape1;
    public Image shape2;
    public Image shape3;

    public GameObject starParent;

    private void Start()
    {
        setStar();
        nameText.text = batteryInfo.batteryName;
        bloodText.text = batteryInfo.blood.ToString();
        attackText.text = batteryInfo.attackValue.ToString();
        rangeText.text = batteryInfo.maxAttackDistance.ToString();
        mwText.text = batteryInfo.MW.ToString();

        Sprite sprite1 = Resources.Load<Sprite>(batteryInfo.icon + "01");
        Sprite sprite2 = Resources.Load<Sprite>(batteryInfo.icon + "02");
        Sprite sprite3 = Resources.Load<Sprite>(batteryInfo.icon + "03");

        shape1.sprite = sprite1 == null ? Resources.Load<Sprite>("UI/Battery/CannonBattery01") : sprite1;
        shape2.sprite = sprite2 == null ? Resources.Load<Sprite>("UI/Battery/CannonBattery02") : sprite2;
        shape3.sprite = sprite3 == null ? Resources.Load<Sprite>("UI/Battery/CannonBattery03") : sprite3;
    }

    void setStar()
    {
        int childCount = starParent.transform.childCount;
        int startLevel = batteryInfo.starLevel;
        for (int i = 0; i < startLevel; i++)
        {
            starParent.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int j = startLevel; j < childCount; j++)
        {
            starParent.transform.GetChild(j).gameObject.SetActive(false);
        }
    }
}
