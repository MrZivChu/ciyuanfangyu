using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 此类类似于服务端
/// </summary>
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


    //基础数据处理
    static string baseDataSaveFile = Application.persistentDataPath + "/baseData.data";
    static ServerBaseDataLibrary baseData = new ServerBaseDataLibrary();

    public static ServerBaseDataLibrary ReadBaseData()
    {
        if (File.Exists(baseDataSaveFile))
        {
            string jsonData = File.ReadAllText(baseDataSaveFile);
            baseData = JsonMapper.ToObject<ServerBaseDataLibrary>(jsonData);
        }
        return baseData;
    }

    public static void WriteBaseData()
    {
        if (baseData != null)
        {
            string content = JsonMapper.ToJson(baseData);
            if (!string.IsNullOrEmpty(content))
            {
                if (!File.Exists(baseDataSaveFile))
                {
                    using (File.Create(baseDataSaveFile))
                    { }
                }
                File.WriteAllText(baseDataSaveFile, content);
            }
        }
    }

    public static void UpdateBaseDataMusicVolume(double value)
    {
        baseData.musicVolume = value;
    }

    public static void UpdateBaseDataMusicIndex(int index)
    {
        baseData.musicIndex = index;
    }

}

public class ServerBaseDataLibrary
{
    public int coins;
    public int diamond;
    public int people;
    public double happinessDegree;

    //操作员的专注度（当专注度很高的时候，操作员能够轻易使用它们的特别技能，没完成一次出击任务，专注度就会提升，如果操作员没有参与任务的话，专注度就会下降，如果能量为0，专注度会完全丧失）
    public int focus;
    //能量
    public int engry;

    //拥有的佣兵集合
    public List<int> mercenaryList;

    //佣兵币
    public int mercenaryMoney;

    //背景音乐的声音大小
    public double musicVolume = 1;
    //选择哪首歌
    public int musicIndex = 0;
}

public class BatteryData
{
    public int index;
    public BatteryType batteryType;
    public int batteryLevel;
}
