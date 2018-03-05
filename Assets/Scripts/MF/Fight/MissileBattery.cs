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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            if (canAttackEnemyList != null)
            {
                if (!canAttackEnemyList.Contains(other.gameObject))
                {
                    canAttackEnemyList.Add(other.gameObject);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            if (canAttackEnemyList != null)
            {
                if (canAttackEnemyList.Contains(other.gameObject))
                {
                    canAttackEnemyList.Remove(other.gameObject);
                    if (currentTarget != null && currentTarget == other.gameObject)
                    {
                        currentTarget = null;
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
                    GameObject bullet = Instantiate(Resources.Load("Bomb")) as GameObject;
                    bullet.transform.position = tt.position;
                    //bullet.transform.localScale = Vector3.one;
                    BulletParent bp = bullet.GetComponent<BulletParent>();
                    bp.target = currentTarget;
                    bp.speed = 5;
                    bp.damage = 1;
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
            for (int i = 0; i < canAttackEnemyList.Count; i++)
            {
                item = canAttackEnemyList[i];
                if (item == null)
                {
                    canAttackEnemyList.Remove(item);
                }
                else if (item.GetComponent<EnemyParent>().blood <= 0)
                {
                    canAttackEnemyList.Remove(item);
                }
                else
                {
                    return item;
                }
            }
        }
        return null;
    }
}
