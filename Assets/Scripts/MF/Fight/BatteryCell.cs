using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryCell : MonoBehaviour
{

    public BatteryInfo batteryInfo;

    public Text nameText;
    public Text bloodText;
    public Text attackText;
    public Text rangeText;
    public Text mwText;

    public GameObject starParent;

    public Image icon;
    public Image model;

    private void Start()
    {
        setStar();
        nameText.text = batteryInfo.batteryName;
        bloodText.text = batteryInfo.blood.ToString();
        attackText.text = batteryInfo.attack.ToString();
        rangeText.text = batteryInfo.maxAttackDistance.ToString();
        mwText.text = batteryInfo.MW.ToString();

        icon.sprite = Resources.Load<Sprite>(batteryInfo.icon);
        model.sprite = Resources.Load<Sprite>(batteryInfo.model);

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
