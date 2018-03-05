using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyParent : MonoBehaviour
{  
    //血量
    public float blood;

    //目标
    public GameObject target;

    //行走速度
    public float walkSpeed;

    //攻击距离
    public float maxAttackDistance;

    //几秒攻击一次
    public float attackRepeatRateTime;

    //攻击行为
    public abstract void Attack();
    
    public GameObject boss;

}
