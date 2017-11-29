using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameJsonDataHelper
{
    static string saveFile = Application.persistentDataPath + "/batteryData.data";

    public static List<BatteryData> ReadData()
    {
        if (File.Exists(saveFile))
        {
            string jsonData = File.ReadAllText(saveFile);
            list = JsonMapper.ToObject<List<BatteryData>>(jsonData);
        }
        return list;
    }

    public static void AddData(BatteryData bt)
    {
        if (!list.Contains(bt))
        {
            list.Add(bt);
        }
    }

    static List<BatteryData> list = new List<BatteryData>();
    public static void WriteData()
    {
        string content = JsonMapper.ToJson(list);
        if (!File.Exists(saveFile))
        {
            using (File.Create(saveFile))
            { }
        }
        File.WriteAllText(saveFile, content);
    }
}

public class BatteryData
{
    public int index;
    public BatteryType batteryType;
    public int batteryLevel;
}
