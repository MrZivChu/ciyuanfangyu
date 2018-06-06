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
    public RecoverBatteryDataBase recoverBatteryDataBase;
    //敌人出生点
    public List<Transform> enemySpawnPointList = new List<Transform>();
    //方向警告集合
    public List<Transform> directionWarningList = new List<Transform>();
    //敌人出生点对应的方向警告
    Dictionary<Transform, Transform> sPointWithdwarningDic = new Dictionary<Transform, Transform>();

    //出生点对应生成的敌人集合
    public static Dictionary<Transform, List<GameObject>> dic = new Dictionary<Transform, List<GameObject>>();

    public List<EachWaveConfig> eachWaveConfigList = new List<EachWaveConfig>();

    public List<SpawnTask> spawnTaskList = new List<SpawnTask>();
    int allEnemyCount = 0;

    void Awake()
    {
        dic.Clear();
        for (int i = 0; i < enemySpawnPointList.Count; i++)
        {
            dic.Add(enemySpawnPointList[i], new List<GameObject>());
            sPointWithdwarningDic.Add(enemySpawnPointList[i], directionWarningList[i]);
        }
    }

    void Start()
    {
        if (enemySpawnPointList != null && enemySpawnPointList.Count > 0)
        {
            EachWaveConfig ewc = new EachWaveConfig();
            ewc.startTime = 2;
            Transform pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>() {
                new InfantryEnemy() { blood = 1300, maxAttackDistance = 3, walkSpeed = 6, attackRepeatRateTime = 4,model ="InfantryEnemy", attackValue = 60 ,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 5;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>() {
                new InfantryEnemy() { blood = 1300, maxAttackDistance = 3,  walkSpeed = 5, attackRepeatRateTime = 5 ,model="InfantryEnemy" ,attackValue = 50,recoverBatteryDataBase =recoverBatteryDataBase}
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 12;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1250, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 70,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 1;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1200, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 65,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);


            ewc = new EachWaveConfig();
            ewc.startTime = 12;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1300, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 35,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 6;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1280, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 30,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 5;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1320, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 55,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 8;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 2300, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="BossEnemy" ,attackValue = 60,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 10;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 2000, maxAttackDistance = 3,  walkSpeed = 4, attackRepeatRateTime = 3,model="BossEnemy" ,attackValue = 100,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);


            ewc = new EachWaveConfig();
            ewc.startTime = 20;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>() {
                new InfantryEnemy() { blood = 1300, maxAttackDistance = 3, walkSpeed = 6, attackRepeatRateTime = 4,model ="InfantryEnemy", attackValue = 60 ,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 22;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>() {
                new InfantryEnemy() { blood = 1300, maxAttackDistance = 3,  walkSpeed = 5, attackRepeatRateTime = 5 ,model="InfantryEnemy" ,attackValue = 50,recoverBatteryDataBase =recoverBatteryDataBase}
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 39;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1250, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 70,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 25;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1200, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 65,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 35;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1300, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 35,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 40;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1280, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 30,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 24;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 1320, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="InfantryEnemy" ,attackValue = 55,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 12;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 2300, maxAttackDistance = 3,  walkSpeed = 7, attackRepeatRateTime = 3,model="BossEnemy" ,attackValue = 60,recoverBatteryDataBase =recoverBatteryDataBase }
            };
            eachWaveConfigList.Add(ewc);

            ewc = new EachWaveConfig();
            ewc.startTime = 42;
            pos = enemySpawnPointList[UnityEngine.Random.Range(0, enemySpawnPointList.Count - 1)];
            ewc.point = pos;
            ewc.enemyTypelist = new List<EnemyParent>(){
                new InfantryEnemy() { blood = 2000, maxAttackDistance = 3,  walkSpeed = 4, attackRepeatRateTime = 3,model="BossEnemy" ,attackValue = 100,recoverBatteryDataBase =recoverBatteryDataBase }
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

                allEnemyCount += item.enemyTypelist.Count;
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

    List<GameObject> hasSpawnEnemy = new List<GameObject>();
    void Callback(object param)
    {
        EachWaveConfig ewc = (EachWaveConfig)param;
        GameObject go = null;
        foreach (var item in ewc.enemyTypelist)
        {
            string resourceName = string.Empty;
            go = Instantiate(Resources.Load(item.model)) as GameObject;
            EnemyParent enemyParent = go.GetComponent<EnemyParent>();
            enemyParent.blood = item.blood;
            enemyParent.maxAttackDistance = item.maxAttackDistance;
            enemyParent.walkSpeed = item.walkSpeed;
            enemyParent.attackRepeatRateTime = item.attackRepeatRateTime;
            enemyParent.attackValue = item.attackValue;
            enemyParent.recoverBatteryDataBase = item.recoverBatteryDataBase;

            go.transform.position = ewc.point.position;

            HandleDic(ewc.point, go);

            hasSpawnEnemy.Add(go);
        }
        if (sPointWithdwarningDic.ContainsKey(ewc.point))
        {
            Transform tt = sPointWithdwarningDic[ewc.point];
            DirectionWarning dw = tt.GetComponent<DirectionWarning>();
            if (dw != null)
            {
                dw.PlayAnim();
            }
        }
    }

    public GameObject result;
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
        if (allEnemyCount != 0)
        {
            if (allEnemyCount == hasSpawnEnemy.Count)
            {
                bool isAllOver = true;
                foreach (var item in hasSpawnEnemy)
                {
                    if (item != null)
                    {
                        EnemyParent enemy = item.GetComponent<EnemyParent>();
                        if (enemy != null)
                        {
                            if (enemy.blood > 0)
                            {
                                isAllOver = false;
                                break;
                            }
                        }
                    }
                }
                if (isAllOver)
                {
                    result.SetActive(true);
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
