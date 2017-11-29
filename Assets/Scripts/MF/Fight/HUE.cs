using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUE : MonoBehaviour
{   
    public GameObject result;
    public Button okBtn;

    private void Start()
    {
        EventTriggerListener.Get(okBtn.gameObject).onClick = FightOverClick;
    }

    void FightOverClick(GameObject go, object data)
    {
        result.SetActive(true);
    }}
