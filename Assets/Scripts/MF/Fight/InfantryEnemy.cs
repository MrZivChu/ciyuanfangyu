using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryEnemy : EnemyParent
{
    void Awake()
    {
        base.BaseAwake();
        transform.LookAt(boss.transform);
    }

    public List<GameObject> attackTargetList = new List<GameObject>();
    Animator animator;

    System.Random random;
    private void Start()
    {
        random = new System.Random();
        animator = GetComponent<Animator>();
        HandleDistance();

        InvokeRepeating("getTarget", 0, 0.2f);
        InvokeRepeating("Attack", 0, attackRepeatRateTime);
    }

    public BoxCollider boxCollider;
    void HandleDistance()
    {
        float sizeZ = boxCollider.size.z;
        float span = maxAttackDistance - sizeZ;
        float addValue = span * 0.5f;
        Vector3 tCenter = boxCollider.center;
        tCenter.z += addValue;
        boxCollider.center = tCenter;

        Vector3 tSize = boxCollider.size;
        tSize.z = maxAttackDistance;
        boxCollider.size = tSize;
    }

    void Update()
    {
        if (boss != null)
        {
            if (target == null)
            {
                transform.LookAt(boss.transform);
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
            }
        }
    }

    public void getTarget()
    {
        if (target == null && blood > 0)
        {
            if (attackTargetList.Count > 0)
            {
                GameObject temp = null;
                for (int i = attackTargetList.Count - 1; i >= 0; i--)
                {
                    temp = attackTargetList[i];
                    if (temp != null)
                    {
                        BatteryParent bp = temp.GetComponent<BatteryParent>();
                        if (bp != null && bp.blood > 0)
                        {
                            target = temp;
                            animator.SetBool("canWalk", false);
                            Attack();
                            return;
                        }
                        else
                        {
                            HandleDieTarget(temp);
                        }
                    }
                    else
                    {
                        attackTargetList.Remove(temp);
                    }
                }
            }
            animator.SetBool("canAttack", false);
            animator.SetFloat("walkSpeed", walkSpeed);
            animator.SetBool("canWalk", true);
        }
    }

    void HandleDieTarget(GameObject temp)
    {
        BatteryParent bp = temp.GetComponent<BatteryParent>();
        if (bp != null)
        {
            List<GameObject> bList = bp.singleHoleList;
            if (bList != null && bList.Count > 0)
            {
                for (int j = 0; j < bList.Count; j++)
                {
                    recoverBatteryDataBase.DestoryBattery(bList[j].transform);
                }
                recoverBatteryDataBase.DestoryGroupBattery(temp);
            }
        }
        attackTargetList.Remove(temp);
    }

    public override void Attack()
    {
        if (boss != null && target != null && blood > 0)
        {
            BatteryParent batteryScript = target.GetComponent<BatteryParent>();
            if (target != null && batteryScript != null && batteryScript.blood > 0)
            {
                animator.SetInteger("attackType", random.Next(1, 3));
                animator.SetBool("canAttack", true);
                batteryScript.blood -= attackValue;
                batteryScript.BeAttack();
                if (batteryScript.blood <= 0)
                {
                    HandleDieTarget(target);
                }
            }
            else
            {
                if (attackTargetList.Contains(target))
                {
                    attackTargetList.Remove(target);
                }
                getTarget();
            }
        }
    }

    bool isPlayingDie = false;
    public override void Die()
    {
        if (blood <= 0 && !isPlayingDie)
        {
            isPlayingDie = true;
            animator.SetBool("canAttack", false);
            animator.SetBool("canWalk", false);
            animator.SetTrigger("die");
            Invoke("DestoryOwn", 3.7f);
        }
    }

    void DestoryOwn()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
