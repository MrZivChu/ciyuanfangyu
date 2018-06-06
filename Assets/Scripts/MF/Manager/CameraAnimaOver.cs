using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimaOver : MonoBehaviour
{
    public HUM humScript;
    public HUE hueScript;
    public RotateCamera rotateCameraScript;
    public Animator animator;
    public EnemyManager enemyManager;

    private void Start()
    {
        rotateCameraScript.enabled = false;
    }

    public void AnimaOver()
    {
        animator.enabled = false;
        if (humScript != null)
            humScript.PlayHumAnima();
        if (hueScript != null)
            hueScript.PlayHumAnima();
        if (enemyManager != null)
            enemyManager.enabled = true;
        rotateCameraScript.enabled = true;
    }
}
