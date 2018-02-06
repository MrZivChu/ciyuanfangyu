using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        RecoverBaseData();
        RecoverMusic();
    }

    public List<AudioClip> audioClipList;
    //恢复音乐设置
    void RecoverMusic()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = Convert.ToSingle(LocalBaseData.musicVolume);
        audioSource.clip = audioClipList[LocalBaseData.musicIndex];
        audioSource.Play();
    }

    //恢复用户数据
    void RecoverBaseData()
    {
        ServerBaseData serverBaseDataLibrary = ServerDataHelper.GetServerBaseData();
        LocalBaseData.coins = serverBaseDataLibrary.coins;
        LocalBaseData.diamond = serverBaseDataLibrary.diamond;
        LocalBaseData.engry = serverBaseDataLibrary.engry;
        LocalBaseData.focus = serverBaseDataLibrary.focus;
        LocalBaseData.happinessDegree = serverBaseDataLibrary.happinessDegree;
        LocalBaseData.mercenaryMoney = serverBaseDataLibrary.mercenaryMoney;
        LocalBaseData.musicVolume = serverBaseDataLibrary.musicVolume;
        LocalBaseData.people = serverBaseDataLibrary.people;
        LocalBaseData.musicIndex = serverBaseDataLibrary.musicIndex;
        LocalBaseData.mercenaryList = serverBaseDataLibrary.mercenaryList;
        LocalBaseData.battleMercenaryList = serverBaseDataLibrary.battleMercenaryList;
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    //杀游戏进程时是不会调用OnApplicationQuit的，所以用此事件进行数据保存，防止数据没法保存
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    void SaveData()
    {
        ServerDataHelper.UpdateServerBaseData();
        ServerDataHelper.UpdateServerBatteryData();
    }
}
