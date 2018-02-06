using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBattery : BatteryParent
{
    public List<GameObject> canAttackEnemyList = new List<GameObject>();
    public GameObject currentTarget;

    private void Start()
    {
        blood = 100;
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
                }
            }
        }
    }

    void Update()
    {
        if (boss != null)
        {
            if (blood > 0)
            {
                if (currentTarget != null)
                {
                    if (currentTarget.GetComponent<EnemyParent>().blood > 0)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            RaycastHit hit;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.transform.CompareTag("Battery"))
                                {
                                    Shoot();
                                }
                            }
                        }
                    }
                    else
                    {
                        RemoveEnemy(currentTarget);
                        currentTarget = null;
                        GetEnemy();
                    }
                }
                else
                {
                    GetEnemy();
                }
            }
        }
    }

    public override void Shoot()
    {
        if (boss != null)
        {
            if (currentTarget != null && currentTarget.GetComponent<EnemyParent>().blood > 0)
            {
                GameObject bullet = Instantiate(Resources.Load("Bomb")) as GameObject;
                bullet.transform.position = transform.position;
                //bullet.transform.localScale = Vector3.one;
                BulletParent bp = bullet.GetComponent<BulletParent>();
                bp.target = currentTarget;
                bp.speed = 5;
                bp.damage = 10;
            }
        }
    }

    //获取一个敌人
    void GetEnemy()
    {
        if (currentTarget == null)
        {
            if (canAttackEnemyList != null && canAttackEnemyList.Count > 0)
            {
                GameObject item = null;
                for (int i = 0; i < canAttackEnemyList.Count; i++)
                {
                    item = canAttackEnemyList[i];
                    if (item.GetComponent<EnemyParent>().blood <= 0)
                    {
                        canAttackEnemyList.Remove(item);
                    }
                    else
                    {
                        currentTarget = item;
                    }
                }
            }
        }
    }

    //移除一个敌人
    void RemoveEnemy(GameObject item)
    {
        if (canAttackEnemyList != null && canAttackEnemyList.Count > 0)
        {
            if (canAttackEnemyList.Contains(item))
            {
                canAttackEnemyList.Remove(item);
            }
        }
    }
}
