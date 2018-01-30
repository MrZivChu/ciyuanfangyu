using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRotateWheel : MonoBehaviour
{
    public float upDownSpeed;
    public Transform centerObj;

    bool isRotating = false;
    Transform preDisk;
    Transform nowDisk;
    Transform hitBuild;

    Vector3 preEulerAngles = Vector2.zero;
    Vector3 nowEulerAngles = Vector2.zero;


    float diskEveryTimeNeedRotateAngle = 45;
    float rotateDiskDirection = 1;//负值为顺时针，正值为逆时针
    int whichDisk = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector2 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            int layMask = 1 << LayerMask.NameToLayer("CheckWheel");
            if (Physics.Raycast(ray, out hit, 1000, layMask))
            {
                nowDisk = null;
                //if (hit.transform.CompareTag("build"))
                {
                    hitBuild = hit.transform.parent;
                    nowDisk = hit.transform.parent.parent.parent;
                }
                if (nowDisk != null)
                {
                    if (preDisk != null)
                    {
                        Utils.SetObjectHighLight(preDisk.gameObject, false, Color.clear, Color.clear);
                    }
                    Utils.SetObjectHighLight(nowDisk.gameObject, true, new Color(1, 164f / 255, 0, 1), new Color(212f / 255, 153f / 255, 47f / 255, 1));
                    preDisk = nowDisk;
                    preEulerAngles = nowDisk.eulerAngles;
                    nowEulerAngles = preEulerAngles;
                    if (nowDisk.CompareTag("oneInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 45f;
                        whichDisk = 1;
                    }
                    else if (nowDisk.CompareTag("twoInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 22.5f;
                        whichDisk = 2;
                    }
                    else if (nowDisk.CompareTag("threeInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 15f;
                        whichDisk = 3;
                    }
                    else if (nowDisk.CompareTag("fourInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 11.25f;
                        whichDisk = 4;
                    }
                    isRotating = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CalcLeftAngle();
            isRotating = false;
            hasTouchMove = false;
        }
        RotateEndHandle();

        if (isRotating)
        {
#if !UNITY_EDITOR  //手机操作
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    hasTouchMove = true;
                    FollowRotate(nowDisk, hitBuild, centerObj);
                }
            }
#else
            hasTouchMove = true;
            FollowRotate(nowDisk, hitBuild, centerObj);
#endif
            Up(nowDisk, Time.deltaTime);
        }
        else
        {
            //Down(nowDisk, Time.deltaTime);
        }
    }

    float tempTime = 0;
    float preAngle = 0;
    float nowAngle = 0;
    float homingTime = 0.3f;
    bool hasTouchMove = false;
    float leftNeedRotateAngle = 0;
    //旋转结束后的处理
    void RotateEndHandle()
    {
        if (leftNeedRotateAngle > 0 && nowDisk != null)
        {
            tempTime = tempTime + Time.deltaTime;
            if (tempTime <= homingTime)
            {
                nowAngle = Mathf.Lerp(0, leftNeedRotateAngle, tempTime * (1 / homingTime));
                nowDisk.Rotate(Vector3.forward, -rotateDiskDirection * (nowAngle - preAngle));
                preAngle = nowAngle;
            }
            else
            {
                nowDisk.Rotate(Vector3.forward, -rotateDiskDirection * (leftNeedRotateAngle - preAngle));
                tempTime = 0;
                preAngle = 0;
                nowAngle = 0;
                leftNeedRotateAngle = 0;
                Down(nowDisk, Time.deltaTime);
            }
        }
    }

    private void CalcLeftAngle()
    {
        leftNeedRotateAngle = 0;
        if (nowDisk != null && hasTouchMove)
        {
            float hasRotateAngle = 0;
            nowEulerAngles = nowDisk.eulerAngles;
            //print(preEulerAngles.y + " = " + nowEulerAngles.y + " = " + rotateDiskDirection);
            if (nowEulerAngles.y > preEulerAngles.y)
            {
                if (rotateDiskDirection == -1)
                {
                    hasRotateAngle = nowEulerAngles.y - preEulerAngles.y;
                }
                else
                {
                    hasRotateAngle = 360 - nowEulerAngles.y + preEulerAngles.y;
                }
            }
            else
            {
                if (rotateDiskDirection == -1)
                {
                    hasRotateAngle = nowEulerAngles.y + (360 - preEulerAngles.y);
                }
                else
                {
                    hasRotateAngle = preEulerAngles.y - nowEulerAngles.y;
                }
            }
            leftNeedRotateAngle = diskEveryTimeNeedRotateAngle - (hasRotateAngle - Mathf.Floor(hasRotateAngle / diskEveryTimeNeedRotateAngle) * diskEveryTimeNeedRotateAngle);
        }
    }

    //鼠标控制轮盘旋转
    void FollowRotate(Transform tNowDisk, Transform tHitBuild, Transform tCenterObj)
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 hitBuildScreenPosition = Camera.main.WorldToScreenPoint(tHitBuild.position);
        Vector2 centerObjScreenPosition = Camera.main.WorldToScreenPoint(tCenterObj.position);

        //print(tHitBuild.name + "鼠标位置=" + mouseScreenPosition + " ; 物体位置=" + hitBuildScreenPosition + " ; 中心点位置=" + centerObjScreenPosition);

        Vector2 a = hitBuildScreenPosition - centerObjScreenPosition;
        Vector2 b = mouseScreenPosition - centerObjScreenPosition;
        float angle = Vector2.Angle(a, b);

        //print(tNowDisk.eulerAngles);
        //print(angle);
        if (angle > 1)
        {
            Quaternion quaternion = Quaternion.FromToRotation(a, b);
            float tempDirectionZ = quaternion.z;
            rotateDiskDirection = tempDirectionZ / Mathf.Abs(tempDirectionZ);
            tNowDisk.Rotate(Vector3.forward, rotateDiskDirection * -angle * Time.deltaTime * 10);
        }

        //当摄像机从小于45度的角去俯视轮盘的时候，此旋转方式会出现异常，如果摄像机从轮盘顶部或者大于45度的角去俯视轮盘则不会出现任何异常
        //Quaternion quaternion = Quaternion.FromToRotation(a, b);
        //tNowDisk.Rotate(-quaternion.eulerAngles);
    }

    //开始旋转的时候物体上移
    void Up(Transform tNowDisk, float deltaTime)
    {
        if (tNowDisk != null)
        {
            float maxValue = whichDisk == 1 ? 1.4f : whichDisk == 2 ? 1.9f : whichDisk == 3 ? 1.35f : 0.55f;
            if (tNowDisk.localPosition.y < maxValue)
            {
                Vector3 v = tNowDisk.localPosition;
                v.y += deltaTime * upDownSpeed;
                if (v.y >= maxValue)
                {
                    v.y = maxValue;
                }
                tNowDisk.localPosition = v;
            }
        }
    }

    //结束旋转的时候物体下移
    void Down(Transform tNowDisk, float deltaTime)
    {
        if (tNowDisk != null)
        {
            float minValue = whichDisk == 1 ? 0.85f : whichDisk == 2 ? 1.35f : whichDisk == 3 ? 0.8f : 0;
            if (tNowDisk.localPosition.y > minValue)
            {
                Vector3 v = tNowDisk.localPosition;
                v.y -= deltaTime * upDownSpeed;
                if (v.y <= minValue)
                {
                    v.y = minValue;
                }
                tNowDisk.localPosition = v;
            }
        }
    }
}
