using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyParent : MonoBehaviour
{  
    //血量
    [HideInInspector]
    public float blood;

    //目标
    [HideInInspector]
    public GameObject target;

    //行走速度
    [HideInInspector]
    public float walkSpeed;

    //攻击距离
    [HideInInspector]
    public float maxAttackDistance;

    //几秒攻击一次
    [HideInInspector]
    public float attackRepeatRateTime;

    //攻击行为
    public abstract void Attack();

    [HideInInspector]
    public GameObject boss;

}
