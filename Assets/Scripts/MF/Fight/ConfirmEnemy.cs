using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmEnemy : MonoBehaviour
{
    public BatteryParent bp;
    public List<GameObject> canAttackList = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyConfirm"))
        {
            Transform pos = other.transform.GetChild(0);
            if (EnemyManager.dic.ContainsKey(pos))
            {
                canAttackList.Clear();
                canAttackList.AddRange(EnemyManager.dic[pos]);
                bp.ResetNewTarget();
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
                List<GameObject> temp = EnemyManager.dic[pos];
                if (temp != null)
                {
                    GameObject go = null;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        go = temp[i];
                        if (canAttackList.Contains(go))
                        {
                            canAttackList.Remove(go);
                        }
                    }
                    bp.ResetNewTarget();
                }
            }
        }
    }
}
