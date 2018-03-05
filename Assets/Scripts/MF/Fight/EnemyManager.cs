using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    InfantryEnemy = 1 //步兵
}

public class EachWaveConfig
{
    //几秒后开始生成此波
    public float startTime;
    //生成点
    public Transform point;
    //生成的敌人类型
    public List<EnemyParent> enemyTypelist;
}


public class EnemyManager : MonoBehaviour
{
    //敌人出生点
    public List<Transform> enemySpawnPointList = new List<Transform>();

    //出生点对应生成的敌人集合
    public static Dictionary<Transform, List<GameObject>> dic = new Dictionary<Transform, List<GameObject>>();

    public List<EachWaveConfig> eachWaveConfigList = new List<EachWaveConfig>();

    public List<SpawnTask> spawnTaskList = new List<SpawnTask>();
    void Start()
    {
        dic.Clear();

        if (enemySpawnPointList != null && enemySpawnPointList.Count > 0)
        {
            EachWaveConfig ewc = new EachWaveConfig();
            ewc.startTime = 2;
            Transform pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>() {
                new InfantryEnemy() { blood = 100, maxAttackDistance = 35, walkSpeed = 2, attackRepeatRateTime = 2 }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 5;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>() {
                new InfantryEnemy() { blood = 100, maxAttackDistance = 31,  walkSpeed = 5, attackRepeatRateTime = 2 }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 12;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 100, maxAttackDistance = 31,  walkSpeed = 10, attackRepeatRateTime = 2 }
            };
            eachWaveConfigList.Add(ewc);
        }

        if (eachWaveConfigList != null && eachWaveConfigList.Count > 0)
        {
            SpawnTask st = null;
            foreach (var item in eachWaveConfigList)
            {
                st = new SpawnTask();
                st.startTime = item.startTime;
                st.param = item;
                st.onCallBack = Callback;
                spawnTaskList.Add(st);
            }
        }
    }

    void HandleDic(Transform pos, GameObject enemy)
    {
        if (dic.ContainsKey(pos))
        {
            if (!dic[pos].Contains(enemy))
            {
                dic[pos].Add(enemy);
            }
        }
        else
        {
            dic[pos] = new List<GameObject>() { enemy };
        }
    }

    void Callback(object param)
    {
        EachWaveConfig ewc = (EachWaveConfig)param;
        GameObject go = null;
        foreach (var item in ewc.enemyTypelist)
        {
            string resourceName = string.Empty;
            Type type;
            if (item is InfantryEnemy)
            {
                type = typeof(InfantryEnemy);
                resourceName = type.ToString();
                go = Instantiate(Resources.Load(resourceName)) as GameObject;
                InfantryEnemy infantryEnemy = go.AddComponent<InfantryEnemy>();
                infantryEnemy.blood = item.blood;
                infantryEnemy.maxAttackDistance = item.maxAttackDistance;
                infantryEnemy.walkSpeed = item.walkSpeed;
                infantryEnemy.attackRepeatRateTime = item.attackRepeatRateTime;
                go.transform.position = ewc.point.position;

                HandleDic(ewc.point, go);
            }
        }
    }

    void Update()
    {
        if (spawnTaskList != null && spawnTaskList.Count > 0)
        {
            SpawnTask st = null;
            for (int i = 0; i < spawnTaskList.Count; i++)
            {
                st = spawnTaskList[i];
                if (st.isStop)
                {
                    spawnTaskList.Remove(st);
                }
                else
                {
                    st.Update(Time.deltaTime);
                }
            }
        }
    }
}

public class SpawnTask
{
    public float startTime = 0;
    public object param;
    public Action<object> onCallBack;
    public bool isStop = false;

    float tempTime = 0;
    public void Update(float deltaTime)
    {
        if (!isStop)
        {
            tempTime += Time.deltaTime;
            if (tempTime >= startTime)
            {
                if (onCallBack != null)
                {
                    isStop = true;
                    onCallBack(param);
                }
            }
        }
    }
}
