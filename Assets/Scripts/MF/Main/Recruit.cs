using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruit : MonoBehaviour
{
    public Button backBtn;
    public Button refreshBtn;
    public HUD hudScript;

    public List<Transform> itemList;

    void Start()
    {
        EventTriggerListener.Get(backBtn.gameObject).onClick = BackClick;
        EventTriggerListener.Get(refreshBtn.gameObject).onClick = RefreshClick;
        RefreshClick(null, null);
    }

    void BackClick(GameObject go, object data)
    {
        GameJsonDataHelper.WriteMercenaryData();
        hudScript.ResetTweenPlay();
        Destroy(gameObject);
    }

    int index = 0;
    void RefreshClick(GameObject go, object data)
    {
        List<Mercenary> mercenaryList = MercenaryDataConfigTable.MercenaryList;

        if (itemList != null && itemList.Count > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(mercenaryList[index].iconPath);
                itemList[i].GetChild(11).GetComponent<Text>().text = mercenaryList[index].mercenaryName;
                itemList[i].GetChild(4).GetComponent<Text>().text = mercenaryList[index].price + "G";
                itemList[i].GetChild(6).GetComponent<Text>().text = mercenaryList[index].talent;
                itemList[i].GetChild(8).GetComponent<Text>().text = "+" + mercenaryList[index].shootDistance;
                itemList[i].GetChild(10).GetComponent<Text>().text = "+" + mercenaryList[index].shootPower;
                int starCount = mercenaryList[index].star;
                for (int j = 0; j < starCount; j++)
                {
                    itemList[i].GetChild(2).GetChild(j).gameObject.SetActive(true);
                }
                for (int j = starCount; j < 4; j++)
                {
                    itemList[i].GetChild(2).GetChild(j).gameObject.SetActive(false);
                }
                if (BaseDataLibrary.mercenaryList != null && BaseDataLibrary.mercenaryList.Contains(mercenaryList[index].ID))
                {
                    itemList[i].GetChild(12).gameObject.SetActive(false);
                    itemList[i].GetChild(3).GetComponent<Text>().text = "已雇佣";
                }
                else
                {
                    List<object> list = new List<object>();
                    list.Add(mercenaryList[index].ID);
                    list.Add(itemList[i]);
                    itemList[i].GetChild(12).gameObject.SetActive(true);
                    itemList[i].GetChild(3).GetComponent<Text>().text = "雇佣";
                    EventTriggerListener.Get(itemList[i].GetChild(12).gameObject, list).onClick = BuyMercenary;
                }
                index++;
                if (index >= mercenaryList.Count)
                {
                    index = 0;
                }
            }
        }
    }


    void BuyMercenary(GameObject obj, object param)
    {
        List<object> list = (List<object>)param;
        MessageBox.Instance.PopYesNo("是否雇佣此佣兵", null, () =>
        {
            GameJsonDataHelper.AddMercenaryData((int)list[0]);
            ((Transform)list[1]).GetChild(12).gameObject.SetActive(false);
            ((Transform)list[1]).GetChild(3).GetComponent<Text>().text = "已雇佣";

        }, "取消", "确定");
    }
}

