using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private BarController cooldownBar;

    protected float damagePerHit, hitRange;
    public float maxCooldown;
    [System.Serializable]
    public struct Stats
    {
        public int spdLvl;
        public int dmgLvl;
    }
    public Stats stats;
    [HideInInspector]
    public float hitCooldown, lastHit;
    float arrowTimeToHit, fireArrowTimer;
    public Collider[] enemiesToHit;
    private Collider currEnemieToHit;

    // arrow prefab
    public GameObject Arrow;
    // Start is called before the first frame update
    void Start()
    {
        maxCooldown = 1.5f;
        // Ustawienie statystyk wieży
        damagePerHit = 15f;
        hitRange = 15f;
        hitCooldown = maxCooldown;
        arrowTimeToHit = 0.5f;
        // Ustawienie tego na czas "z przeszłości" aby od razu wieża mogła strzelać
        lastHit = -hitCooldown;

        
        cooldownBar = GetComponentInChildren<BarController>();
        //cooldownBar.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesDetection();

        FireArrow();
        //TowerDealingDamage();
        CooldownBarUpdate();
    }

    /// <summary>
    /// wykrywanie przeciwników w danym promieniu oraz dodawanie ich do tablicy
    /// </summary>
    private void EnemiesDetection()
    {
        var enemiesInRange = Physics.OverlapSphere(gameObject.transform.position, hitRange, 1 << LayerMask.NameToLayer("Enemies"));
        enemiesToHit = enemiesInRange.ToList().FindAll(enemyCollider => !enemyCollider.GetComponent<Enemy>().IsDead).ToArray();
    }

    /// <summary>
    /// zadawanie obrazeń pierwszemu celowi w tablicy w określonch odstępach czasu
    /// </summary>
    private void TowerDealingDamage()
    {
        if (Time.time - lastHit >= hitCooldown)
        {
            if (enemiesToHit.Length > 0)
            {
                // TODO: W przyszłości tutaj trzeba umieścić jakis algorytm, który wybierze
                // przeciwnika, w którego wieża ma strzelać
                // Tymczasowo bierze pierwszego z listy

                if (currEnemieToHit == null)
                {
                    currEnemieToHit = enemiesToHit.ElementAt(0);
                }

                if (currEnemieToHit.GetComponent<Enemy>().Hit(damagePerHit+stats.dmgLvl))
                {
                    currEnemieToHit = null;
                }

                lastHit = Time.time;
            }

        }
    }
    private void FireArrow()
    {
        fireArrowTimer -= Time.deltaTime;
        if(fireArrowTimer<=0.0 && enemiesToHit.Length>0)
        {
            if(currEnemieToHit)
            {
                
                fireArrowTimer = maxCooldown;
                GameObject tmp =Instantiate(Arrow);
                //call  constructor of BasicArrow
                tmp.GetComponent<BasicArrow>().Init(stats.dmgLvl, hitRange/arrowTimeToHit, transform.position, 
                Quaternion.FromToRotation(Vector3.left, transform.position-currEnemieToHit.transform.position),
                currEnemieToHit.gameObject);
            }
            else currEnemieToHit = enemiesToHit.ElementAt(0);
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
