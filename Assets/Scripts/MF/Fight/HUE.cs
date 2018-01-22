using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUE : MonoBehaviour
{   
    public GameObject result;
    public Button okBtn;

    public List<GameObject> mercenaryGameobjectList;

    private void Start()
    {
        EventTriggerListener.Get(okBtn.gameObject).onClick = FightOverClick;

        InitMercenary();
    }

    void FightOverClick(GameObject go, object data)
    {
        result.SetActive(true);
    }

    void InitMercenary()
    {
        if (BaseDataLibrary.battleMercenaryList != null && BaseDataLibrary.battleMercenaryList.Count > 0)
        {
            for (int i = 0; i < BaseDataLibrary.battleMercenaryList.Count; i++)
            {
                Mercenary mer = MercenaryDataConfigTable.MercenaryList.Find(it => it.ID == BaseDataLibrary.battleMercenaryList[i]);
                if (mer != null)
                {
                    GameObject go = mercenaryGameobjectList[i];
                    Sprite sprite = Resources.Load<Sprite>(mer.iconPath.Replace("zm", "zd"));
                    go.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                    go.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "LV" + mer.star;
                    go.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = mer.mercenaryName;
                    go.SetActive(true);
                }
            }
        }

        int count = BaseDataLibrary.battleMercenaryList == null ? mercenaryGameobjectList.Count : BaseDataLibrary.battleMercenaryList.Count;
        for (int i = count; i < mercenaryGameobjectList.Count; i++)
        {
            mercenaryGameobjectList[i].SetActive(false);
        }
    }

}


