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
    /// Lista przeciwników pozostałych do zespawnowania podczas obecnej fali
    /// </summary>
    protected List<EnemiesWave> enemiesToSpawn = new List<EnemiesWave>();

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

    /// <summary>
    /// Lista zestawów fal przeciwników dla różnych poziomów
    /// </summary>
    protected List<List<List<EnemiesWave>>> allEnemiesWaves = new List<List<List<EnemiesWave>>>();

    /// <summary>
    /// Index zestawu fal przeciwników, który mas być użyty
    /// </summary>
    [SerializeField]
    protected int enemiesWavesSetId;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        DefineEnemiesWaves();
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
        currentEnemiesWave = 0;

        enemiesToSpawn.Clear();
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
                if (enemiesToSpawn.Count > 0)
                {
                    if (Time.time - lastEnemySpawnTime > enemySpawningCooldown) SpawnNextEnemy();
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

        if (enemiesWavesSetId < 0 || enemiesWavesSetId >= allEnemiesWaves.Count)
        {
            Debug.LogError("Nieprawidłowy numer zestawu fal przeciwników");
            enemiesWavesSetId = 0;
        }

        int definiedWavesCount = allEnemiesWaves[enemiesWavesSetId].Count;
        int statsMultiplier = currentEnemiesWave / definiedWavesCount + 1;

        enemiesToSpawn.Clear();
        foreach (EnemiesWave ew in allEnemiesWaves[enemiesWavesSetId][currentEnemiesWave % definiedWavesCount])
        {
            if (ew.count > 0)
            {
                for (int i = 0; i < ew.count; i++)
                {
                    enemiesToSpawn.Add(new EnemiesWave(ew.prefabId, ew.hp * statsMultiplier, ew.speed * statsMultiplier));
                }
            }
        }

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
    /// Spawnuje następnego przeciwnika
    /// </summary>
    protected void SpawnNextEnemy()
    {
        if (enemiesToSpawn.Count > 0)
        {
            EnemiesWave enemyToSpawn = enemiesToSpawn[0];

            int randomSpawnPointIndex = Random.Range(0, enemiesSpawnPoints.Count);
            int prefabId = enemyToSpawn.prefabId;
            if (prefabId < 0 || prefabId >= enemiesPrefabs.Count)
            {
                Debug.LogError("Nieprawidłowy numer prefabu przeciwnika");
                prefabId = 0;
            }
            GameObject newEnemy = Instantiate(enemiesPrefabs[prefabId], enemiesSpawnPoints[randomSpawnPointIndex], Quaternion.identity, enemiesParentObject);

            newEnemy.GetComponent<Enemy>().InitStats(enemyToSpawn.hp, enemyToSpawn.speed);

            enemiesToSpawn.RemoveAt(0);
            lastEnemySpawnTime = Time.time;
        }
    }

    /// <summary>
    /// Utworzenie listy wszystkich fal przeciwników
    /// </summary>
    private void DefineEnemiesWaves()
    {
        allEnemiesWaves.Clear();

        // zestaw 0
        allEnemiesWaves.Add(new List<List<EnemiesWave>>());
        // fala 0
        allEnemiesWaves[0].Add(new List<EnemiesWave>());
        allEnemiesWaves[0][0].Add(new EnemiesWave(0, 50, 1, 2));
        // fala 1
        allEnemiesWaves[0].Add(new List<EnemiesWave>());
        allEnemiesWaves[0][1].Add(new EnemiesWave(0, 60, 1.25f, 3));
        // fala 2
        allEnemiesWaves[0].Add(new List<EnemiesWave>());
        allEnemiesWaves[0][2].Add(new EnemiesWave(0, 80, 1.25f, 5));
        // fala 3
        allEnemiesWaves[0].Add(new List<EnemiesWave>());
        allEnemiesWaves[0][3].Add(new EnemiesWave(0, 100, 1.5f, 6));
        // fala 4
        allEnemiesWaves[0].Add(new List<EnemiesWave>());
        allEnemiesWaves[0][4].Add(new EnemiesWave(0, 100, 1.5f, 8));
        // fala 5
        allEnemiesWaves[0].Add(new List<EnemiesWave>());
        allEnemiesWaves[0][5].Add(new EnemiesWave(0, 100, 2, 10));
        // fala 6
        allEnemiesWaves[0].Add(new List<EnemiesWave>());
        allEnemiesWaves[0][6].Add(new EnemiesWave(0, 100, 2, 15));

    }
}

public class EnemiesWave
{
    /// <summary>
    /// Indeks w liście prefabów przeciwników
    /// </summary>
    public int prefabId;

    /// <summary>
    /// Ilość życia przeciwnika
    /// </summary>
    public float hp;

    /// <summary>
    /// Szybkość przeciwnika
    /// </summary>
    public float speed;

    /// <summary>
    /// Ilość przeciwników
    /// </summary>
    public int count;

    public EnemiesWave(int prefabId, float hp, float speed, int count = 1)
    {
        this.prefabId = prefabId;
        this.hp = hp;
        this.speed = speed;
        this.count = count;
    }
}
