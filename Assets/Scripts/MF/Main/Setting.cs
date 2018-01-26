using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public HUD hudScript;
    public Button closeBtn;
    public Slider slider;

    public Toggle closeMusicToggle;

    public Button leftBtn;
    public Button rightBtn;
    public Text musicName;
    public List<AudioClip> audioClipList;

    AudioSource audioSource;
    void Start()
    {
        GameObject go = GameObject.Find("DontDestroyOnLoad");
        if (go != null)
        {
            audioSource = go.GetComponent<AudioSource>();
            closeBtn.onClick.AddListener(ClosePanel);
            slider.onValueChanged.AddListener(AudioSourceChanged);
            closeMusicToggle.onValueChanged.AddListener(CloseMusic);
            slider.value = Convert.ToSingle(BaseDataLibrary.musicVolume);

            leftBtn.onClick.AddListener(leftClick);
            rightBtn.onClick.AddListener(rightClick);

            index = BaseDataLibrary.musicIndex;
            musicName.text = audioClipList[index].name;
        }
    }

    int index = 0;
    void leftClick()
    {
        if (audioSource != null && audioClipList != null && audioClipList.Count > 0)
        {
            index--;
            if (index < 0)
            {
                index = audioClipList.Count - 1;
            }
            audioSource.clip = audioClipList[index];
            musicName.text = audioClipList[index].name;
            BaseDataLibrary.musicIndex = index;
            GameJsonDataHelper.UpdateBaseDataMusicIndex(index);
            audioSource.Play();
        }
    }

    void rightClick()
    {
        if (audioSource != null && audioClipList != null && audioClipList.Count > 0)
        {
            index++;
            if (index >= audioClipList.Count)
            {
                index = 0;
            }
            audioSource.clip = audioClipList[index];
            musicName.text = audioClipList[index].name;
            BaseDataLibrary.musicIndex = index;
            GameJsonDataHelper.UpdateBaseDataMusicIndex(index);
            audioSource.Play();
        }
    }

    void ClosePanel()
    {
        hudScript.ResetTweenPlay();
        Destroy(gameObject);
    }

    void CloseMusic(bool isOn)
    {
        slider.value = isOn == false ? slider.value : 0;
    }

    public void AudioSourceChanged(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
            BaseDataLibrary.musicVolume = value;
            GameJsonDataHelper.UpdateBaseDataMusicVolume(value);
        }
    }
}
