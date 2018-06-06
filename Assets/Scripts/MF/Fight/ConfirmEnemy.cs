using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmEnemy : MonoBehaviour
{
    public BatteryParent bp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyConfirm"))
        {
            Transform pos = other.transform.GetChild(0);
            if (EnemyManager.dic.ContainsKey(pos))
            {
                if (bp != null)
                {
                    bp.enemySpawnPoint = pos;
                    bp.ResetNewTarget();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemyConfirm"))
        {
            Transform pos = other.transform.GetChild(0);
            if (EnemyManager.dic.ContainsKey(pos))
            {
                if (bp != null)
                {
                    if (bp.enemySpawnPoint != null && bp.enemySpawnPoint == pos)
                    {
                        bp.enemySpawnPoint = null;
                    }
                    bp.ResetNewTarget();
                }
            }
        }
    }
}
