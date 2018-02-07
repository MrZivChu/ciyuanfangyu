﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRotateWheel : MonoBehaviour
{
    public float upDownSpeed;
    public Transform centerObj;

    Transform preHitBuild;
    Transform nowHitBuild;
    Transform currentDisk;

    float diskEveryTimeNeedRotateAngle = 45;
    float rotateDiskDirection = 1;//负值为顺时针，正值为逆时针
    int whichDisk = 1;

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GroupCheck.canCheck = false;
            RaycastHit hit;
            Vector2 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            int layMask = 1 << LayerMask.NameToLayer("CheckWheel");
            if (Physics.Raycast(ray, out hit, 1000, layMask))
            {
                currentDisk = null;
                //if (hit.transform.CompareTag("build"))
                {
                    nowHitBuild = hit.transform.parent;
                    currentDisk = hit.transform.parent.parent.parent;
                }
                if (currentDisk != null)
                {
                    if (preHitBuild != null)
                    {
                        Utils.SetObjectEmissionColor(preHitBuild.gameObject, new Color(0, 0, 0));
                    }
                    Utils.SetObjectEmissionColor(nowHitBuild.gameObject, new Color(0.35f, 0.31f, 0.16f));
                    preHitBuild = nowHitBuild;
                    if (currentDisk.CompareTag("oneInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 45f;
                        whichDisk = 1;
                    }
                    else if (currentDisk.CompareTag("twoInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 22.5f;
                        whichDisk = 2;
                    }
                    else if (currentDisk.CompareTag("threeInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 15f;
                        whichDisk = 3;
                    }
                    else if (currentDisk.CompareTag("fourInLand"))
                    {
                        diskEveryTimeNeedRotateAngle = 11.25f;
                        whichDisk = 4;
                    }
                    leftNeedRotateAngle = diskEveryTimeNeedRotateAngle;
                }
            }
        }

        RotateEndHandle();

        if (isRotating)
        {
            Up(currentDisk, Time.deltaTime);
        }
        if (isStop)
        {
            Down(currentDisk, Time.deltaTime);
        }
    }

    [HideInInspector]
    public bool isStop = false;
    bool isRotating = false;
    float tempTime = 0;
    float preAngle = 0;
    float nowAngle = 0;
    float homingTime = 0.2f;
    float leftNeedRotateAngle = 0;
    //旋转结束后的处理
    void RotateEndHandle()
    {
        if (leftNeedRotateAngle > 0 && currentDisk != null && isRotating)
        {
            tempTime = tempTime + Time.deltaTime;
            if (tempTime <= homingTime)
            {
                nowAngle = Mathf.Lerp(0, leftNeedRotateAngle, tempTime * (1 / homingTime));
                currentDisk.Rotate(Vector3.forward, -rotateDiskDirection * (nowAngle - preAngle));
                preAngle = nowAngle;
            }
            else
            {
                currentDisk.Rotate(Vector3.forward, -rotateDiskDirection * (leftNeedRotateAngle - preAngle));
                tempTime = 0;
                preAngle = 0;
                nowAngle = 0;
                isRotating = false;
            }
        }
    }

    //isClockwise是否顺时针
    public void Rotate(bool isClockwise)
    {
        if (isRotating)
            return;
        isRotating = true;
        rotateDiskDirection = isClockwise ? -1 : 1;
    }

    //开始旋转的时候物体上移
    void Up(Transform tNowDisk, float deltaTime)
    {
        if (tNowDisk != null)
        {
            float maxValue = whichDisk == 1 ? 1.5f : whichDisk == 2 ? 2f : whichDisk == 3 ? 1.45f : 0.65f;
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
            else
            {
                isStop = false;
                CheckGroup();
            }
        }
    }

    public List<GroupCheck> groupCheckList = new List<GroupCheck>();
    void CheckGroup()
    {
        GroupCheck.canCheck = true;
        if (groupCheckList != null && groupCheckList.Count > 0)
        {
            for (int i = 0; i < groupCheckList.Count; i++)
            {
                groupCheckList[i].Check();
            }
        }
    }
}
