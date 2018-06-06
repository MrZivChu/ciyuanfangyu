using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//恢复设置的炮塔数据
public class RecoverManagerBatteryData : RecoverBatteryDataBase
{
    //是否从战斗场景过来
    public static bool fromFightScene;

    public void Start()
    {
        base.BaseStart();
    }

    public override GameObject InstanceManagerBatteryObj(BatteryConfigInfo info, Transform parent, int level = 1)
    {
        GameObject go = base.InstanceManagerBatteryObj(info, parent, level);
        if (fromFightScene)
        {
            Animator animator = go.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("houseBX");
            }
        }
        return go;
    }

    public override void SpawnOver()
    {
        base.SpawnOver();
        fromFightScene = false;
    }
}
