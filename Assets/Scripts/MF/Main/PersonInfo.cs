using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInfo : MonoBehaviour
{
    public HUD hudScript;
    public Button closeBtn;
    void Start()
    {
        closeBtn.onClick.AddListener(ClosePanel);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
        hudScript.ResetTweenPlay();
    }
}
