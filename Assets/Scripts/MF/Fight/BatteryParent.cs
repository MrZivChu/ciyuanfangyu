using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BatteryParent : MonoBehaviour
{
    //名称
    public string batteryName;
    //炮塔的图标
    public string icon;
    //炮塔的模型
    public string model;
    //血量
    public float blood;
    //炮塔的攻击力
    public float attack;
    //炮塔攻击的最大距离
    public float maxAttackDistance;
    //炮塔的描述
    public string desc;
    //炮塔的星级
    public int starLevel;

    //购买此炮塔需要消耗的木材
    public float wood;
    //此炮塔所占能源
    public float MW;

    //几秒攻击一次
    [HideInInspector]
    public float attackRepeatRateTime;


    //射击行为
    public abstract void Shoot();

    [HideInInspector]
    public GameObject boss;

}
