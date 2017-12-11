using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//核弹
public class Bomb : BulletParent
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
    }

    float time;
    void FixedUpdate()
    {
        if (transform.position.y < target.transform.position.y)
        {
            //finish  
            BatteryParent bp = target.GetComponent<BatteryParent>();
            if (bp != null)
            {
                bp.blood -= damage;
                if (bp.blood <= 0)
                {
                    if (target.name != "boss")
                    {
                        Destroy(target);
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                EnemyParent ep = target.GetComponent<EnemyParent>();
                if (ep != null)
                {
                    ep.blood -= damage;
                    if (ep.blood <= 0)
                    {
                        if (target.name != "boss")
                        {
                            Destroy(target);
                        }
                    }
                    Destroy(gameObject);
                }
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
