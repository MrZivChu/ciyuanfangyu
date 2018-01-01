using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverMercenaryData : MonoBehaviour
{
    void Awake()
    {
        BaseDataLibrary.mercenaryList = GameJsonDataHelper.ReadMercenaryData();
    }
}
