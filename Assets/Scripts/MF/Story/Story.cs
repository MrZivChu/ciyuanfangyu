using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using uTools;

public class Story : MonoBehaviour
{
    int index = 0;
    public Button nextBtn;
    public Button SkipBtn;
    public Text contentText;

    public uTweenScale uTweenScale;

    public uTweenScale[] nextuTweenScale;
    public uTweenAlpha nextuTweenAlpha;

    List<string> msgList = new List<string>()
    {
        "公元2209年，本已和平几百年的世界，忽然被一块神秘的外星陨石所打破\n陨石着落在南印度洋，奇迹般的外星陨石没有落到海中\n而是在水上形成了一个人工岛",
        "联合国派出的6批调查队都有去无回\n直到有一天，从陨石坠落地涌出大量外星生物！\n地球人口80%在一个月内被毁灭！地球危在旦夕！",
        "地球上仅存的人类，龟缩在几个要塞型城市中！\n由地球指挥部统一指挥！\n指挥官！地球存亡，全靠你了"
    };


    void Start()
    {
        uTweenScale = contentText.GetComponent<uTweenScale>();
        nextuTweenScale = nextBtn.GetComponents<uTweenScale>();
        nextuTweenAlpha = nextBtn.GetComponent<uTweenAlpha>();
        SkipBtn.onClick.AddListener(Skip);
        nextBtn.onClick.AddListener(Next);
        Next();
    }

    void Next()
    {
        if (index < msgList.Count)
        {
            nextBtn.gameObject.SetActive(false);
            Invoke("ShowNextBtn", 1.5f);
            uTweenScale.Reset();
            if (index == msgList.Count - 1)
            {
                nextBtn.onClick.RemoveAllListeners();
                nextBtn.onClick.AddListener(Skip);
            }
            string content = msgList[index];
            contentText.text = content;
            index++;
        }
    }

    void ShowNextBtn()
    {
        nextBtn.transform.localScale = Vector3.zero;
        nextBtn.gameObject.SetActive(true);
        nextuTweenScale[0].Reset();
        nextuTweenScale[1].Reset();
        nextuTweenAlpha.Reset();
    }

    void Skip()
    {
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }
}
