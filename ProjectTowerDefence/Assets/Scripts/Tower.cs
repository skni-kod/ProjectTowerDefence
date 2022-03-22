using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private BarController cooldownBar;

    public GameObject thisTower;

    protected float damagePerHit, hitRange;
    public float maxCooldown;
    public struct Stats
    {
        public int spdLvl;
        public int dmgLvl;
    }
    public Stats stats;
    [HideInInspector]
    public float hitCooldown, lastHit;
    public Collider[] enemiesInRange;
    private Collider currEnemieToHit;

    private float dist, shortestDist;
    private int shortesDistIndex;

    // Start is called before the first frame update
    void Start()
    {
        maxCooldown = 1.5f;
        // Ustawienie statystyk wieży
        damagePerHit = 15f;
        hitRange = 5f;
        hitCooldown = maxCooldown;

        // Ustawienie tego na czas "z przeszłości" aby od razu wieża mogła strzelać
        lastHit = -hitCooldown;

        cooldownBar = GetComponentInChildren<BarController>();
        //cooldownBar.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesDetection();
        TowerDealingDamage();
        CooldownBarUpdate();
    }

    /// <summary>
    /// wykrywanie przeciwników w danym promieniu oraz dodawanie ich do tablicy
    /// </summary>
    private void EnemiesDetection()
    {
        enemiesInRange = Physics.OverlapSphere(gameObject.transform.position, hitRange, 1 << LayerMask.NameToLayer("Enemies"));
        
    }

    /// <summary>
    /// zadawanie obrazeń pierwszemu celowi w tablicy w określonch odstępach czasu
    /// </summary>
    private void TowerDealingDamage()
    {

        if (Time.time - lastHit >= hitCooldown)
        {
            if (enemiesInRange.Length > 0)
            {
                //najbliższy przeciwnik

                shortestDist = 0;
                shortesDistIndex = 0;

                for (int i = 0; i < enemiesInRange.Length; i++)
                {
                    dist = Vector3.Distance(thisTower.transform.position, enemiesInRange[i].transform.position);
                    if (shortestDist > Vector3.Distance(thisTower.transform.position, enemiesInRange[i].transform.position))
                    {
                        shortestDist = Vector3.Distance(thisTower.transform.position, enemiesInRange[i].transform.position);
                        shortestDistIndex = i;
                        Debug.Log(shortestDistIndex);
                    }
                }


                // TODO: W przyszłości tutaj trzeba umieścić jakis algorytm, który wybierze
                // przeciwnika, w którego wieża ma strzelać
                // Tymczasowo bierze pierwszego z listy

                if (currEnemieToHit == null)
                {
                    currEnemieToHit = enemiesInRange[0];
                }

                if (currEnemieToHit.GetComponent<Enemy>().Hit(damagePerHit+stats.dmgLvl))
                {
                    currEnemieToHit = null;
                }

                lastHit = Time.time;
            }

        }
    }

    /// <summary>
    /// Aktualizacja paska czasu oczekiwania
    /// </summary>
    private void CooldownBarUpdate()
    {
        cooldownBar.SetValue(100 * (1 - ((Time.time - lastHit) / hitCooldown)));
    }
}
