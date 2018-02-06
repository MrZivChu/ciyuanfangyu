using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryBag : MonoBehaviour
{
    public Transform parent;
    public Button backBtn;
    public HUD hudScript;
    Object item;
    void Start()
    {
        EventTriggerListener.Get(backBtn.gameObject).onClick = BackClick;
        item = Resources.Load("UI/Mercenary/MercenaryBagItem");
        InitData();
    }

    void InitData()
    {
        List<int> idList = LocalBaseData.mercenaryList;
        if (idList != null && idList.Count > 0)
        {
            for (int i = 0; i < idList.Count; i++)
            {
                GameObject go = Instantiate(item) as GameObject;
                go.transform.parent = parent;
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
                MercenaryConfigInfo mer = MercenaryDataConfigTable.MercenaryList.Find(it => it.ID == idList[i]);
                if (mer != null)
                {
                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(mer.iconPath.Replace("zm", "bb"));
                    go.transform.GetChild(2).GetComponent<Text>().text = mer.mercenaryName;
                    go.transform.GetChild(4).GetComponent<Text>().text = mer.talent;
                    go.transform.GetChild(5).GetComponent<Text>().text = mer.star.ToString();
                    ChangeStatus(go, mer);
                }
            }
        }
    }

    void ChangeStatus(GameObject go, MercenaryConfigInfo mer)
    {
        Button upBtn = go.transform.GetChild(6).GetComponent<Button>();
        Button downBtn = go.transform.GetChild(7).GetComponent<Button>();
        List<object> list = new List<object>() { go, mer };
        if (LocalBaseData.battleMercenaryList.Contains(mer.ID))
        {
            upBtn.gameObject.SetActive(false);
            downBtn.gameObject.SetActive(true);
            EventTriggerListener.Get(downBtn.gameObject, list).onClick = DownClick;
        }
        else
        {
            upBtn.gameObject.SetActive(true);
            EventTriggerListener.Get(upBtn.gameObject, list).onClick = UpClick;
            downBtn.gameObject.SetActive(false);
        }
    }

    void BackClick(GameObject go, object data)
    {
        hudScript.ResetTweenPlay();
        Destroy(gameObject);
    }

    //上阵
    void UpClick(GameObject go, object data)
    {
        if (LocalBaseData.battleMercenaryList.Count >= 6)
        {
            MessageBox.Instance.PopOK("上阵佣兵已经满6人\n请下阵其他佣兵后方可上阵此佣兵", null, "确定");
        }
        else
        {
            List<object> list = (List<object>)data;
            MercenaryConfigInfo mer = (MercenaryConfigInfo)list[1];
            GameObject goes = (GameObject)list[0];
            LocalBaseData.battleMercenaryList.Add(mer.ID);
            ChangeStatus(goes, mer);
        }
    }

    //下阵
    void DownClick(GameObject go, object data)
    {
        List<object> list = (List<object>)data;
        MercenaryConfigInfo mer = (MercenaryConfigInfo)list[1];
        GameObject goes = (GameObject)list[0];
        LocalBaseData.battleMercenaryList.Remove(mer.ID);
        ChangeStatus(goes, mer);
    }
}
