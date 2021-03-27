using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControler : MonoBehaviour
{
    protected List<Vector3> enemiesSpawnPoints = new List<Vector3>();

    protected int enemiesCountToSpawn;
    protected float lastEnemySpawnTime;
    [SerializeField]
    protected float enemySpawningCooldown = 2;

    public enum levelPhase { preparations, defence }
    protected levelPhase currentPhase;
    protected int currentEnemiesWave;
    public levelPhase GetCurrentPhase { get { return currentPhase; } }
    public int GetCurrentWave { get { return currentEnemiesWave; } }

    [SerializeField]
    protected List<GameObject> enemiesPrefabs = new List<GameObject>();

    [SerializeField]
    protected Transform enemiesParentObject;

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = levelPhase.preparations;
        currentEnemiesWave = 1;

        enemiesSpawnPoints.Clear();
        enemiesSpawnPoints.Add(new Vector3(3, 1, 11));
        enemiesSpawnPoints.Add(new Vector3(3, 1, 7));

        enemiesCountToSpawn = 0;
        lastEnemySpawnTime = -enemySpawningCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPhase)
        {
            case levelPhase.preparations:
                // TODO: Tutaj trzeba dodać jakieś czekanie aż gracz kliknie przycisk by rozpocząć kolejną fazę
                StartNewWave();
                break;

            case levelPhase.defence:
                if (enemiesCountToSpawn > 0)
                {
                    if (Time.time - lastEnemySpawnTime > enemySpawningCooldown) SpawnRandomEnemy();
                }
                else if (GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
                {
                    EndCurrentWave();
                }
                break;
        }
    }

    /// <summary>
    /// Inicjalizuje nową falę przeciwników i rozpoczyna fazę obrony
    /// </summary>
    protected void StartNewWave()
    {
        Debug.Log($"Starting wave {currentEnemiesWave}");

        enemiesCountToSpawn = currentEnemiesWave * 2;

        currentPhase = levelPhase.defence;
    }

    /// <summary>
    /// Zakańcza obecną falę przeciwników i rozpoczyna fazę przygotowań
    /// </summary>
    protected void EndCurrentWave()
    {
        Debug.Log($"Ending wave {currentEnemiesWave}");

        currentEnemiesWave++;

        currentPhase = levelPhase.preparations;
    }

    /// <summary>
    /// Spawnuje losowego przeciwnika
    /// </summary>
    protected void SpawnRandomEnemy()
    {
        int randomPrefabIndex = Random.Range(0, enemiesPrefabs.Count);
        int randomSpawnPointIndex = Random.Range(0, enemiesSpawnPoints.Count);
        GameObject newEnemy = Instantiate(enemiesPrefabs[randomPrefabIndex], enemiesSpawnPoints[randomSpawnPointIndex], Quaternion.identity, enemiesParentObject);

        newEnemy.GetComponent<Enemy>().InitStats(40 + currentEnemiesWave * 10, 4 + currentEnemiesWave);

        enemiesCountToSpawn--;
        lastEnemySpawnTime = Time.time;
    }
}
