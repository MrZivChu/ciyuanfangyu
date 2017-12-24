using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruit : MonoBehaviour
{
    public Button backBtn;
    public Animator animator;
    public HUD hudScript;
    void Start()
    {
        EventTriggerListener.Get(backBtn.gameObject).onClick = BackClick;
    }

    private void OnEnable()
    {
        animator.enabled = true;
        animator.SetBool("start", true);
    }

    void BackClick(GameObject go, object data)
    {
        gameObject.SetActive(false);
        animator.SetBool("start", false);
        hudScript.ResetTweenPlay();
    }
}
