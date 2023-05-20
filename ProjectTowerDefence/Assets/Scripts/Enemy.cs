﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

abstract public class Enemy : MonoBehaviour
{
    protected float maxHp;

    protected List<Vector3> pathVectorList;
    protected int currentPathIndex;

    protected float hp;
    protected int lvl;

    protected Rigidbody rigidbodyComponent;
    protected BarController healthBar;

    protected Pathfinding pathfinding;

    public AudioClip[] sounds;
    private float nextSoundDelay;

    public static List<Enemy> listOfEnemies = new List<Enemy>();

    [HideInInspector]
    public Vector3 destination;

    //[SerializeField]
    //protected GameObject nexus;

    /// <summary>
    /// Właściwość wskazująca, czy przeciwnik został zabity (ale obiekt nie został jeszcze usunięty)
    /// </summary>
    public bool IsDead { get => hp <= 0; }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        //pathfinding = Pathfinding.Instance;
        //SetDestinationPosition(nexus.transform.Find("nexus").position);

        rigidbodyComponent = GetComponent<Rigidbody>();

        healthBar = transform.GetComponentInChildren<BarController>();
        //Debug.Log(healthBar);
        listOfEnemies.Add(this);

        hp = maxHp;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!IsDead)
        {
            Movement();
            TryToPlaySound();
        }

        // Tymczasowe wyświetlanie ilości zdrowia dla ułatwienia testowania
        //Debug.Log(gameObject.name + "'s health: " + hp);
    }

    /// <summary>
    /// poruszanie sie postaci
    /// </summary>
    protected abstract void Movement();

    /// <summary>
    /// atak przeciwnika
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// inicjalizacja statystyk
    /// </summary>
    /// <param name="maxHp">Ilość punktów zdrowia</param>
    /// <param name="speedMultiplier">Mnożnik podstawowej prędkości przeciwnika</param>
    public void InitStats(float maxHp, float speedMultiplier)
    {
        this.maxHp = maxHp;

        var navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = navMeshAgent.speed * speedMultiplier;
        navMeshAgent.angularSpeed = navMeshAgent.angularSpeed * speedMultiplier;
    }

    /// <summary>
    /// Zadawanie obrażeń przeciwnikowi
    /// </summary>
    /// <param name="damageAmount">Ilość obrażeń</param>
    public bool Hit(float damageAmount)
    {

        hp -= damageAmount;
        if (hp <= 0)
        {
            // Usunięcie obiektu, bo umarł
            EnemyKilled();

            return true;
        }
        // Aktualizacja paska zdrowia
        healthBar.SetValue(100 * hp / maxHp);
        return false;
    }

    protected virtual void EnemyKilled()
    {
        Destroy(gameObject);
    }

    /**
     * metoda ustawia punkt docelowy sciezki
     * @param targetPosition Vector3 współrzędnych punktu docelowego
     */
    public void SetDestinationPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        //pathVectorList = Pathfinding.Instance.Path(transform.position, targetPosition);
        pathVectorList = pathfinding.Path(transform.position, targetPosition);
        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    /**
     * metoda zwraca współrzedne X oraz Z obiektu
     * @return Vektor3 pozycji obiektu o współrzędnej Y równej 0
     */
    protected Vector3 GetPositionXZ()
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnDestroy()
    {
        listOfEnemies.Remove(this);
    }

    private void TryToPlaySound()
    {
        if (sounds.Length > 0)
        {
            nextSoundDelay -= Time.deltaTime;
            if (nextSoundDelay <= 0)
            {
                AudioSource.PlayClipAtPoint(sounds[Random.Range(0, sounds.Length)], transform.position);
                nextSoundDelay = Random.Range(3.0f, 7.0f);
            }
        }
    }

}
