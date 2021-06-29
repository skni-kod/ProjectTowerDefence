using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private BarController cooldownBar;

    protected float damagePerHit, hitRange;

    [HideInInspector]
    public float hitCooldown, lastHit;
    public Collider[] enemiesInRange;


    // Start is called before the first frame update
    void Start()
    {
        // Ustawienie statystyk wieży
        damagePerHit = 15f;
        hitRange = 5f;
        hitCooldown = 1.5f;

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
                // TODO: W przyszłości tutaj trzeba umieścić jakis algorytm, który wybierze
                // przeciwnika, w którego wieża ma strzelać
                // Tymczasowo bierze pierwszego z listy
                enemiesInRange[0].GetComponent<Enemy>().Hit(damagePerHit);
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
