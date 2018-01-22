using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimaOver : MonoBehaviour
{
    public HUM humScript;
    public RotateCamera rotateCameraScript;
    public Animator animator;

    private void Start()
    {
        rotateCameraScript.enabled = false;
    }

    public void AnimaOver()
    {
        animator.enabled = false;
        humScript.PlayHumAnima();
        rotateCameraScript.enabled = true;
    }
}
