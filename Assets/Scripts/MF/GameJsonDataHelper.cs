using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameJsonDataHelper
{
    static string saveFile = Application.persistentDataPath + "/batteryData.data";
    static List<BatteryData> batteryDatalist = new List<BatteryData>();

    public static List<BatteryData> ReadBatteryData()
    {
        if (File.Exists(saveFile))
        {
            string jsonData = File.ReadAllText(saveFile);
            batteryDatalist = JsonMapper.ToObject<List<BatteryData>>(jsonData);
        }
        return batteryDatalist;
    }

    public static void WriteBatteryData()
    {
        if (batteryDatalist != null)
        {
            string content = JsonMapper.ToJson(batteryDatalist);
            if (!string.IsNullOrEmpty(content))
            {
                if (!File.Exists(saveFile))
                {
                    using (File.Create(saveFile))
                    { }
                }
                File.WriteAllText(saveFile, content);
            }
        }
    }

    public static void AddBatteryData(BatteryData bt)
    {
        int count = batteryDatalist.FindAll(item => { return item.index == bt.index; }).Count;
        if (count <= 0)
        {
            batteryDatalist.Add(bt);
        }
    }

    public static void DeleteBatteryData(BatteryData bt)
    {
        if (batteryDatalist != null && batteryDatalist.Count > 0)
        {
            for (int i = 0; i < batteryDatalist.Count; i++)
            {
                if (batteryDatalist[i].index == bt.index)
                {
                    batteryDatalist.Remove(batteryDatalist[i]);
                    return;
                }
            }
        }
    }

}
public class BatteryData
{
    public int index;
    public BatteryType batteryType;
    public int batteryLevel;
}
