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

public class CircleCheckManager : MonoBehaviour
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

        foreach (Range range in rangList)
        {
            List<GameObject> leftObjList = range.leftObjList;
            List<GameObject> rightObjList = range.rightObjList;
            List<GameObject> topObjList = range.topObjList;

            leftObjList.ForEach((go) => { go.SetActive(true); });
            rightObjList.ForEach((go) => { go.SetActive(true); });
            topObjList.ForEach((go) => { go.SetActive(true); });

            HandleThisRangeToOtherRanges(range);

            HandleCircle(range.leftIndex, range.rightIndex);
        }
    }

    void HandleThisRangeToOtherRanges(Range currentRange)
    {
        foreach (Range compareRange in rangList)
        {
            if (compareRange != currentRange)
            {
                int compareLeftIndex = compareRange.leftIndex;
                int compareRightIndex = compareRange.rightIndex;

                int currentLeftIndex = currentRange.leftIndex;
                int currentRightIndex = currentRange.rightIndex;

                if (compareRightIndex > compareLeftIndex)
                {
                    if (currentLeftIndex == compareLeftIndex && currentRightIndex == compareRightIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.topObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.topObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else
                        {
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.topObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        break;
                    }
                    else if (currentLeftIndex == compareRightIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.rightObjList.Count; i++)
                            {
                                currentRange.leftObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else
                        {
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.leftObjList.Count; i++)
                            {
                                compareRange.rightObjList[i].SetActive(false);
                            }
                        }
                        break;
                    }
                    else if (currentRightIndex == compareLeftIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.leftObjList.Count; i++)
                            {
                                currentRange.rightObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else
                        {
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.rightObjList.Count; i++)
                            {
                                compareRange.leftObjList[i].SetActive(false);
                            }
                        }
                        break;
                    }
                    else if (currentLeftIndex > compareLeftIndex && currentLeftIndex < compareRightIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.rightObjList.Count; i++)
                            {
                                currentRange.leftObjList[i].SetActive(false);
                            }
                            for (int i = compareRange.topObjList.Count - 1, j = 0; j < compareRightIndex - currentLeftIndex; i--, j++)
                            {
                                compareRange.topObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0, j = 0; j < compareRightIndex - currentLeftIndex; i++, j++)
                            {
                                currentRange.topObjList[i].SetActive(false);
                            }
                            for (int i = compareRange.topObjList.Count - 1, j = 0; j < compareRightIndex - currentLeftIndex; i--, j++)
                            {
                                compareRange.topObjList[i].SetActive(false);
                            }
                        }
                        else
                        {
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.leftObjList.Count; i++)
                            {
                                compareRange.rightObjList[i].SetActive(false);
                            }
                            for (int i = 0, j = 0; j < compareRightIndex - currentLeftIndex; i++, j++)
                            {
                                currentRange.topObjList[i].SetActive(false);
                            }
                        }
                        break;
                    }
                    else if (currentRightIndex > compareLeftIndex && currentRightIndex < compareRightIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.leftObjList.Count; i++)
                            {
                                currentRange.rightObjList[i].SetActive(false);
                            }
                            for (int i = 0, j = 0; j < currentRightIndex - compareLeftIndex; i++, j++)
                            {
                                compareRange.topObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0, j = 0; j < currentRightIndex - compareLeftIndex; i++, j++)
                            {
                                compareRange.topObjList[i].SetActive(false);
                            }
                            for (int i = currentRange.topObjList.Count - 1, j = 0; j < currentRightIndex - compareLeftIndex; i--, j++)
                            {
                                currentRange.topObjList[i].SetActive(false);
                            }
                        }
                        else
                        {
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.rightObjList.Count; i++)
                            {
                                compareRange.leftObjList[i].SetActive(false);
                            }
                            for (int i = currentRange.topObjList.Count - 1, j = 0; j < currentRightIndex - compareLeftIndex; i--, j++)
                            {
                                currentRange.topObjList[i].SetActive(false);
                            }
                        }
                        break;
                    }
                }
                else if (compareRightIndex < compareLeftIndex)
                {
                    if (currentLeftIndex == compareLeftIndex && currentRightIndex == compareRightIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.topObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            compareRange.topObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else
                        {
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.topObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        break;
                    }
                    else if (currentLeftIndex == compareRightIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.rightObjList.Count; i++)
                            {
                                currentRange.leftObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else
                        {
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.leftObjList.Count; i++)
                            {
                                compareRange.rightObjList[i].SetActive(false);
                            }
                        }
                        break;
                    }
                    else if (currentRightIndex == compareLeftIndex)
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.leftObjList.Count; i++)
                            {
                                currentRange.rightObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                        }
                        else
                        {
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.rightObjList.Count; i++)
                            {
                                compareRange.leftObjList[i].SetActive(false);
                            }
                        }
                        break;
                    }
                    else if ((currentLeftIndex > 0 && currentLeftIndex < compareRightIndex) || (currentLeftIndex > compareLeftIndex && currentLeftIndex <= 96))
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.rightObjList.Count; i++)
                            {
                                currentRange.leftObjList[i].SetActive(false);
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
                            for (int i = compareRange.topObjList.Count - 1, j = 0; j < count; i--, j++)
                            {
                                compareRange.topObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });

                            int count = 0;
                            if (currentLeftIndex > 0 && currentLeftIndex < compareRightIndex)
                            {
                                count = compareRightIndex - currentLeftIndex;
                            }
                            else if (currentLeftIndex > compareLeftIndex && currentLeftIndex <= 96)
                            {
                                count = (96 - currentLeftIndex) + compareRightIndex;
                            }
                            for (int i = compareRange.topObjList.Count - 1, j = 0; j < count; i--, j++)
                            {
                                compareRange.topObjList[i].SetActive(false);
                            }
                            for (int i = 0, j = 0; j < count; i++, j++)
                            {
                                currentRange.topObjList[i].SetActive(false);
                            }
                        }
                        else
                        {
                            currentRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.leftObjList.Count; i++)
                            {
                                compareRange.rightObjList[i].SetActive(false);
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
                                currentRange.topObjList[i].SetActive(false);
                            }
                        }
                        break;
                    }
                    else if ((currentRightIndex > compareLeftIndex && currentRightIndex <= 96) || (currentRightIndex > 0 && currentRightIndex < compareRightIndex))
                    {
                        if (currentRange.level > compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < compareRange.leftObjList.Count; i++)
                            {
                                currentRange.rightObjList[i].SetActive(false);
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
                                compareRange.topObjList[i].SetActive(false);
                            }
                        }
                        else if (currentRange.level == compareRange.level)
                        {
                            compareRange.leftObjList.ForEach((go) => { go.SetActive(false); });
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });

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
                                compareRange.topObjList[i].SetActive(false);
                            }
                            for (int i = currentRange.topObjList.Count - 1, j = 0; j < count; i--, j++)
                            {
                                currentRange.topObjList[i].SetActive(false);
                            }
                        }
                        else
                        {
                            currentRange.rightObjList.ForEach((go) => { go.SetActive(false); });
                            for (int i = 0; i < currentRange.rightObjList.Count; i++)
                            {
                                compareRange.leftObjList[i].SetActive(false);
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
                            for (int i = currentRange.topObjList.Count - 1, j = 0; j < count; i--, j++)
                            {
                                currentRange.topObjList[i].SetActive(false);
                            }
                        }
                        break;
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
