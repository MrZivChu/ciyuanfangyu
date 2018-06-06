using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinGunBullet : BulletParent
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

    void DestoryOwn()
    {
        if (gameObject)
        {
            Destroy(gameObject);
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

    void FixedUpdate()
    {
        if (target != null)
        {
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
