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
        string content = JsonMapper.ToJson(batteryDatalist);
        if (!File.Exists(saveFile))
        {
            using (File.Create(saveFile))
            { }
        }
        File.WriteAllText(saveFile, content);
    }

    public static void AddBatteryData(BatteryData bt)
    {
        if (!batteryDatalist.Contains(bt))
        {
            batteryDatalist.Add(bt);
        }
    }

    public static void DeleteBatteryData(BatteryData bt)
    {
        if (batteryDatalist.Contains(bt))
        {
            batteryDatalist.Remove(bt);
        }
    }
}

public class BatteryData
{
    public int index;
    public BatteryType batteryType;
    public int batteryLevel;
}
