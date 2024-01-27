using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup>  enemyGroups;
        public int waveQuota; //敌人波次
        public float spawnInterval; //时间间隔
        public int spawnCount; //敌人数量
    }
[System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }
    public List<Wave> waves;
    public int currentWaveCount;
    
    [Header("SpawnAttributes")]
    float spawnTime;
    public int enemiesAlive; // 当前数量
    public int MaxEnemiesAlive; //最大数量
    public bool maxEnemiesReached = false;  //是否达到最大数量
    public float waveInterval;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPositions;

    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();

    }

    // Update is called once per frame
    void Update()
    {

        if (currentWaveCount < waves.Count &&waves[currentWaveCount].spawnCount <= 0)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTime += Time.deltaTime;

        //生成敌人
        if (spawnTime <= waves[currentWaveCount].spawnInterval)
        {
            spawnTime = 0f;
            SpawnEneies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var wave in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += wave.enemyCount;
        }
         waves[currentWaveCount].waveQuota = currentWaveQuota;
         Debug.LogWarning("当前波次数量：" + currentWaveQuota);
    }
    
    /// <summary>
    /// 地图上的敌人到达最大数量将停止生成敌人
    /// </summary> <summary>
    /// 这个方法会在特定的波次中参生敌人,知道下一波敌人到来
    /// 
    /// </summary>
    void SpawnEneies()
    {
        //检查波中是否有最小数量的敌人被生成
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota &&!maxEnemiesReached)
        {
            //刷出各种类型的敌人，直到配额被填满
            foreach (var wave in waves[currentWaveCount].enemyGroups)
            {


                //检查这种类型的敌人的最小数量是否被生成
                if (wave.spawnCount< wave.enemyCount)
                {
                    if (enemiesAlive >= MaxEnemiesAlive )
                    {
                        maxEnemiesReached = true;
                        return;

                    }

                    Instantiate(wave.enemyPrefab,player.transform.position + relativeSpawnPositions[Random.Range(0,relativeSpawnPositions.Count)].position, Quaternion.identity);
                    // Vector2 spwaPosition = new Vector2(player.transform.position.x+ Random.Range(-10f, 10f), player.transform.position.y+ Random.Range(-10f, 10f));
                    // Instantiate(wave.enemyPrefab, spwaPosition, Quaternion.identity);
                    wave.spawnCount ++;
                    waves[currentWaveCount].spawnCount ++;

                    enemiesAlive++;
                }
            }
        }


        //重置最大数量
        if (enemiesAlive<MaxEnemiesAlive)
        {
            maxEnemiesReached = false;
        }
    }
        //怪物被杀死时调用
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
