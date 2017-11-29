using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftInfo : MonoBehaviour
{
    public Text num1Text;
    public Text num2Text;
    public Text contentText;

    public void SetData(BatteryInfo batteryinfo)
    {
        num1Text.text = batteryinfo.wood.ToString();
        num2Text.text = batteryinfo.MW.ToString();
        contentText.text = batteryinfo.desc;
    }
}
