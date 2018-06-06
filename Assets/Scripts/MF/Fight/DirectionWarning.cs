using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionWarning : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    public void PlayAnim()
    {
        animator.enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        tempTime = 0;
        canShow = true;
    }

    bool canShow = false;
    float showTime = 8f;
    float tempTime = 0f;
    void Update()
    {
        if (canShow)
        {
            tempTime += Time.deltaTime;
            if (tempTime >= showTime)
            {
                canShow = false;
                animator.enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
