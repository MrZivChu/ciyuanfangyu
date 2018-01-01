using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameJsonDataHelper
{
    //炮塔数据
    static string batteryDataSaveFile = Application.persistentDataPath + "/batteryData.data";
    static List<BatteryData> batteryDataList = new List<BatteryData>();

    public static List<BatteryData> ReadBatteryData()
    {
        if (File.Exists(batteryDataSaveFile))
        {
            string jsonData = File.ReadAllText(batteryDataSaveFile);
            batteryDataList = JsonMapper.ToObject<List<BatteryData>>(jsonData);
        }
        return batteryDataList;
    }

    public static void WriteBatteryData()
    {
        if (batteryDataList != null)
        {
            string content = JsonMapper.ToJson(batteryDataList);
            if (!string.IsNullOrEmpty(content))
            {
                if (!File.Exists(batteryDataSaveFile))
                {
                    using (File.Create(batteryDataSaveFile))
                    { }
                }
                File.WriteAllText(batteryDataSaveFile, content);
            }
        }
    }

    public static void AddBatteryData(BatteryData bt)
    {
        int count = batteryDataList.FindAll(item => { return item.index == bt.index; }).Count;
        if (count <= 0)
        {
            batteryDataList.Add(bt);
        }
    }

    public static void DeleteBatteryData(BatteryData bt)
    {
        if (batteryDataList != null && batteryDataList.Count > 0)
        {
            for (int i = 0; i < batteryDataList.Count; i++)
            {
                if (batteryDataList[i].index == bt.index)
                {
                    batteryDataList.Remove(batteryDataList[i]);
                    return;
                }
            }
        }
    }

    //佣兵数据处理
    static string mercenaryDataSaveFile = Application.persistentDataPath + "/mercenaryData.data";
    static List<int> mercenaryDataList = new List<int>();

    public static List<int> ReadMercenaryData()
    {
        if (File.Exists(mercenaryDataSaveFile))
        {
            string jsonData = File.ReadAllText(mercenaryDataSaveFile);
            mercenaryDataList = JsonMapper.ToObject<List<int>>(jsonData);
        }
        return mercenaryDataList;
    }

    public static void WriteMercenaryData()
    {
        if (mercenaryDataList != null)
        {
            string content = JsonMapper.ToJson(mercenaryDataList);
            if (!string.IsNullOrEmpty(content))
            {
                if (!File.Exists(mercenaryDataSaveFile))
                {
                    using (File.Create(mercenaryDataSaveFile))
                    { }
                }
                File.WriteAllText(mercenaryDataSaveFile, content);
            }
        }
    }

    public static void AddMercenaryData(int index)
    {
        int count = mercenaryDataList.FindAll(item => { return item == index; }).Count;
        if (count <= 0)
        {
            mercenaryDataList.Add(index);
        }
    }

    public static void DeleteMercenaryData(int index)
    {
        if (mercenaryDataList != null && mercenaryDataList.Count > 0)
        {
            for (int i = 0; i < mercenaryDataList.Count; i++)
            {
                if (mercenaryDataList[i] == index)
                {
                    mercenaryDataList.Remove(mercenaryDataList[i]);
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
