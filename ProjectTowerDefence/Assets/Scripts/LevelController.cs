using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
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
    protected List<EnemyInfo> enemiesToSpawn = new List<EnemyInfo>();

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
    public enum LevelPhase { PREPARATIONS, DEFENCE, LEVEL_COMPLETED }

    /// <summary>
    /// Obecna faza gry
    /// </summary>
    protected LevelPhase currentPhase;

    /// <summary>
    /// Numer obecnej fali przeciwników
    /// </summary>
    protected int currentEnemiesWave;

    /// <summary>
    /// Obecna faza gry
    /// </summary>
    public LevelPhase GetCurrentPhase { get { return currentPhase; } }

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
    /// Lista fal przeciwników dla obecnego poziomów
    /// </summary>
    protected List<EnemyWave> enemiesWaves = new List<EnemyWave>();

    /// <summary>
    /// Numer zestawu fal przeciwników, który mas być użyty. Można ustawiać z edytorze Unity
    /// </summary>
    [SerializeField]
    protected int enemiesWavesSetId;

    /// <summary>
    /// Tekts przedstawiający graczowi aktualny numer fali.
    /// </summary>
    protected Text waveText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        DefineEnemiesWaves();
        WaveTextStart();
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
        currentPhase = LevelPhase.PREPARATIONS;
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
            case LevelPhase.PREPARATIONS:
                // TODO: Tutaj trzeba dodać jakieś czekanie aż gracz kliknie przycisk by rozpocząć kolejną fazę
                StartNewWave();
                break;

            case LevelPhase.DEFENCE:
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
        if (currentEnemiesWave >= enemiesWaves.Count)
        {
            Debug.LogError($"Cannot start wave {currentEnemiesWave} - not found");
            return;
        }

        Debug.Log($"Starting wave {currentEnemiesWave}");

        waveText.text = "Current Wave " + (currentEnemiesWave+1).ToString()+"/"+(enemiesWaves.Count);//przekazanie do text aktualnej fali

        enemiesToSpawn = enemiesWaves[currentEnemiesWave].GetEnemiesToSpawn();

        currentPhase = LevelPhase.DEFENCE;
    }

    /// <summary>
    /// Zakańcza obecną falę przeciwników i rozpoczyna fazę przygotowań
    /// </summary>
    protected void EndCurrentWave()
    {
        Debug.Log($"Ending wave {currentEnemiesWave}");

        currentEnemiesWave++;

        if (currentEnemiesWave < enemiesWaves.Count)
        {
            currentPhase = LevelPhase.PREPARATIONS;
        }
        else
        {
            Debug.Log("Level completed");
            currentPhase = LevelPhase.LEVEL_COMPLETED;
            // TODO: Obsługa przejścia do następnej mapy, czy coś
        }
    }

    /// <summary>
    /// Spawnuje następnego przeciwnika
    /// </summary>
    protected void SpawnNextEnemy()
    {
        if (enemiesToSpawn.Count > 0)
        {
            var enemyToSpawn = enemiesToSpawn[0];

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
    /// Utworzenie listy fal przeciwników. Tutaj zdefiniowane są wszystkie zestawy fal
    /// </summary>
    private void DefineEnemiesWaves()
    {
        enemiesWaves.Clear();

        switch (enemiesWavesSetId)
        {
            default:
                Debug.LogError("Nieprawidłowy numer zestawu fal - ładowanie zestawu nr 0");
                enemiesWavesSetId = 0;
                DefineEnemiesWaves();
                break;
            case 0:
                // zestaw 0
                Debug.Log("Ładowanie zestawu fal nr 0");
                // fala 0
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 50, 1))
                    .Build()
                    );
                // fala 1
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 50, 1, 3))
                    .Build()
                    );
                // fala 2
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 50, 1, 5))
                    .Build()
                    );
                // fala 3
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 2))
                    .Build()
                    );
                // fala 4
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 4))
                    .Build()
                    );
                // fala 5
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 7))
                    .Build()
                    );
                // fala 6
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 10))
                    .Build()
                    );
                break;

            case 1:
                // zestaw 1
                Debug.Log("Ładowanie zestawu fal nr 1");
                // fala 0
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 100, 1, 2))
                    .Build()
                    );
                // fala 1
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(0, 100, 1, 5))
                    .Build()
                    );
                // fala 2
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(1, 120, 1, 2))
                    .AddEnemy(new EnemyInfo(0, 100, 1, 5))
                    .Build()
                    );
                // fala 3
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(1, 120, 1, 5))
                    .AddEnemy(new EnemyInfo(0, 100, 1, 7))
                    .Build()
                    );
                // fala 4
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 7))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 15))
                    .Build()
                    );
                // fala 5
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 10))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 20))
                    .Build()
                    );
                // fala 6
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 20))
                    .Build()
                    );
                break;

            case 2:
                // zestaw 2
                Debug.Log("Ładowanie zestawu fal nr 2");
                // fala 0
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 1))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 5))
                    .Build()
                    );
                // fala 1
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 2))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 10))
                    .Build()
                    );
                // fala 2
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 5))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 15))
                    .Build()
                    );
                // fala 3
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(2, 150, 0.5f))
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 1))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 5))
                    .Build()
                    );
                // fala 4
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(2, 150, 0.5f, 2))
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 5))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 10))
                    .Build()
                    );
                // fala 5
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(2, 150, 0.5f, 5))
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 10))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 15))
                    .Build()
                    );
                // fala 6
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(2, 150, 0.5f, 7))
                    .AddEnemy(new EnemyInfo(1, 120, 1.25f, 15))
                    .AddEnemy(new EnemyInfo(0, 100, 1.25f, 20))
                    .Build()
                    );
                // fala 7
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.RANDOM)
                    .AddEnemy(new EnemyInfo(2, 150, 0.7f, 10))
                    .AddEnemy(new EnemyInfo(0, 100, 1.5f, 25))
                    .Build()
                    );
                // fala 8
                enemiesWaves.Add(EnemyWave.New(EnemiesSpawnStrategy.FIXED)
                    .AddEnemy(new EnemyInfo(2, 150, 0.7f, 20))
                    .Build()
                    );
                break;

            /*case 3:
                // TODO: Tutaj będą zdefiniowane kolejne zestawy fal przeciwników
                break;*/
        }
    }
    /// <summary>
    /// Przekazanie graczowi na ekranie aktualnej fali
    /// </summary>
    private void WaveTextStart()
    {
        waveText = GameObject.Find("WaveText").GetComponentInChildren<Text>();
    }
}

/// <summary>
/// Typ wylieczeniowy sposobu spawnowania przeciwników
/// </summary>
public enum EnemiesSpawnStrategy
{
    RANDOM, FIXED
}

public class EnemyInfo
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

    public EnemyInfo(int prefabId, float hp, float speed, int count = 1)
    {
        this.prefabId = prefabId;
        this.hp = hp;
        this.speed = speed;
        this.count = count;
    }
}

public class EnemyWave
{
    /// <summary>
    /// Strategira spawnowania przeciwników w danej fali
    /// </summary>
    private EnemiesSpawnStrategy enemiesSpawnStrategy;

    /// <summary>
    /// Lista przeciwnków w danej fali
    /// </summary>
    private List<EnemyInfo> enemies = new List<EnemyInfo>();

    /// <summary>
    /// Prywatny konstruktor EnemyWave
    /// </summary>
    /// <param name="enemiesSpawnStrategy">Strategira spawnowania przeciwników w danej fali</param>
    private EnemyWave(EnemiesSpawnStrategy enemiesSpawnStrategy)
    {
        this.enemiesSpawnStrategy = enemiesSpawnStrategy;
    }

    /// <summary>
    /// Tworzenie nowej fali przeciwników
    /// </summary>
    /// <param name="enemiesSpawnStrategy">Strategira spawnowania przeciwników w danej fali</param>
    public static Builder New(EnemiesSpawnStrategy enemiesSpawnStrategy)
    {
        return new Builder(enemiesSpawnStrategy);
    }

    public List<EnemyInfo> GetEnemiesToSpawn()
    {
        var result = new List<EnemyInfo>();

        if (enemies.TrueForAll(enemyInfo => enemyInfo.count == 1))
        {
            result.AddRange(enemies);
        }
        else
        {
            enemies.ForEach(enemyInfo =>
            {
                for (int i = 0; i < enemyInfo.count; i++)
                {
                    result.Add(new EnemyInfo(enemyInfo.prefabId, enemyInfo.hp, enemyInfo.speed));
                }
            });
        }

        switch (enemiesSpawnStrategy)
        {
            case EnemiesSpawnStrategy.FIXED:
            default:
                return result;
            case EnemiesSpawnStrategy.RANDOM:
                int n = result.Count;
                while (n > 1)
                {
                    n--;
                    var k = Random.Range(0, n + 1);
                    var tmp = result[k];
                    result[k] = result[n];
                    result[n] = tmp;
                }
                return result;
        }
    }

    /// <summary>
    /// Klasa pomocnicza do tworzenia fal przeciwnków
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Tworzona aktualnie fala przeciwników
        /// </summary>
        private EnemyWave enemyWave;

        public Builder(EnemiesSpawnStrategy enemiesSpawnStrategy)
        {
            enemyWave = new EnemyWave(enemiesSpawnStrategy);
        }

        /// <summary>
        /// Dodanie przeciwnika(-ów) do fali
        /// </summary>
        /// <param name="enemyInfo">Informacje o przeciwniku(-ach)</param>
        public Builder AddEnemy(EnemyInfo enemyInfo)
        {
            enemyWave.enemies.Add(enemyInfo);
            return this;
        }

        /// <summary>
        /// Zakończenie tworzenia fali
        /// </summary>
        /// <returns>Utworzona fala</returns>
        public EnemyWave Build()
        {
            return enemyWave;
        }
    }
}
