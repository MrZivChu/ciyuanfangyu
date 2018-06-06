using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTriggerTarget : MonoBehaviour
{
    public EnemyParent enemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            //print("碰到了炮塔" + other.name);
            if (!enemy.attackTargetList.Contains(other.gameObject))
            {
                enemy.attackTargetList.Add(other.gameObject);
            }
            ////没有目标的话，就选择一个目标开始进攻
            //if (infantryEnemy.target == null)
            //{
            //    infantryEnemy.getTarget();
            //}
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            //print("炮塔" + other.name + "离开了");
            if (enemy.attackTargetList.Contains(other.gameObject))
            {
                enemy.attackTargetList.Remove(other.gameObject);
            }

            ////如果是当前攻击的目标离开了，那么就要重新获取新的目标
            //if (infantryEnemy.target != null && other.gameObject == infantryEnemy.target)
            //{
            //    infantryEnemy.target = null;
            //    infantryEnemy.getTarget();
            //}
        }
    }
}
