using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataLibrary : MonoBehaviour
{
    public int coins;
    public int diamond;
    public int people;
    public double happinessDegree;

    //操作员的专注度（当专注度很高的时候，操作员能够轻易使用它们的特别技能，没完成一次出击任务，专注度就会提升，如果操作员没有参与任务的话，专注度就会下降，如果能量为0，专注度会完全丧失）
    public int focus;
    //能量
    public int engry; 
}
