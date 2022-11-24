using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MageTower : Tower
{
    [SerializeField] public int maxTargets;
    void  Start()
    {
        // Ustawienie statystyk wieży
        this.hitCooldown = maxCooldown;
        this.arrowTimeToHit = 0.0000001f;
        // Ustawienie tego na czas "z przeszłości" aby od razu wieża mogła strzelać
        this.lastHit = -hitCooldown;
    }

    void Update()
    {
        EnemiesDetection();

        Fire();
        //TowerDealingDamage();
        CooldownBarUpdate();
    }

    protected override void Fire()
    {
        fireTimer -= Time.deltaTime;
        //if tower can fire and have a target
        if(fireTimer<=0.0 && enemiesToHit.Length>0)
        {
            for(int i =0; i<maxTargets; i++)
            {
                if(enemiesToHit[i])
                {
                    enemiesToHit.ElementAt(i).gameObject.GetComponent<Enemy>().Hit(stats.dmgLvl+damageBase);
                    Debug.Log("Fire"+i);
                    fireTimer = maxCooldown;
                    Instantiate(Arrow, enemiesToHit.ElementAt(i).gameObject.transform.position+new Vector3(0,100,0), Quaternion.identity);

                                          

                }
                
            }
            
            
        }
    }
}
