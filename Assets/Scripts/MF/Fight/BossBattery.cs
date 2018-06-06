using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattery : BatteryParent
{
    public override void Shoot()
    {

    }

    public GameObject result;
    public override void BeAttack()
    {
        if (blood <= 0)
        {
            result.SetActive(true);
        }else
        {
            base.BeAttack();
        }
    }

    public override void ResetNewTarget()
    {

    }
}
