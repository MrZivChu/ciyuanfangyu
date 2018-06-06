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
    public float attackValue;
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
    public float attackRepeatRateTime;

    public BatteryType battleType;

    public GameObject boss;

    //记录哪些坑生成的组合炮塔
    public List<GameObject> holeListForGroupBattery;

    //射击行为
    public abstract void Shoot();

    public abstract void ResetNewTarget();

    public virtual void ShowBuildOverCanvas()
    { }

    //能够攻击的敌人列表
    public List<GameObject> canAttackEnemyList = new List<GameObject>();
    //敌人生成的点
    public Transform enemySpawnPoint;

    //被攻击行为
    public Transform hitPlace;
    public virtual void BeAttack()
    {
        Object bloodObj = Resources.Load("metalBlood");
        if (bloodObj != null && hitPlace != null)
        {
            GameObject obj = Instantiate(bloodObj) as GameObject;
            obj.transform.position = hitPlace.position;
            obj.transform.localScale = Vector3.one;
        }
    }
}
