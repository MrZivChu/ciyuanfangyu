using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uTools;

public class Story : MonoBehaviour
{
    public Button SkipBtn;
    public Text contentText;
    public uTweenPosition zhuanchangTweenPosition;
    public uTweenScale peopleTweenScale;
    public Transform peopleTransform;
    public uTweenScale descTweenScale;
    public Transform descTransform;

    public List<Sprite> bgSpriteList;

    List<string> msgList = new List<string>()
    {
        "公元2209年，本已和平几百年的\n世界，忽然被一块神秘的外星陨石所打破陨石着落在南印度洋，奇迹般的外星陨石没有落到海中而是在水上形成了一个人工岛",
        "联合国派出的6批调查队都有去\n无回直到有一天，从陨石坠落地涌出大量外星生物！地球人口80%在一个月内被毁灭！\n地球危在旦夕！",
        "地球上仅存的人类，龟缩在几个\n要塞型城市中！\n由地球指挥部统一指挥！\n指挥官！地球存亡，全靠你了"
    };


    void Start()
    {
        SkipBtn.onClick.AddListener(Skip);
        Skip();
    }

    public Image bgImage1;
    public Image bgImage2;
    int index = 0;
    GameObject tempObj;
    void Skip()
    {
        if (bgSpriteList.Count > 0 && index + 1 <= bgSpriteList.Count)
        {
            peopleTransform.localScale = Vector3.zero;
            descTransform.localScale = new Vector3(1, 0, 1);
            if (index == 1)
                zhuanchangTweenPosition.Reset();
            if (index % 2 != 0)
            {
                bgImage1.sprite = bgSpriteList[index];
                bgImage2.sprite = bgSpriteList[index + 1];
            }
            if (bgImage1.gameObject == tempObj)
            {
                bgImage1.gameObject.SetActive(false);
                bgImage2.gameObject.SetActive(true);
                tempObj = bgImage2.gameObject;
            }
            else
            {
                bgImage1.gameObject.SetActive(true);
                bgImage2.gameObject.SetActive(false);
                tempObj = bgImage1.gameObject;
            }
            tempObj.GetComponent<uTweenPosition>().Reset();

            if (index < msgList.Count)
            {
                contentText.text = msgList[index];
            }
            index++;
            peopleTweenScale.Reset();
            descTweenScale.Reset();
            return;
        }
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }
}
