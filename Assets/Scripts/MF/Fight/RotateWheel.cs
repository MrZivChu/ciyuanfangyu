using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float perRotateTime;
    public float sensitiveDrag;
    public Transform centerPoint;
    public float upDownSpeed;


    bool isRotating = false;
    Transform preBuild;
    Transform nowBuild;

    Vector2 prePosition = Vector2.zero;
    Vector2 nowPosition = Vector2.zero;
    bool startRotate = false;

    float tempTime = 0;
    float preAngle = 0;
    float nowAngle = 0;
    float perRotateAngle = 45;
    int direction = 1;//为负值表示逆时针，-1为顺时针，0为不旋转
    int flag = 1;//标志是点的屏幕哪个区域
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                nowBuild = null;
                if (hit.transform.CompareTag("build"))
                {
                    nowBuild = hit.transform.parent;
                }
                if (nowBuild != null)
                {
                    if (preBuild != null)
                    {
                        Utils.SetObjectHighLight(preBuild.gameObject, false, Color.clear, Color.clear);
                    }
                    Utils.SetObjectHighLight(nowBuild.gameObject, true, new Color(1, 164f / 255, 0, 1), new Color(212f / 255, 153f / 255, 47f / 255, 1));
                    preBuild = nowBuild;
                    prePosition = mousePosition;
                    nowPosition = prePosition;
                    if (nowBuild.CompareTag("oneInLand"))
                    {
                        perRotateAngle = 45f;
                    }
                    else if (nowBuild.CompareTag("twoInLand"))
                    {
                        perRotateAngle = 22.5f;
                    }
                    else if (nowBuild.CompareTag("threeInLand"))
                    {
                        perRotateAngle = 15f;
                    }
                    else if (nowBuild.CompareTag("fourInLand"))
                    {
                        perRotateAngle = 11.25f;
                    }
                    isRotating = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            #if !UNITY_EDITOR
//#if true
            //手机操作
            if (Input.touchCount == 1 && !startRotate)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    JudgeFlag(nowPosition, centerPoint);
                    nowPosition = Input.mousePosition;
                    if (flag == 1)
                    {
                        if (Mathf.Abs(nowPosition.y - prePosition.y) >= sensitiveDrag)
                        {
                            if (nowPosition.y > prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = 0;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = -1;
                                    }
                                }
                                else
                                {
                                    direction = -1;
                                }
                            }
                            else if (nowPosition.y < prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = 0;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = 1;
                                    }
                                }
                                else
                                {
                                    direction = 1;
                                }
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                            {
                                if (nowPosition.x > prePosition.x)
                                {
                                    direction = 1;
                                }
                                else if (nowPosition.x < prePosition.x)
                                {
                                    direction = -1;
                                }
                            }
                            else
                            {
                                direction = 0;
                            }
                        }
                    }
                    else if (flag == 2)
                    {
                        if (Mathf.Abs(nowPosition.y - prePosition.y) >= sensitiveDrag)
                        {
                            if (nowPosition.y > prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = -1;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = -1;
                                    }
                                }
                                else
                                {
                                    direction = -1;
                                }
                            }
                            else if (nowPosition.y < prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = 0;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = 1;
                                    }
                                }
                                else
                                {
                                    direction = 1;
                                }
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                            {
                                if (nowPosition.x > prePosition.x)
                                {
                                    direction = -1;
                                }
                                else if (nowPosition.x < prePosition.x)
                                {
                                    direction = -1;
                                }
                            }
                            else
                            {
                                direction = 0;
                            }
                        }
                    }
                    else if (flag == 3)
                    {
                        if (Mathf.Abs(nowPosition.y - prePosition.y) >= sensitiveDrag)
                        {
                            if (nowPosition.y > prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = 0;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = 1;
                                    }
                                }
                                else
                                {
                                    direction = 1;
                                }
                            }
                            else if (nowPosition.y < prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = -1;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = 0;
                                    }
                                }
                                else
                                {
                                    direction = -1;
                                }
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                            {
                                if (nowPosition.x > prePosition.x)
                                {
                                    direction = -1;
                                }
                                else if (nowPosition.x < prePosition.x)
                                {
                                    direction = 1;
                                }
                            }
                            else
                            {
                                direction = 0;
                            }
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(nowPosition.y - prePosition.y) >= sensitiveDrag)
                        {
                            if (nowPosition.y > prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = 1;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = -1;
                                    }
                                }
                                else
                                {
                                    direction = 1;
                                }
                            }
                            else if (nowPosition.y < prePosition.y)
                            {
                                if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                                {
                                    if (nowPosition.x > prePosition.x)
                                    {
                                        direction = 0;
                                    }
                                    else if (nowPosition.x < prePosition.x)
                                    {
                                        direction = -1;
                                    }
                                }
                                else
                                {
                                    direction = -1;
                                }
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(nowPosition.x - prePosition.x) >= sensitiveDrag)
                            {
                                if (nowPosition.x > prePosition.x)
                                {
                                    direction = -1;
                                }
                                else if (nowPosition.x < prePosition.x)
                                {
                                    direction = 1;
                                }
                            }
                            else
                            {
                                direction = 0;
                            }
                        }
                    }
                    startRotate = true;
                    prePosition = nowPosition;
                }
            }
#else
            nowBuild.Rotate(Vector3.forward, -perRotateTime * (Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y")));
#endif
        }

        if (startRotate)
        {
            tempTime = tempTime + Time.deltaTime;
            if (tempTime <= perRotateTime)
            {
                nowAngle = Mathf.Lerp(0, perRotateAngle, tempTime * (1 / perRotateTime));
                nowBuild.Rotate(Vector3.forward, direction * (nowAngle - preAngle));
                preAngle = nowAngle;
                //第一版（差值为0的旋转）
                //nowBuild.Rotate(Vector3.forward, perRotateAngle * Time.deltaTime * -(1 / perRotateTime));
            }
            else
            {
                nowBuild.Rotate(Vector3.forward, direction * (perRotateAngle - preAngle));
                startRotate = false;
                tempTime = 0;
                preAngle = 0;
                nowAngle = 0;
            }

            //开始旋转的时候物体上移
            if (nowBuild.localPosition.z < 1)
            {
                Vector3 v = nowBuild.localPosition;
                v.z += Time.deltaTime * upDownSpeed;
                if (v.z >= 1)
                {
                    v.z = 1;
                }
                nowBuild.localPosition = v;
            }
        }
        else
        {
            //结束旋转的时候物体下移
            if (nowBuild != null)
            {
                if (nowBuild.localPosition.z > 0)
                {
                    Vector3 v = nowBuild.localPosition;
                    v.z -= Time.deltaTime * upDownSpeed;
                    if (v.z <= 0)
                    {
                        v.z = 0;
                    }
                    nowBuild.localPosition = v;
                }
            }
        }
    }

    void JudgeFlag(Vector2 mousePosition, Transform theCenterPoint)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(theCenterPoint.position);
        if (mousePosition.x > screenPosition.x && mousePosition.y > screenPosition.y)
        {
            flag = 1;
        }
        else if (mousePosition.x > screenPosition.x && mousePosition.y < screenPosition.y)
        {
            flag = 2;
        }
        else if (mousePosition.x < screenPosition.x && mousePosition.y < screenPosition.y)
        {
            flag = 3;
        }
        else
        {
            flag = 4;
        }
    }
}
