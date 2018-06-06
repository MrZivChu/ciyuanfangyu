using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinGunBattery : BatteryParent
{
    public List<GameObject> canAttackEnemyList = new List<GameObject>();
    public GameObject currentTarget;

    public List<GameObject> barrelList = new List<GameObject>();

    public GameObject tempParticleSystem;
    public Animator animator;
    private void Start()
    {
        InvokeRepeating("ChooseNewTarget", 0, 0.5f);
        InvokeRepeating("Shoot", 0, attackRepeatRateTime);
        InvokeRepeating("StartShoot", 0, 10);
        InvokeRepeating("StopShoot", 5, 15);

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
                        cef.bp = this;
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

    public override void ResetNewTarget()
    {
        currentTarget = null;
        currentTarget = GetEnemy();
    }

    void StopShoot()
    {
        if (animator != null)
        {
            shooting = false;
            animator.enabled = false;
            tempParticleSystem.SetActive(false);
        }
    }

    bool shooting = false;
    void StartShoot()
    {
        if (animator != null)
        {
            if (currentTarget != null && currentTarget.GetComponent<EnemyParent>().blood > 0)
            {
                animator.enabled = true;
                tempParticleSystem.SetActive(true);
                shooting = true;
            }
        }
    }

    public override void Shoot()
    {
        if (shooting)
        {
            if (currentTarget != null && currentTarget.GetComponent<EnemyParent>().blood > 0)
            {
                if (barrelList != null && barrelList.Count > 0)
                {
                    for (int i = 0; i < barrelList.Count; i++)
                    {
                        Transform tt = barrelList[i].transform;
                        GameObject bullet = Instantiate(Resources.Load("GatlinGunBullet")) as GameObject;
                        bullet.transform.position = tt.position;
                        BulletParent bp = bullet.GetComponent<BulletParent>();
                        bp.target = currentTarget;
                        bp.speed = 100;
                        bp.damage = attackValue;
                    }
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
                        if (Vector3.Distance(transform.position, item.transform.position) < (maxAttackDistance + 5))
                        {
                            StartShoot();
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
