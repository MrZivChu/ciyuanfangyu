using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType
{
    cannonBatteryLv1 = 1,
    cannonBatteryLv2 = 2,
    cannonBatteryLv3 = 3,
    gatlinGunBatteryLv1 = 4,
    gatlinGunBatteryLv2 = 5,
    gatlinGunBatteryLv3 = 6,
    missileBatteryLv1 = 7,
    missileBatteryLv2 = 8,
    missileBatteryLv3 = 9,
    diskUpDown = 10,
    diskSelected = 11,
}

public class MusicManager : MonoBehaviour
{
    public static Dictionary<MusicType, string> dic = new Dictionary<MusicType, string>()
    {
        { MusicType.cannonBatteryLv1 ,"batteryMusic/战斗加农炮01" },
        { MusicType.cannonBatteryLv2 ,"batteryMusic/战斗加农炮02" },
        { MusicType.cannonBatteryLv3 ,"batteryMusic/战斗加农炮03" },
        { MusicType.gatlinGunBatteryLv1 ,"batteryMusic/战斗加特林_01" },
        { MusicType.gatlinGunBatteryLv2 ,"batteryMusic/战斗加特林_02" },
        { MusicType.gatlinGunBatteryLv3 ,"batteryMusic/战斗加特林_03" },
        { MusicType.missileBatteryLv1 ,"batteryMusic/战斗导弹01" },
        { MusicType.missileBatteryLv2 ,"batteryMusic/战斗导弹02" },
        { MusicType.missileBatteryLv3 ,"batteryMusic/战斗导弹03" },
        { MusicType.diskUpDown ,"otherMusic/圆盘齿轮上下落起-声音" },
        { MusicType.diskSelected ,"otherMusic/选择圆盘-声音" },
    };

    public static void Play(MusicType musicType)
    {
        string musicPath = dic[musicType];
        AudioClip audioClip = Resources.Load<AudioClip>(musicPath);
        GameObject musicObj = Instantiate(Resources.Load("Music")) as GameObject;
        DelayDestoryOwn delayDestoryOwn = musicObj.GetComponent<DelayDestoryOwn>();
        delayDestoryOwn.delay = audioClip.length;
        AudioSource audioSource = musicObj.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
