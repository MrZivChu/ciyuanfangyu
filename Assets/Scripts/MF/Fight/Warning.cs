using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    void Update()
    {
        if (isFinished == true)
        {

        }
    }

    bool isFinished = false;
    public void AnimaFinished()
    {
        isFinished = true;
    }
}
