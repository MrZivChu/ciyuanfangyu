using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//核弹
public class CannonBullet : BulletParent
{
    void Start()
    {
        Vector3 v = target.transform.position;
        v.y += 5;
        transform.LookAt(v);

        Invoke("DestoryOwn", 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("terrain"))
        {
            DestoryOwn();
        }
        else if (other.CompareTag("enemy"))
        {
            HandleEnemy();
        }
    }

    void HandleEnemy()
    {
        EnemyParent ep = target.GetComponent<EnemyParent>();
        if (ep != null)
        {
            ep.blood -= damage;
            ep.BeAttack();
            if (ep.blood <= 0)
            {
                ep.Die();
            }
            DestoryOwn();
        }
    }

    void DestoryOwn()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            print(Vector3.Distance(transform.position, target.transform.position));
            if (Vector3.Distance(transform.position, target.transform.position) < 5)
            {
                HandleEnemy();
                return;
            }
            if (gameObject)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }
}
