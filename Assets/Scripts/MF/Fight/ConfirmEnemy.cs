using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmEnemy : MonoBehaviour
{

    public List<GameObject> canAttackList = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("enemyConfirm"))
        {
            Transform pos = other.transform.GetChild(0);
            if (EnemyManager.dic.ContainsKey(pos))
            {
                canAttackList = EnemyManager.dic[pos];
            }
        }
    }
}
