using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private string Name;
    private int enemyCount;
    private float spawnRate;

    public Wave()
    {
        Name = "";
        enemyCount = 0;
        spawnRate = 0;
    }
    public void setName(string tmp_)
    {
        Name = tmp_;
    }
    public void setEnemyCount( int tmp_)
    {
        enemyCount = tmp_;
    }
    public void setSpawnRate(float tmp_)
    {
        spawnRate = tmp_;
    }
    
    public string returnName()
    {
        return Name;
    }
    public int returnEnemyCount()
    {
        return enemyCount;
    }
    public float returnSpawnRate()
    {
        return spawnRate;
    }
}
