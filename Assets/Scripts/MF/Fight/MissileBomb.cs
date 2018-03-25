using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//核弹
public class MissileBomb : BulletParent
{
    public const float g = 9.8f;

    private float verticalSpeed;
    private Vector3 moveDirection;

    private float angleSpeed;
    private float angle;
    void Start()
    {
        float tmepDistance = Vector3.Distance(transform.position, target.transform.position);
        float tempTime = tmepDistance / speed;
        float riseTime, downTime;
        riseTime = downTime = tempTime / 2;
        verticalSpeed = g * riseTime;
        transform.LookAt(target.transform.position);

        float tempTan = verticalSpeed / speed;
        double hu = Mathf.Atan(tempTan);
        angle = (float)(180 / Mathf.PI * hu);
        transform.eulerAngles = new Vector3(-angle, transform.eulerAngles.y, transform.eulerAngles.z);
        angleSpeed = angle / riseTime;

        moveDirection = target.transform.position - transform.position;

        Invoke("DestoryOwn", 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("terrain"))
        {
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

    float time;
    void FixedUpdate()
    {
        if (target != null)
        {
            //if (transform.position.y < target.transform.position.y)
            if (Vector3.Distance(transform.position, target.transform.position) < 3)
            {
                //finish  
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
                return;
            }
            if (gameObject)
            {
                time += Time.deltaTime;
                float test = verticalSpeed - g * time;
                transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
                transform.Translate(Vector3.up * test * Time.deltaTime, Space.World);
                float testAngle = -angle + angleSpeed * time;
                transform.eulerAngles = new Vector3(testAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
    }
}
