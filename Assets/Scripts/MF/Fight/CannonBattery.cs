using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBattery : BatteryParent
{
    public GameObject currentTarget;
    public List<GameObject> barrelList = new List<GameObject>();

    public GameObject tempParticleSystem;
    public Animator animator;
    public GameObject blooadCanvas;
    public GameObject buildOverCanvas;

    private void Start()
    {
        if (blooadCanvas != null)
            blooadCanvas.SetActive(false);
        sumBlood = blood;
        InvokeRepeating("ChooseNewTarget", 0, 0.5f);
        InvokeRepeating("Shoot", 0, attackRepeatRateTime);
    }

    void Update()
    {
        CalcBlood();
    }

    float sumBlood;
    void CalcBlood()
    {
        if (blooadCanvas != null)
        {
            blooadCanvas.transform.LookAt(Camera.main.transform);
            Slider bloodSlider = blooadCanvas.transform.GetChild(0).GetComponent<Slider>();
            if (bloodSlider != null)
            {
                float rate = blood / sumBlood;
                bloodSlider.value = rate;
            }
        }
    }

    public override void ShowBuildOverCanvas()
    {
        if (buildOverCanvas != null)
        {
            buildOverCanvas.SetActive(true);
            buildOverCanvas.transform.LookAt(Camera.main.transform);
            Invoke("SetBuildOverCanvasHidden", 2);
        }
    }

    void SetBuildOverCanvasHidden()
    {
        if (buildOverCanvas != null)
        {
            buildOverCanvas.SetActive(false);
        }
    }

    public override void BeAttack()
    {
        base.BeAttack();
        if (blooadCanvas != null)
            blooadCanvas.SetActive(true);
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

    public override void Shoot()
    {
        if (animator != null && currentTarget != null && currentTarget.GetComponent<EnemyParent>().blood > 0)
        {
            if (barrelList != null && barrelList.Count > 0)
            {
                for (int i = 0; i < barrelList.Count; i++)
                {
                    MusicManager.Play(MusicType.cannonBatteryLv1);
                    animator.SetTrigger("shootTrigger");
                    tempParticleSystem.SetActive(false);
                    tempParticleSystem.SetActive(true);

                    Transform tt = barrelList[i].transform;
                    GameObject bullet = Instantiate(Resources.Load("CannonBullet")) as GameObject;
                    bullet.transform.position = tt.position;
                    //bullet.transform.localScale = Vector3.one;
                    BulletParent bp = bullet.GetComponent<BulletParent>();
                    bp.target = currentTarget;
                    bp.speed = 30;
                    bp.damage = attackValue / barrelList.Count;
                }
            }
        }
    }

    //获取一个敌人
    GameObject GetEnemy()
    {
        if (enemySpawnPoint == null)
            return null;
        canAttackEnemyList = EnemyManager.dic[enemySpawnPoint];
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
