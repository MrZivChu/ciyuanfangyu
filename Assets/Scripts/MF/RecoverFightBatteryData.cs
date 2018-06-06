using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//恢复设置的炮塔数据
public class RecoverFightBatteryData : RecoverBatteryDataBase
{
    void Start()
    {
        base.BaseStart();
    }

    public override GameObject InstanceManagerBatteryObj(BatteryConfigInfo info, Transform parent, int level = 1)
    {
        GameObject go = base.InstanceManagerBatteryObj(info, parent, level);
        Animator animator = go.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("batteryBX");
        }
        return go;
    }

}
