using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryDataConfigTable : MonoBehaviour
{
    private void Awake()
    {
        InitData();
    }

    public static List<Mercenary> MercenaryList = new List<Mercenary>();
    void InitData()
    {
        Mercenary mercenary = new Mercenary();
        mercenary.ID = 1;
        mercenary.mercenaryName = "霍根";
        mercenary.iconPath = "UI/Mercenary/Texture/zm01";
        mercenary.star = 1;
        mercenary.price = 100;
        mercenary.talent = "加农炮";
        mercenary.shootDistance = "1";
        mercenary.shootPower = "7";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 2;
        mercenary.mercenaryName = "慕得";
        mercenary.iconPath = "UI/Mercenary/Texture/zm02";
        mercenary.star = 2;
        mercenary.price = 200;
        mercenary.talent = "雷克斯加农炮";
        mercenary.shootDistance = "2";
        mercenary.shootPower = "9";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 3;
        mercenary.mercenaryName = "艾比盖";
        mercenary.iconPath = "UI/Mercenary/Texture/zm03";
        mercenary.star = 3;
        mercenary.price = 300;
        mercenary.talent = "新星加农炮";
        mercenary.shootDistance = "3";
        mercenary.shootPower = "11";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 4;
        mercenary.mercenaryName = "珍尼丝";
        mercenary.iconPath = "UI/Mercenary/Texture/zm04";
        mercenary.star = 1;
        mercenary.price = 100;
        mercenary.talent = "加特林枪";
        mercenary.shootDistance = "4";
        mercenary.shootPower = "10";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 5;
        mercenary.mercenaryName = "葛莱蒂丝";
        mercenary.iconPath = "UI/Mercenary/Texture/zm05";
        mercenary.star = 1;
        mercenary.price = 100;
        mercenary.talent = "导弹";
        mercenary.shootDistance = "1";
        mercenary.shootPower = "4";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 6;
        mercenary.mercenaryName = "丽莎";
        mercenary.iconPath = "UI/Mercenary/Texture/zm06";
        mercenary.star = 2;
        mercenary.price = 200;
        mercenary.talent = "雷克斯导弹";
        mercenary.shootDistance = "2";
        mercenary.shootPower = "7";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 7;
        mercenary.mercenaryName = "费滋";
        mercenary.iconPath = "UI/Mercenary/Texture/zm07";
        mercenary.star = 3;
        mercenary.price = 300;
        mercenary.talent = "新星导弹";
        mercenary.shootDistance = "3";
        mercenary.shootPower = "14";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 8;
        mercenary.mercenaryName = "多莉丝";
        mercenary.iconPath = "UI/Mercenary/Texture/zm08";
        mercenary.star = 1;
        mercenary.price = 100;
        mercenary.talent = "雷克斯激光";
        mercenary.shootDistance = "1";
        mercenary.shootPower = "2";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 9;
        mercenary.mercenaryName = "奥萝拉";
        mercenary.iconPath = "UI/Mercenary/Texture/zm09";
        mercenary.star = 2;
        mercenary.price = 200;
        mercenary.talent = "反击导弹B";
        mercenary.shootDistance = "2";
        mercenary.shootPower = "5";
        MercenaryList.Add(mercenary);

        mercenary = new Mercenary();
        mercenary.ID = 10;
        mercenary.mercenaryName = "艾谱莉";
        mercenary.iconPath = "UI/Mercenary/Texture/zm10";
        mercenary.star = 3;
        mercenary.price = 300;
        mercenary.talent = "狙击手";
        mercenary.shootDistance = "3";
        mercenary.shootPower = "8";
        MercenaryList.Add(mercenary);


        //mercenary = new Mercenary();
        //mercenary.ID = 11;
        //mercenary.mercenaryName = "亚莉克希亚";
        //mercenary.iconPath = "UI/Mercenary/Texture/zm11";
        //mercenary.star = 4;
        //mercenary.price = 400;
        //mercenary.talent = "新星狙击手";
        //mercenary.shootDistance = "4";
        //mercenary.shootPower = "12";
        //MercenaryList.Add(mercenary);
    }
}

public class Mercenary
{
    public int ID;
    public string mercenaryName;
    public string iconPath;
    public int star;
    public float price;
    public string talent;
    public string shootDistance;
    public string shootPower;
}
