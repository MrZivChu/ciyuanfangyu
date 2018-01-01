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
        item = Resources.Load("Mercenary/MercenaryBagItem");
        InitData();
    }

    void InitData()
    {
        List<int> idList = BaseDataLibrary.mercenaryList;
        if (idList != null && idList.Count > 0)
        {
            for (int i = 0; i < idList.Count; i++)
            {
                GameObject go = Instantiate(item) as GameObject;
                go.transform.parent = parent;
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
                Mercenary mer = MercenaryDataLibrary.MercenaryList.Find(it => it.ID == idList[i]);
                if (mer != null)
                {
                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(mer.iconPath.Replace("girl", ""));
                    go.transform.GetChild(2).GetComponent<Text>().text = mer.mercenaryName;
                    go.transform.GetChild(4).GetComponent<Text>().text = mer.talent;
                    go.transform.GetChild(5).GetComponent<Text>().text = mer.star.ToString();
                }
            }
        }
    }

    void BackClick(GameObject go, object data)
    {
        hudScript.ResetTweenPlay();
        Destroy(gameObject);
    }

}
