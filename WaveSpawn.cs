using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    public Transform Enemy_1;
    public Transform Enemy_2;
    public Transform[] spawnPoints;

    private int nextWave = 0;
    private float timeBetweenWaves = 10f;
    private float waveCountDown;
    private SpawnState State = SpawnState.COUNTING;
    private float searchCountDown = 1f;

    public Wave[] Waves;

    void Start()
    {
       SetWaves();
       waveCountDown = timeBetweenWaves;
    }
    void SetWaves()
    {
        Waves = new Wave[Random.Range(1, 13)];

        for (int i = 0; i< Waves.Length; i++)
        {
            Waves[i] = new Wave();
            string Wave_Number = "Wave : " + i + 1;
            Waves[i].setName(Wave_Number);
            Waves[i].setEnemyCount(Random.Range(6, 21));
            Waves[i].setSpawnRate(Random.Range(1, 5));
        }
    }
    void Update()
    {

        if (State == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                //Begin a new round 
                WaveCompleted();
              
                return; 
            }
            else
            {
                return; 
            }
        }

        if (waveCountDown <= 0)
        {
            if (State != SpawnState.SPAWNING)
            {
                ///start Spawning wave
                
                StartCoroutine(SpawnWave((Waves[nextWave])));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        State = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > Waves.Length -1)
        {
            Debug.Log("All Waves are done...");
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
       
    }
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave wave_)
    {
        
        State = SpawnState.SPAWNING;
        Debug.Log("Spawning wave!");
        //Spawn
        for (int i = 0; i < wave_.returnEnemyCount(); i++)
        {
            int choosenEnemy =  Random.Range(1, 3);
            Debug.Log("choosenEnemy" +  choosenEnemy);
            if(choosenEnemy == 1)
            {
                SpawnEnemy(Enemy_1);
            }
            else if (choosenEnemy == 2)
            {
                SpawnEnemy(Enemy_2);
            }
            yield return new WaitForSeconds (1f / wave_.returnSpawnRate());
        }
        State = SpawnState.WAITING;
        yield break;
    }
    void SpawnEnemy(Transform enemy_)
    {
        //Spawn Enemy 
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy_, sp.position, sp.rotation);
    }
}
