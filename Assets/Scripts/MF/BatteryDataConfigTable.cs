using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatteryType
{
    None = 0,

    CannonBattery = 1,//加农炮
    RexCannonBattery = 2,//雷克斯加农炮
    NovaCannonBattery = 3,//新星加农炮

    GatlinGunBattery = 4,//加特林枪

    MissileBattery = 5, //导弹
    RexMissileBattery = 6,//雷克斯导弹
    NovaMissileBattery = 7,//新星导弹

    RexLaserBattery = 8,//雷克斯激光
    CounterattackMissileBBattery = 9,//反击导弹B
    SniperBattery = 10,//狙击手
    NovaSniperBattery = 11,//新星狙击手

    ResidentSBattery = 12,//驻地(S)
    ResidentMBattery = 13,//驻地(M)
    LeisureParkBattery = 14,//休闲公园
    MultiPlantBattery = 15,//多产
    ShoppingMallBattery = 16//购物中心
}

public class BatteryDataConfigTable : MonoBehaviour
{
    void Awake()
    {
        InitData();
    }

    public static Dictionary<BatteryType, BatteryConfigInfo> dic = new Dictionary<BatteryType, BatteryConfigInfo>();

    //所有
    public static List<BatteryConfigInfo> allList = new List<BatteryConfigInfo>();
    //加农炮
    public static List<BatteryConfigInfo> cannonList = new List<BatteryConfigInfo>();
    //加特林枪
    public static List<BatteryConfigInfo> gatlinGunList = new List<BatteryConfigInfo>();
    //导弹
    public static List<BatteryConfigInfo> missileList = new List<BatteryConfigInfo>();
    //特殊
    public static List<BatteryConfigInfo> specialList = new List<BatteryConfigInfo>();
    //公共
    public static List<BatteryConfigInfo> publicList = new List<BatteryConfigInfo>();

    void InitData()
    {
        BatteryConfigInfo batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "加农炮";
        batteryInfo.icon = "UI/Battery/CannonBattery";
        batteryInfo.model = "CannonBatteryLv1";
        batteryInfo.battleType = BatteryType.CannonBattery;
        batteryInfo.blood = 500;
        batteryInfo.attack = 240;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 45;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "一般的双重设计武器，在所有战斗中都很有效。如果不确定的话，安装这个就对了";
        batteryInfo.wood = 10;
        batteryInfo.MW = 80;
        cannonList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "雷克斯加农炮";
        batteryInfo.icon = "UI/Battery/RexCannonBattery";
        batteryInfo.model = "RexCannonBatteryLv1";
        batteryInfo.battleType = BatteryType.RexCannonBattery;
        batteryInfo.blood = 1000;
        batteryInfo.attack = 480;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 45;
        batteryInfo.starLevel = 2;
        batteryInfo.desc = "更强力更耐用，和普通的加农炮一样容易上手";
        batteryInfo.wood = 10;
        batteryInfo.MW = 150;
        cannonList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "新星加农炮";
        batteryInfo.icon = "UI/Battery/NovaCannonBattery";
        batteryInfo.model = "NovaCannonBatteryLv1";
        batteryInfo.battleType = BatteryType.NovaCannonBattery;
        batteryInfo.blood = 2000;
        batteryInfo.attack = 960;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 45;
        batteryInfo.starLevel = 3;
        batteryInfo.desc = "一个测试版模型。虽然没有设想中的那么厉害，但火力仍然不容小觑";
        batteryInfo.wood = 10;
        batteryInfo.MW = 300;
        cannonList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "加特林枪";
        batteryInfo.icon = "UI/Battery/GatlinGunBattery";
        batteryInfo.model = "GatlinGunBatteryLv1";
        batteryInfo.battleType = BatteryType.GatlinGunBattery;
        batteryInfo.blood = 400;
        batteryInfo.attack = 257;
        batteryInfo.attackRepeatRateTime = 0.05f;
        batteryInfo.maxAttackDistance = 45;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "快速连射武器，火力强大且能压制敌人，但是耐用性不高";
        batteryInfo.wood = 10;
        batteryInfo.MW = 150;
        gatlinGunList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "导弹";
        batteryInfo.icon = "UI/Battery/MissileBattery";
        batteryInfo.model = "MissileBatteryLv1";
        batteryInfo.battleType = BatteryType.MissileBattery;
        batteryInfo.blood = 600;
        batteryInfo.attack = 171;
        batteryInfo.attackRepeatRateTime = 5;
        batteryInfo.maxAttackDistance = 80;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "该武器射程很远，但不擅长应付快速移动的敌人";
        batteryInfo.wood = 10;
        batteryInfo.MW = 100;
        missileList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "雷克斯导弹";
        batteryInfo.icon = "UI/Battery/RexMissileBattery";
        batteryInfo.model = "RexMissileBatteryLv1";
        batteryInfo.battleType = BatteryType.RexMissileBattery;
        batteryInfo.blood = 1200;
        batteryInfo.attack = 423;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 80;
        batteryInfo.starLevel = 2;
        batteryInfo.desc = "更耐用、更强力的发射器，可以一次性的攻击一大群人";
        batteryInfo.wood = 10;
        batteryInfo.MW = 200;
        missileList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "新星导弹";
        batteryInfo.icon = "UI/Battery/NovaMissileBattery";
        batteryInfo.model = "NovaMissileBatteryLv1";
        batteryInfo.battleType = BatteryType.NovaMissileBattery;
        batteryInfo.blood = 2400;
        batteryInfo.attack = 1004;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 80;
        batteryInfo.desc = "一个精简了尺寸的发射器，很容易带入城市而不被发现，而且威力更大";
        batteryInfo.wood = 10;
        batteryInfo.MW = 400;
        batteryInfo.starLevel = 3;
        missileList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "雷克斯激光";
        batteryInfo.icon = "UI/Battery/RexLaserBattery";
        batteryInfo.model = "RexLaserBatteryLv1";
        batteryInfo.battleType = BatteryType.RexLaserBattery;
        batteryInfo.blood = 1400;
        batteryInfo.attack = 337;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 65;
        batteryInfo.starLevel = 2;
        batteryInfo.desc = "穿透性极强的激光枪，研发部门仍然在继续开发3光束版本，但目前的火力已经足够强大了";
        batteryInfo.wood = 10;
        batteryInfo.MW = 700;
        specialList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "反击导弹B";
        batteryInfo.icon = "UI/Battery/CounterattackMissileBBattery";
        batteryInfo.model = "CounterattackMissileBBatteryLv1";
        batteryInfo.battleType = BatteryType.CounterattackMissileBBattery;
        batteryInfo.blood = 1000;
        batteryInfo.attack = 160;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 450;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "城市防御的最后一道屏障，用来攻击进入城市的敌人";
        batteryInfo.wood = 10;
        batteryInfo.MW = 100;
        specialList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "狙击手";
        batteryInfo.icon = "UI/Battery/SniperBattery";
        batteryInfo.model = "SniperBatteryLv1";
        batteryInfo.battleType = BatteryType.SniperBattery;
        batteryInfo.blood = 700;
        batteryInfo.attack = 333;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 65;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "射程很远的单发设计武器，偏爱暴击者的首选";
        batteryInfo.wood = 10;
        batteryInfo.MW = 200;
        specialList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "新星狙击手";
        batteryInfo.icon = "UI/Battery/NovaSniperBattery";
        batteryInfo.model = "NovaSniperBatteryLv1";
        batteryInfo.battleType = BatteryType.NovaSniperBattery;
        batteryInfo.blood = 2800;
        batteryInfo.attack = 1333;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 65;
        batteryInfo.starLevel = 3;
        batteryInfo.desc = "同样的暴击能力，但是火力和射程都比以前更强，最适合用来对付单一敌人";
        batteryInfo.wood = 10;
        batteryInfo.MW = 800;
        specialList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "驻地(S)";
        batteryInfo.icon = "UI/Battery/ResidentSBattery";
        batteryInfo.model = "ResidentSBatteryLv1";
        batteryInfo.battleType = BatteryType.ResidentSBattery;
        batteryInfo.blood = 800;
        batteryInfo.attack = 1200;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 450;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "用来安置人口的住所，要想引入新居民的话，就得建些这样的地方";
        batteryInfo.wood = 10;
        batteryInfo.MW = 200;
        publicList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "驻地(M)";
        batteryInfo.icon = "UI/Battery/ResidentMBattery";
        batteryInfo.model = "ResidentMBatteryLv1";
        batteryInfo.battleType = BatteryType.ResidentMBattery;
        batteryInfo.blood = 1600;
        batteryInfo.attack = 2400;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 450;
        batteryInfo.starLevel = 2;
        batteryInfo.desc = "可以让人舒适居住的单位，可以增加城市的住宅容量";
        batteryInfo.wood = 10;
        batteryInfo.MW = 400;
        publicList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "休闲公园";
        batteryInfo.icon = "UI/Battery/LeisureParkBattery";
        batteryInfo.model = "LeisureParkBatteryLv1";
        batteryInfo.battleType = BatteryType.LeisureParkBattery;
        batteryInfo.blood = 600;
        batteryInfo.attack = 20;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 450;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "一个能让城市人群放松下来的地方，里面有不少年轻人和来年人都喜爱的娱乐活动";
        batteryInfo.wood = 10;
        batteryInfo.MW = 100;
        publicList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "多厂";
        batteryInfo.icon = "UI/Battery/MultiPlantBattery";
        batteryInfo.model = "MultiPlantBatteryLv1";
        batteryInfo.battleType = BatteryType.MultiPlantBattery;
        batteryInfo.blood = 1000;
        batteryInfo.attack = 30;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 450;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "这个单位负责满足你城市人民的基本需求，对一些大型娱乐活动也很有帮助";
        batteryInfo.wood = 10;
        batteryInfo.MW = 200;
        publicList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        batteryInfo = new BatteryConfigInfo();
        batteryInfo.batteryName = "购物中心";
        batteryInfo.icon = "UI/Battery/ShoppingMallBattery";
        batteryInfo.model = "ShoppingMallBatteryLv1";
        batteryInfo.battleType = BatteryType.ShoppingMallBattery;
        batteryInfo.blood = 1500;
        batteryInfo.attack = 40;
        batteryInfo.attackRepeatRateTime = 2;
        batteryInfo.maxAttackDistance = 450;
        batteryInfo.starLevel = 1;
        batteryInfo.desc = "一个繁忙的地方，很适合举办大型活动，比如独一无二的安奎尔女士的演唱会";
        batteryInfo.wood = 10;
        batteryInfo.MW = 300;
        publicList.Add(batteryInfo);
        dic[batteryInfo.battleType] = batteryInfo;

        allList.AddRange(cannonList);
        allList.AddRange(gatlinGunList);
        allList.AddRange(missileList);
        allList.AddRange(specialList);
        allList.AddRange(publicList);
    }
}


public class BatteryConfigInfo
{
    public BatteryType battleType;

    public string batteryName;
    public string icon;
    public string model;
    public float blood;
    public float attack;
    public float attackRepeatRateTime;
    public float maxAttackDistance;
    public string desc;

    public int starLevel;

    //木材
    public float wood;
    //能源
    public float MW;
}