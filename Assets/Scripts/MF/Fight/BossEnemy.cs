using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyParent
{
    float spanDistance = 6;

    void Awake()
    {
        base.BaseAwake();
        transform.LookAt(boss.transform);
    }

    Animator animator;

    System.Random random;
    AnimatorStateInfo animatorStateInfo;
    private void Start()
    {
        random = new System.Random();
        animator = GetComponent<Animator>();

        InvokeRepeating("HandleTarget", 0, 0.3f);
        InvokeRepeating("Attack", 0, attackRepeatRateTime);
    }

    void Update()
    {
        if (blood > 0 && boss != null)
        {
            if (target == null)
            {
                animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (animatorStateInfo.IsName("Base Layer.walk") || animatorStateInfo.IsName("Base Layer.run"))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                }
            }
            else
            {
                transform.LookAt(target.transform);
            }
        }
    }

    public void HandleTarget()
    {
        if (target == null)
        {
            getTarget();
            if (target == null && boss != null)
            {
                transform.LookAt(boss.transform);
                animator.SetFloat("walkSpeed", walkSpeed);
                animator.SetBool("canAttack", false);
                animator.SetBool("canWalk", true);
            }
            else
            {
                if (target != null)
                {
                    transform.LookAt(target.transform);
                    animator.SetBool("canWalk", false);
                }
            }
        }
    }

    public void getTarget()
    {
        if (target == null && blood > 0)
        {
            if (attackTargetList.Count > 0)
            {
                GameObject nearBattery = attackTargetList[attackTargetList.Count - 1];
                if (nearBattery != null)
                {
                    float tempDistance = Vector3.Distance(transform.position, nearBattery.transform.position);
                    for (int i = attackTargetList.Count - 2; i >= 0; i--)
                    {
                        GameObject tempBattery = attackTargetList[i];
                        if (tempBattery != null)
                        {
                            float distance = Vector3.Distance(transform.position, tempBattery.transform.position);

                            if (distance < tempDistance)
                            {
                                tempDistance = distance;
                                nearBattery = tempBattery;
                            }
                        }
                    }

                    float juli = Vector3.Distance(transform.position, nearBattery.transform.position);
                    if (juli < maxAttackDistance + spanDistance)
                    {
                        BatteryParent bp = nearBattery.GetComponent<BatteryParent>();
                        if (bp != null && bp.blood > 0)
                        {
                            target = nearBattery;
                            return;
                        }
                        else
                        {
                            attackTargetList.Remove(nearBattery);
                        }
                    }
                }
            }
        }
    }

    void HandleDieTarget(GameObject temp)
    {
        BatteryParent bp = temp.GetComponent<BatteryParent>();
        if (bp != null)
        {
            List<GameObject> bList = bp.holeListForGroupBattery;
            if (bList != null && bList.Count > 0)
            {
                for (int j = 0; j < bList.Count; j++)
                {
                    recoverBatteryDataBase.DestoryBattery(bList[j].transform);
                }
            }
            recoverBatteryDataBase.DestoryGroupBattery(temp);
        }
        attackTargetList.Remove(temp);
        SpawnPoHuai(temp.transform.parent);
        target = null;
    }

    void SpawnPoHuai(Transform hole)
    {
        UnityEngine.Object obj = Resources.Load("pohuai");
        GameObject tempGO = Instantiate(obj) as GameObject;
        tempGO.transform.parent = hole;
        tempGO.transform.localPosition = Vector3.zero;

        Vector3 vv = Vector3.zero;
        vv.y = tempGO.transform.position.y;
        tempGO.transform.LookAt(vv);
        tempGO.transform.Rotate(Vector3.up, 180);

    }

    public override void Attack()
    {
        if (boss != null && blood > 0)
        {
            if (target != null)
            {
                float juli = Vector3.Distance(transform.position, target.transform.position);
                if (juli < maxAttackDistance + spanDistance)
                {
                    BatteryParent batteryScript = target.GetComponent<BatteryParent>();
                    if (batteryScript != null && batteryScript.blood > 0)
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
                        HandleDieTarget(target);
                    }
                }
                else
                {
                    target = null;
                }
            }
        }
    }

    public void PlayCameraShake()
    {
        //CameraShake.shakeFor(0.3f, 0.2f);
    }

    bool nowIsPlayingDie = false;
    public override void Die()
    {
        if (blood <= 0 && !nowIsPlayingDie)
        {
            nowIsPlayingDie = true;
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

    public override void BeAttack()
    {
        UnityEngine.Object bloodObj = Resources.Load("metalBlood");
        if (bloodObj != null)
        {
            GameObject obj = Instantiate(bloodObj) as GameObject;
            obj.transform.position = hitPlace.position;
            obj.transform.localScale = Vector3.one;
        }
    }
}
