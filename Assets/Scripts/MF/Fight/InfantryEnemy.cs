using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryEnemy : EnemyParent
{
    List<GameObject> attackTargetList = new List<GameObject>();

    private void Start()
    {
        boss = GameObject.Find("boss");
        attackTargetList.Add(boss);
        getTarget();
    }

    //是否正在攻击
    bool attacking = false;
    void Update()
    {
        if (boss != null)
        {
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > maxAttackDistance)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                }
                else
                {
                    if (!attacking)
                    {
                        attacking = true;
                        print("hahah"+attackRepeatRateTime);
                        InvokeRepeating("Attack", 0, attackRepeatRateTime);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            print("碰到了炮塔" + other.name);
            if (!attackTargetList.Contains(other.gameObject))
            {
                attackTargetList.Add(other.gameObject);
                //一开始目标是boss，中途遇到和自己最近的炮塔，那么就必须进行攻击
                attacking = false;
                getTarget();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            print("炮塔" + other.name + "离开了");
            if (attackTargetList.Contains(other.gameObject))
            {
                attackTargetList.Remove(other.gameObject);
            }
            //如果是当前攻击的目标离开了，那么就要重新获取新的目标
            if (target != null && other.gameObject == target)
            {
                attacking = false;
                getTarget();
            }
        }
    }

    void getTarget()
    {
        if (attackTargetList.Count > 0)
        {
            float tDistance = Vector3.Distance(transform.position, attackTargetList[0].transform.position);
            GameObject tCurrentTarget = attackTargetList[0];
            if (attackTargetList.Count > 1)
            {
                for (int i = 1; i < attackTargetList.Count; i++)
                {
                    if (attackTargetList[i] != null)
                    {
                        float tCurrentDistance = Vector3.Distance(transform.position, attackTargetList[i].transform.position);
                        if (tCurrentDistance < tDistance)
                        {
                            tCurrentTarget = attackTargetList[i];
                        }
                    }
                }
            }
            target = tCurrentTarget;
            transform.LookAt(target.transform);
        }
        else
        {
            target = null;
        }
    }

    public override void Attack()
    {
        if (boss != null)
        {
            if (target != null && target.GetComponent<BatteryParent>() != null && target.GetComponent<BatteryParent>().blood > 0)
            {
                GameObject bullet = Instantiate(Resources.Load("Bomb")) as GameObject;
                bullet.transform.position = transform.position;
                bullet.transform.localScale = Vector3.one;
                BulletParent bp = bullet.GetComponent<BulletParent>();
                bp.target = target;
                bp.speed = 20;
                bp.damage = 5;
            }
            else
            {
                if (attackTargetList.Contains(target))
                {
                    attackTargetList.Remove(target);
                }
                attacking = false;
                getTarget();
            }
        }
    }
}
