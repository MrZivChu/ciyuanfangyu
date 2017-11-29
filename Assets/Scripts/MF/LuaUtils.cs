using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuaUtils 
{
    public static void LoadLevel(string tSceneName)
    {
        Loading.sceneName = tSceneName;
        SceneManager.LoadScene(1);
    }
}
