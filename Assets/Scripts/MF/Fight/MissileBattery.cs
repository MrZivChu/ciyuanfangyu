using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBattery : BatteryParent
{
    public List<GameObject> canAttackEnemyList = new List<GameObject>();
    public GameObject currentTarget;

    public List<GameObject> barrelList = new List<GameObject>();

    Animator animator;
    private void Start()
    {        
        animator = GetComponent<Animator>();
        InvokeRepeating("ChooseNewTarget", 0, 0.5f);
        InvokeRepeating("Shoot", 0, attackRepeatRateTime);

        ConfirmEnemy();
    }

    void ConfirmEnemy()
    {
        if (transform.childCount > 0)
        {
            Transform range = transform.GetChild(transform.childCount - 1);
            if (range != null && range.childCount > 0)
            {
                Transform confirmEnemyObj = range.GetChild(range.childCount - 1);
                if (confirmEnemyObj != null)
                {
                    ConfirmEnemy cef = confirmEnemyObj.GetComponent<ConfirmEnemy>();
                    if (cef != null)
                    {
                        canAttackEnemyList = cef.canAttackList;
                    }
                }
            }
        }
    }

    //为炮塔选择新的目标
    void ChooseNewTarget()
    {
        if (currentTarget == null)
        {
            currentTarget = GetEnemy();
        }
    }

    public override void Shoot()
    {
        if (currentTarget != null && currentTarget.GetComponent<EnemyParent>().blood > 0)
        {
            if (barrelList != null && barrelList.Count > 0)
            {
                for (int i = 0; i < barrelList.Count; i++)
                {
                    Transform tt = barrelList[i].transform;
                    GameObject bullet = Instantiate(Resources.Load("MissileBomb")) as GameObject;
                    bullet.transform.position = tt.position;
                    //bullet.transform.localScale = Vector3.one;
                    BulletParent bp = bullet.GetComponent<BulletParent>();
                    bp.target = currentTarget;
                    bp.speed = 15;
                    bp.damage = attackValue;
                    animator.SetTrigger("shootTrigger");
                }
            }
        }
    }
  
    //获取一个敌人
    GameObject GetEnemy()
    {
        if (canAttackEnemyList != null && canAttackEnemyList.Count > 0)
        {
            GameObject item = null;
            for (int i = canAttackEnemyList.Count - 1; i >= 0; i--)
            {
                item = canAttackEnemyList[i];
                if (item != null)
                {
                    EnemyParent ep = item.GetComponent<EnemyParent>();
                    if (ep != null && ep.blood > 0)
                    {
                        if (Vector3.Distance(transform.position, item.transform.position) < (maxAttackDistance - 5))
                        {
                            return item;
                        }
                    }
                    else
                    {
                        canAttackEnemyList.Remove(item);
                    }
                }
                else
                {
                    canAttackEnemyList.Remove(item);
                }
            }
        }
        return null;
    }
}
