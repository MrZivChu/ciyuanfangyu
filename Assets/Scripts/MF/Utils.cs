﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Utils
{
    /// <summary>
    /// 获取网络是否可用
    /// </summary>
    public static bool NetIsAvailable { get { return Application.internetReachability != NetworkReachability.NotReachable; } }

    /// <summary>
    /// wifi是否可用
    /// </summary>
    public static bool WifiIsAvailable { get { return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork; } }

    /// <summary>
    /// Get方式网络请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onFailed"></param>
    public static void GetHttp(string url, System.Action<string> onSuccess, System.Action<string> onFailed)
    {
        GameObject go = GameObject.Find("CoroutineHelper");
        go.GetComponent<CoroutineHelper>().StartCoroutine(HttpGet(url, onSuccess, onFailed));
    }

    private static System.Collections.IEnumerator HttpGet(string url, System.Action<string> onSuccess, System.Action<string> onFailed)
    {
        if (url.IndexOf('?') > 0)
        {
            if (!url.EndsWith("&")) url += "&";
        }
        else
        {
            url += "?";
        }
        url += "&appId=" + AppConfig.APP_ID;
        url += "&channelId=" + AppConfig.CHANNEL_ID;
        url += "&clientFoceVersion=" + AppConfig.APP_VERSION;

        WWW www = new WWW(url);
        yield return www;

        if (www.isDone && string.IsNullOrEmpty(www.error))
        {
            onSuccess(www.text);
        }
        else
        {
            onFailed(www.error);
        }
    }


    /// <summary>
    /// post方式网络请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="fields"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onFailed"></param>
    public static void PostHttp(string url, string fields, System.Action<string> onSuccess, System.Action<string> onFailed)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(fields))
        {
            string[] str = fields.Split('&');
            for (int i = 0; i + 1 < str.Length; i += 2)
            {
                dict[str[i]] = str[i + 1];
            }
        }
        GameObject go = GameObject.Find("DontDestroyOnLoad");
        go.GetComponent<CoroutineHelper>().StartCoroutine(HttpPost(url, dict, onSuccess, onFailed));
    }

    private static IEnumerator HttpPost(string url, Dictionary<string, string> fields, System.Action<string> onSuccess, System.Action<string> onFailed)
    {
        WWWForm form = new WWWForm();
        form.AddField("appId", AppConfig.APP_ID.ToString());
        form.AddField("channelId", AppConfig.CHANNEL_ID.ToString());
        form.AddField("clientFoceVersion", AppConfig.APP_VERSION);
        if (fields != null)
        {
            foreach (var item in fields)
            {
                form.AddField(item.Key, item.Value);
            }
        }
        WWW www = new WWW(url, form);
        yield return www;

        if (www.isDone && string.IsNullOrEmpty(www.error))
        {
            onSuccess(www.text);
        }
        else
        {
            if (www.error.Contains("Timed out"))
            {
                onFailed(StaticText.STR_TIMEOUT);
            }
            else if (www.error.Contains("Host unreachable"))
            {
                onFailed(StaticText.STR_UNREACHABLE);
            }
            else if (www.error.StartsWith("Could not resolve host"))
            {
                onFailed(StaticText.STR_NOT_RESOLVE);
            }
            else
            {
                onFailed(StaticText.STR_SERVER_FAILED);
            }
        }
        www.Dispose();
        www = null;
    }

    //加密AES
    public static string Encrypt(string toEncrypt, string salt)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(salt);
        SHA256 sha256 = new SHA256Managed();
        keyArray = sha256.ComputeHash(keyArray);

        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
        byte[] ivArray = UTF8Encoding.UTF8.GetBytes("writedbyMrzivchu");

        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.IV = ivArray;
        rDel.Mode = CipherMode.CBC;
        rDel.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = rDel.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return System.Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    //解密AES
    public static string Decrypt(string toDecrypt, string salt)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(salt);
        SHA256 sha256 = new SHA256Managed();
        keyArray = sha256.ComputeHash(keyArray);

        byte[] ivArray = UTF8Encoding.UTF8.GetBytes("writedbyMrzivchu");
        byte[] toEncryptArray = System.Convert.FromBase64String(toDecrypt);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.IV = ivArray;
        rDel.Mode = CipherMode.CBC;
        rDel.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = rDel.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    #region PlayerPrefs
    static string GetKey(string key)
    {
        return AppConfig.APP_NAME + "_" + key;
    }

    public static bool HasKey(string key)
    {
        string name = GetKey(key);
        return PlayerPrefs.HasKey(name);
    }

    public static int GetInt(string key, int value)
    {
        string name = GetKey(key);
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetInt(name);
        else
            return value;
    }

    public static string GetString(string key, string value)
    {
        string name = GetKey(key);
        if (PlayerPrefs.HasKey(name))
        {
            string str = PlayerPrefs.GetString(name);
            str = WWW.UnEscapeURL(str);
            return str;
        }
        else
        {
            return value;
        }
    }

    public static void SetInt(string key, int value)
    {
        string name = GetKey(key);
        PlayerPrefs.SetInt(name, value);
    }

    public static void SetString(string key, string value)
    {
        string name = GetKey(key);
        value = WWW.EscapeURL(value);//用url编码,否则无法识别中文
        PlayerPrefs.SetString(name, value);
    }

    public static void RemoveKey(string key)
    {
        string name = GetKey(key);
        PlayerPrefs.DeleteKey(name);
    }
    #endregion

    public static bool UncompressMemory(byte[] bytes)
    {
        using (var ms = new MemoryStream(bytes))
        {
            using (var ar = SharpCompress.Archive.ArchiveFactory.Open(ms))
            {
                foreach (var item in ar.Entries)
                {
                    if (!item.IsDirectory)
                    {
                        string file = AppConfig.HotAssetsPath + item.FilePath;
                        string path = Path.GetDirectoryName(file);
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                        using (FileStream fs = new FileStream(file, FileMode.Create))
                        {
                            item.WriteTo(fs);
                        }
                    }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 设置对象高亮
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isLight"></param>
    /// <param name="color"></param>
    public static void SetObjectHighLight(GameObject go, bool isLight, Color startColor, Color endColor, float flashingDelay = 0, float flashingFrequency = 2)
    {
        if (isLight)
        {
            FlashingController flashingController = go.GetComponent<FlashingController>();
            if (flashingController == null)
            {
                flashingController = go.AddComponent<FlashingController>();
                flashingController.flashingDelay = flashingDelay;
                flashingController.flashingFrequency = flashingFrequency;
            }
            else
            {
                if (startColor == Color.clear)
                {
                    startColor = flashingController.flashingStartColor;
                }
                if (endColor == Color.clear)
                {
                    endColor = flashingController.flashingEndColor;
                }
            }
            flashingController.flashingStartColor = startColor;
            flashingController.flashingEndColor = endColor;
            HighlightableObject highlightableObject = go.GetComponent<HighlightableObject>();
            if (highlightableObject)
            {
                highlightableObject.FlashingParams(startColor, endColor, flashingFrequency);
                highlightableObject.FlashingOn();
            }
        }
        else
        {
            HighlightableObject highlightableObject = go.GetComponent<HighlightableObject>();
            if (highlightableObject)
            {
                highlightableObject.FlashingOff();
            }
        }
    }

    public static bool CheckGuiRaycastObjects()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
#if IPHONE || ANDROID
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public static void SpawnUIObj(Transform child, Transform parent)
    {
        child.parent = parent;
        child.localScale = Vector3.one;
        child.localPosition = Vector3.zero;
    }
}
