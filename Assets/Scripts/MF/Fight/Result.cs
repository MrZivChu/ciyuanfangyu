using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Button backBaseBtn;
    public Button backHeadquartersBtn;
    void Start()
    {
        EventTriggerListener.Get(backBaseBtn.gameObject).onClick = BackManagerClick;
        EventTriggerListener.Get(backHeadquartersBtn.gameObject).onClick = BackClick;
    }

    void ResetAudio()
    {
        AudioSource mainAudioSource = GameObject.Find("DontDestroyOnLoad").GetComponent<AudioSource>();
        mainAudioSource.enabled = true;
    }

    void BackClick(GameObject go, object data)
    {
        ResetAudio();
        Loading.sceneName = "Main";
        SceneManager.LoadScene("Loading");
    }

    void BackManagerClick(GameObject go, object data)
    {
        ResetAudio();
        RecoverManagerBatteryData.fromFightScene = true;
        Loading.sceneName = "Manager";
        SceneManager.LoadScene("Loading");
    }
}
