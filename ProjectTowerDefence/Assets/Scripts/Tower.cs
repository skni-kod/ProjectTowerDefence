using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    float damagePerHit, hitRange, hitCooldown, lastHit;

    // Start is called before the first frame update
    void Start()
    {
        // Ustawienie statystyk wieży
        damagePerHit = 15f;
        hitRange = 5f;
        hitCooldown = 1.5f;

        // Ustawienie tego na czas "z przeszłości" aby od razu wieża mogła strzelać
        lastHit = -hitCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastHit >= hitCooldown)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(gameObject.transform.position, hitRange, 1 << LayerMask.NameToLayer("Enemies"));
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
}
