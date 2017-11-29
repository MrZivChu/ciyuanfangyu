using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    List<string> tipList = new List<string>()
    {
        "提升炮塔的等级，就拥有更多的伤害",
        "合理建造建筑，有利于自我防守",
        "胜不骄，败不馁！"
    };

    public Text tipText;
    public static string sceneName;
    public Slider slider;

    int randomIndex = 0;
    void Start()
    {
        if (tipList.Count > 0)
        {
            randomIndex = Random.Range(0, tipList.Count - 1);
            tipText.text = tipList[randomIndex];
        }
        slider.value = 0;
        StartCoroutine(LoadLevelAsyn(sceneName));
    }

    //让loading效果更加圆滑
    //http://blog.csdn.net/huang9012/article/details/38659011
    IEnumerator LoadLevelAsyn(string tSceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(tSceneName);

        int displayProgress = 0;
        int toProgress = 0;
        op.allowSceneActivation = false;
        //把AsyncOperation.allowSceneActivation设为false就可以禁止Unity加载完毕后自动切换场景
        //但allowSceneActivation设置成false,百分比最多到0.9
        while (op.progress < 0.9f)
        {
            toProgress = (int)(op.progress * 100);
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                slider.value = (displayProgress * 0.01f);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
        //最后一步,此时场景已经结束
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            slider.value = (displayProgress * 0.01f);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }
}
