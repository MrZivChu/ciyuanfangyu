using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public HUD hudScript;
    public Button closeBtn;
    public Slider slider;

    public AudioSource audioSource;
    void Start()
    {
        audioSource.volume = slider.value;
        closeBtn.onClick.AddListener(ClosePanel);
        slider.onValueChanged.AddListener(AudioSourceChanged);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
        hudScript.ResetTweenPlay();
    }

    public void AudioSourceChanged(float value)
    {
        audioSource.volume = value;
    }
}
