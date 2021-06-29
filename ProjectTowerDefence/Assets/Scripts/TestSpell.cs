using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : SpellBook
{
    private float areaRadius = 8f;
    private Vector3 position;
    private bool isRunning = true;
    private float spellTime = 5f;
    private float betweenSpawnTime = 1f;

    private float elapsedTime = 0f;
    private float elapsedBetweenSpawnTime = 0f;
    

    public override void Run()
    {
        Debug.Log("Aktywowane zaklecie");
        isRunning = true;
    }

    private void SpawnCube()
    {
        GameObject cube = Resources.Load<GameObject>("Prefabs/Cube");
        if (cube == null)
        {
            Debug.Log("asset error");
        }
        Vector2 random = Random.insideUnitCircle * (areaRadius - 0.5f);

        GameObject spawnedCube = GameObject.Instantiate(cube, new Vector3(position.x + random.x, 13f, position.z + random.y), cube.transform.rotation);

        GameObject spellsCaster = GameObject.Find("SpellsCaster");
        spawnedCube.transform.SetParent(spellsCaster.transform);
    }

    public override void DealDMG()
    {
        elapsedTime += Time.deltaTime;
        elapsedBetweenSpawnTime += Time.deltaTime;

        if(elapsedTime <= spellTime)
        {
            if(elapsedBetweenSpawnTime >= betweenSpawnTime)
            {
                elapsedBetweenSpawnTime = 0f;
                SpawnCube();
            }
        }

        // wyłączenie skryptu po zniknięciu ostatniego elementu
        // nie wiem jak to połączyć
        if (elapsedTime - 3f >= spellTime)
        {
            isRunning = false;
        }
    }

    public override float AreaRadius
    {
        get
        {
            return areaRadius;
        }
    }
    public override Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
        }
    }
    public override bool IsRunning
    {
        get
        {
            return isRunning;
        }
    }
}
