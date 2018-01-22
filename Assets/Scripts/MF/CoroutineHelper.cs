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
        audioSource.volume = Convert.ToSingle(BaseDataLibrary.musicVolume);
        audioSource.clip = audioClipList[BaseDataLibrary.musicIndex];
        audioSource.Play();
    }

    //恢复用户数据
    void RecoverBaseData()
    {
        ServerBaseDataLibrary serverBaseDataLibrary = GameJsonDataHelper.ReadBaseData();
        BaseDataLibrary.coins = serverBaseDataLibrary.coins;
        BaseDataLibrary.diamond = serverBaseDataLibrary.diamond;
        BaseDataLibrary.engry = serverBaseDataLibrary.engry;
        BaseDataLibrary.focus = serverBaseDataLibrary.focus;
        BaseDataLibrary.happinessDegree = serverBaseDataLibrary.happinessDegree;
        BaseDataLibrary.mercenaryMoney = serverBaseDataLibrary.mercenaryMoney;
        BaseDataLibrary.musicVolume = serverBaseDataLibrary.musicVolume;
        BaseDataLibrary.people = serverBaseDataLibrary.people;
        BaseDataLibrary.musicIndex = serverBaseDataLibrary.musicIndex;
        BaseDataLibrary.mercenaryList = serverBaseDataLibrary.mercenaryList;
        BaseDataLibrary.battleMercenaryList = serverBaseDataLibrary.battleMercenaryList;
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
        GameJsonDataHelper.WriteBaseData();
        GameJsonDataHelper.WriteBatteryData();
    }
}
