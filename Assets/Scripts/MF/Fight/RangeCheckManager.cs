using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JudgePosition
{
    none = 0,
    outter = 1,
    inner = 2,
    leftCoincidence = 3,//左重合
    rightCoincidence = 4,//右重合
}

public class RangeCheckManager : MonoBehaviour
{
    public List<GameObject> circleList;

    private void Awake()
    {
        if (rangList != null)
            rangList.Clear();
    }

    public static List<Range> rangList = new List<Range>();

    public void Check()
    {
        for (int i = 0; i < circleList.Count; i++)
        {
            if (circleList[i] != null)
            {
                circleList[i].SetActive(true);
            }
        }

        for (int y = 0; y < rangList.Count; y++)
        {
            Range range = rangList[y];
            if (range != null)
            {
                List<GameObject> leftObjList = range.leftObjList;
                List<GameObject> rightObjList = range.rightObjList;
                List<GameObject> topObjList = range.topObjList;

                leftObjList.ForEach((go) => { go.SetActive(true); });
                rightObjList.ForEach((go) => { go.SetActive(true); });
                topObjList.ForEach((go) => { go.SetActive(true); });
            }
        }

        for (int m = 0; m < rangList.Count; m++)
        {
            Range range = rangList[m];
            if (range != null)
            {
                HandleThisRangeToOtherRanges(range, m);
                HandleCircle(range.leftIndex, range.rightIndex);
            }
        }
    }

    void HandleThisRangeToOtherRanges(Range currentRange, int currentIndex = 0)
    {
        if (currentRange != null)
        {
            for (int y = 0; y < rangList.Count; y++)
            {
                Range compareRange1 = rangList[y];
                //print(currentRange.gameObject.transform.parent.name + "&" + currentIndex + " ******** " + compareRange1.gameObject.transform.parent.name + "&" + y);

                if (y > currentIndex)
                {
                    Range compareRange = rangList[y];
                    if (compareRange != null && compareRange != currentRange)
                    {
                        int compareLeftIndex = compareRange.leftIndex;
                        int compareRightIndex = compareRange.rightIndex;
                        List<GameObject> compareLeftObjList = compareRange.leftObjList;
                        List<GameObject> compareRightObjList = compareRange.rightObjList;
                        List<GameObject> compareTopObjList = compareRange.topObjList;

                        int currentLeftIndex = currentRange.leftIndex;
                        int currentRightIndex = currentRange.rightIndex;
                        List<GameObject> currentLeftObjList = currentRange.leftObjList;
                        List<GameObject> currentRightObjList = currentRange.rightObjList;
                        List<GameObject> currentTopObjList = currentRange.topObjList;

                        if (compareLeftIndex < 1 || compareRightIndex < 1 || currentLeftIndex < 1 || currentRightIndex < 1)
                            continue;

                        if (compareRightIndex > compareLeftIndex)
                        {
                            if (currentLeftIndex == compareLeftIndex && currentRightIndex == compareRightIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 1);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    compareTopObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 2);
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    currentTopObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 3);
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    currentTopObjList.ForEach((go) => { go.SetActive(false); });
                                }
                            }
                            else if (currentLeftIndex == compareRightIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 4);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareRightObjList.Count; i++)
                                    {
                                        if (i < currentLeftObjList.Count)
                                            currentLeftObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 5);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 6);
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentLeftObjList.Count; i++)
                                    {
                                        if (i < compareRightObjList.Count)
                                            compareRightObjList[i].SetActive(false);
                                    }
                                }
                            }
                            else if (currentRightIndex == compareLeftIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 7);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareLeftObjList.Count; i++)
                                    {
                                        if (i < currentRightObjList.Count)
                                            currentRightObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 8);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 9);
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentRightObjList.Count; i++)
                                    {
                                        if (i < compareLeftObjList.Count)
                                            compareLeftObjList[i].SetActive(false);
                                    }
                                }
                            }
                            else if (currentLeftIndex > compareLeftIndex && currentLeftIndex < compareRightIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 10);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareRightObjList.Count; i++)
                                    {
                                        if (i < currentLeftObjList.Count)
                                            currentLeftObjList[i].SetActive(false);
                                    }
                                    for (int i = compareTopObjList.Count - 1, j = 0; j < compareRightIndex - currentLeftIndex; i--, j++)
                                    {
                                        if (i < compareTopObjList.Count && i >= 0)
                                            compareTopObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 11);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0, j = 0; j < compareRightIndex - currentLeftIndex; i++, j++)
                                    {
                                        if (i < currentTopObjList.Count)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                    //for (int i = compareTopObjList.Count - 1, j = 0; j < compareRightIndex - currentLeftIndex; i--, j++)
                                    //{
                                    //    compareTopObjList[i].SetActive(false);
                                    //}
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 12);
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentLeftObjList.Count; i++)
                                    {
                                        if (i < compareRightObjList.Count)
                                            compareRightObjList[i].SetActive(false);
                                    }
                                    for (int i = 0, j = 0; j < compareRightIndex - currentLeftIndex; i++, j++)
                                    {
                                        if (i < currentTopObjList.Count)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                }
                            }
                            else if (currentRightIndex > compareLeftIndex && currentRightIndex < compareRightIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 13);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareLeftObjList.Count; i++)
                                    {
                                        if (i < currentRightObjList.Count)
                                            currentRightObjList[i].SetActive(false);
                                    }
                                    for (int i = 0, j = 0; j < currentRightIndex - compareLeftIndex; i++, j++)
                                    {
                                        if (i < compareTopObjList.Count)
                                            compareTopObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 14);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    //for (int i = 0, j = 0; j < currentRightIndex - compareLeftIndex; i++, j++)
                                    //{
                                    //    compareTopObjList[i].SetActive(false);
                                    //}
                                    for (int i = currentTopObjList.Count - 1, j = 0; j < currentRightIndex - compareLeftIndex; i--, j++)
                                    {
                                        if (i < currentTopObjList.Count && i >= 0)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 15);
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentRightObjList.Count; i++)
                                    {
                                        if (i < compareLeftObjList.Count)
                                            compareLeftObjList[i].SetActive(false);
                                    }
                                    for (int i = currentTopObjList.Count - 1, j = 0; j < currentRightIndex - compareLeftIndex; i--, j++)
                                    {
                                        if (i < currentTopObjList.Count && i >= 0)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                }
                            }
                        }
                        else if (compareRightIndex < compareLeftIndex)
                        {
                            if (currentLeftIndex == compareLeftIndex && currentRightIndex == compareRightIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 16);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    compareTopObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 17);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    compareTopObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 18);
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    currentTopObjList.ForEach((go) => { go.SetActive(false); });
                                }
                            }
                            else if (currentLeftIndex == compareRightIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 19);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareRightObjList.Count; i++)
                                    {
                                        if (i < currentLeftObjList.Count)
                                            currentLeftObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 20);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 21);
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentLeftObjList.Count; i++)
                                    {
                                        if (i < compareRightObjList.Count)
                                            compareRightObjList[i].SetActive(false);
                                    }
                                }
                            }
                            else if (currentRightIndex == compareLeftIndex)
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 22);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareLeftObjList.Count; i++)
                                    {
                                        if (i < currentRightObjList.Count)
                                            currentRightObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 23);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 24);
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentRightObjList.Count; i++)
                                    {
                                        if (i < compareLeftObjList.Count)
                                            compareLeftObjList[i].SetActive(false);
                                    }
                                }
                            }
                            else if ((currentLeftIndex > 0 && currentLeftIndex < compareRightIndex) || (currentLeftIndex > compareLeftIndex && currentLeftIndex <= 96))
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 25);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareRightObjList.Count; i++)
                                    {
                                        if (i < currentLeftObjList.Count)
                                            currentLeftObjList[i].SetActive(false);
                                    }

                                    int count = 0;
                                    if (currentLeftIndex > 0 && currentLeftIndex < compareRightIndex)
                                    {
                                        count = compareRightIndex - currentLeftIndex;
                                    }
                                    else if (currentLeftIndex > compareLeftIndex && currentLeftIndex <= 96)
                                    {
                                        count = (96 - currentLeftIndex) + compareRightIndex;
                                    }
                                    for (int i = compareTopObjList.Count - 1, j = 0; j < count; i--, j++)
                                    {
                                        if (i < compareTopObjList.Count && i >= 0)
                                            compareTopObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 26);
                                    compareRightObjList.ForEach((go) => { go.SetActive(false); });
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });

                                    int count = 0;
                                    if (currentLeftIndex > 0 && currentLeftIndex < compareRightIndex)
                                    {
                                        count = compareRightIndex - currentLeftIndex;
                                    }
                                    else if (currentLeftIndex > compareLeftIndex && currentLeftIndex <= 96)
                                    {
                                        count = (96 - currentLeftIndex) + compareRightIndex;
                                    }
                                    //for (int i = compareTopObjList.Count - 1, j = 0; j < count; i--, j++)
                                    //{
                                    //    compareTopObjList[i].SetActive(false);
                                    //}
                                    for (int i = 0, j = 0; j < count; i++, j++)
                                    {
                                        if (i < currentTopObjList.Count)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 27);
                                    currentLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentLeftObjList.Count; i++)
                                    {
                                        if (i < compareRightObjList.Count)
                                            compareRightObjList[i].SetActive(false);
                                    }

                                    int count = 0;
                                    if (currentLeftIndex > 0 && currentLeftIndex < compareRightIndex)
                                    {
                                        count = compareRightIndex - currentLeftIndex;
                                    }
                                    else if (currentLeftIndex > compareLeftIndex && currentLeftIndex <= 96)
                                    {
                                        count = (96 - currentLeftIndex) + compareRightIndex;
                                    }
                                    for (int i = 0, j = 0; j < count; i++, j++)
                                    {
                                        if (i < currentTopObjList.Count)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                }
                            }
                            else if ((currentRightIndex > compareLeftIndex && currentRightIndex <= 96) || (currentRightIndex > 0 && currentRightIndex < compareRightIndex))
                            {
                                if (currentRange.level > compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 28);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < compareLeftObjList.Count; i++)
                                    {
                                        if (i < currentRightObjList.Count)
                                            currentRightObjList[i].SetActive(false);
                                    }

                                    int count = 0;
                                    if (currentRightIndex > compareLeftIndex && currentRightIndex <= 96)
                                    {
                                        count = currentRightIndex - compareLeftIndex;
                                    }
                                    else if (currentRightIndex > 0 && currentRightIndex < compareRightIndex)
                                    {
                                        count = currentRightIndex + (96 - compareLeftIndex);
                                    }
                                    for (int i = 0, j = 0; j < count; i++, j++)
                                    {
                                        if (i < compareTopObjList.Count)
                                            compareTopObjList[i].SetActive(false);
                                    }
                                }
                                else if (currentRange.level == compareRange.level)
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 29);
                                    compareLeftObjList.ForEach((go) => { go.SetActive(false); });
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });

                                    int count = 0;
                                    if (currentRightIndex > compareLeftIndex && currentRightIndex <= 96)
                                    {
                                        count = currentRightIndex - compareLeftIndex;
                                    }
                                    else if (currentRightIndex > 0 && currentRightIndex < compareRightIndex)
                                    {
                                        count = currentRightIndex + (96 - compareLeftIndex);
                                    }
                                    //for (int i = 0, j = 0; j < count; i++, j++)
                                    //{
                                    //    compareTopObjList[i].SetActive(false);
                                    //}
                                    for (int i = currentTopObjList.Count - 1, j = 0; j < count; i--, j++)
                                    {
                                        if (i < currentTopObjList.Count && i >= 0)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                }
                                else
                                {
                                    //print(currentRange.gameObject.transform.parent.name + " = " + compareRange.gameObject.transform.parent.name + 30);
                                    currentRightObjList.ForEach((go) => { go.SetActive(false); });
                                    for (int i = 0; i < currentRightObjList.Count; i++)
                                    {
                                        if (i < compareLeftObjList.Count)
                                            compareLeftObjList[i].SetActive(false);
                                    }

                                    int count = 0;
                                    if (currentRightIndex > compareLeftIndex && currentRightIndex <= 96)
                                    {
                                        count = currentRightIndex - compareLeftIndex;
                                    }
                                    else if (currentRightIndex > 0 && currentRightIndex < compareRightIndex)
                                    {
                                        count = currentRightIndex + (96 - compareLeftIndex);
                                    }
                                    for (int i = currentTopObjList.Count - 1, j = 0; j < count; i--, j++)
                                    {
                                        if (i < currentTopObjList.Count && i >= 0)
                                            currentTopObjList[i].SetActive(false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void HandleCircle(int leftIndex, int rightIndex)
    {
        if (leftIndex != 0 && rightIndex != 0)
        {
            if (leftIndex < rightIndex)
            {
                for (int i = leftIndex; i < rightIndex; i++)
                {
                    int index = i - 1;
                    if (circleList[index] != null)
                        circleList[index].SetActive(false);
                }
            }
            else
            {
                for (int i = 1; i < rightIndex; i++)
                {
                    int index = i - 1;
                    if (circleList[index] != null)
                        circleList[index].SetActive(false);
                }
                for (int j = leftIndex; j <= 96; j++)
                {
                    int index = j - 1;
                    if (circleList[index] != null)
                        circleList[index].SetActive(false);
                }
            }
        }
    }

}
