using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataLibrary
{
    public static int coins;
    public static int diamond;
    public static int people;
    public static double happinessDegree;

    //操作员的专注度（当专注度很高的时候，操作员能够轻易使用它们的特别技能，没完成一次出击任务，专注度就会提升，如果操作员没有参与任务的话，专注度就会下降，如果能量为0，专注度会完全丧失）
    public static int focus;
    //能量
    public static int engry;

    //拥有的佣兵集合
    public static List<int> mercenaryList;

    //佣兵币
    public static int mercenaryMoney;

    //背景音乐的声音大小
    public static double musicVolume;
    //选择哪首歌
    public static int musicIndex;
}
