using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyParent : MonoBehaviour
{
    public void BaseAwake()
    {
        boss = GameObject.Find("boss");
    }

    //敌人的模型
    public string model;

    //血量
    public float blood;

    //目标
    public GameObject target;

    //行走速度
    public float walkSpeed;

    //攻击伤害
    public float attackValue;

    //攻击距离
    public float maxAttackDistance;

    //几秒攻击一次
    public float attackRepeatRateTime;

    public GameObject boss;

    public RecoverBatteryDataBase recoverBatteryDataBase;

    //攻击行为
    public abstract void Attack();

    //死亡行为
    public abstract void Die();

    //被攻击行为
    public Transform hitPlace;
    public virtual void BeAttack()
    {
        Object bloodObj = Resources.Load("metalBlood");
        if (bloodObj != null)
        {
            GameObject obj = Instantiate(bloodObj) as GameObject;
            obj.transform.position = hitPlace.position;
            obj.transform.localScale = Vector3.one;
        }
    }
}
