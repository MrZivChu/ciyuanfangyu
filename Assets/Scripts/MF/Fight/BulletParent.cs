using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParent : MonoBehaviour {
    //目标
    [HideInInspector]
    public GameObject target;
    //速度
    [HideInInspector]
    public float speed;

    //伤害
    [HideInInspector]
    public float damage;
}
