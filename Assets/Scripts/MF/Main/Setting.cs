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

    AudioSource audioSource;
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        audioSource.volume = slider.value;
        closeBtn.onClick.AddListener(ClosePanel);
        slider.onValueChanged.AddListener(AudioSourceChanged);
        closeMusicToggle.onValueChanged.AddListener(CloseMusic);
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
        audioSource.volume = value;
    }
}
