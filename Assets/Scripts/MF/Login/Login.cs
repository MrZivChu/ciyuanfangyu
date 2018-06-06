using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Button okBtn;
    void Start()
    {
        EventTriggerListener.Get(okBtn.gameObject).onClick = EnterGame;
    }

    void EnterGame(GameObject go, object data)
    {       
        Loading.sceneName = "Story";
        SceneManager.LoadScene("Loading");
    }
}
