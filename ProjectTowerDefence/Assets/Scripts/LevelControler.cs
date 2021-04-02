using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControler : MonoBehaviour
{
    #region Deklaracje zmiennych
    /// <summary>
    /// Lista pozycji, w których mają się spawnować przeciwnicy. Powinny być one ustawiane w edytorze Unity!
    /// </summary>
    [SerializeField]
    protected List<Vector3> enemiesSpawnPoints = new List<Vector3>();

    /// <summary>
    /// Liczba przeciwników pozostałych do zespawnowania podczas obecnej fali
    /// </summary>
    protected int enemiesCountToSpawn;

    /// <summary>
    /// Czas ostatniego zespawnowania przeciwnika
    /// </summary>
    protected float lastEnemySpawnTime;

    /// <summary>
    /// Odstęp czasowy pomiędzy spawnowaniem kolejnych przeciwników. Można ustawiać w edytorze Unity.
    /// </summary>
    [SerializeField]
    [Range(0, 30)]
    protected float enemySpawningCooldown = 2;

    /// <summary>
    /// Typ wyliczeniowy faz gry
    /// </summary>
    public enum levelPhase { preparations, defence }

    /// <summary>
    /// Obecna faza gry
    /// </summary>
    protected levelPhase currentPhase;

    /// <summary>
    /// Numer obecnej fali przeciwników
    /// </summary>
    protected int currentEnemiesWave;

    /// <summary>
    /// Obecna faza gry
    /// </summary>
    public levelPhase GetCurrentPhase { get { return currentPhase; } }

    /// <summary>
    /// Numer obecnej fali przeciwników
    /// </summary>
    public int GetCurrentWave { get { return currentEnemiesWave; } }

    /// <summary>
    /// Lista wszystkich dostępnych prefabów przeciwników. Uwaga! Trzeba ją ręcznie aktualizować w edytorze Unity!
    /// </summary>
    [SerializeField]
    protected List<GameObject> enemiesPrefabs = new List<GameObject>();

    /// <summary>
    /// Obiekt nadrzędny, pod którym mają być tworzone obiekty przeciwników
    /// </summary>
    [SerializeField]
    protected Transform enemiesParentObject;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    // Update is called once per frame
    void Update()
    {
        LevelPhaseActions();
    }

    /// <summary>
    /// Początkowa inicjalizacja zmiennych
    /// </summary>
    private void InitVariables()
    {
        currentPhase = levelPhase.preparations;
        currentEnemiesWave = 1;

        enemiesCountToSpawn = 0;
        lastEnemySpawnTime = -enemySpawningCooldown;
    }

    /// <summary>
    /// Obsługa zdarzeń związanych z fazą gry
    /// </summary>
    private void LevelPhaseActions()
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
