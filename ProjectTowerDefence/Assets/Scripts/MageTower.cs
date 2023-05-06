using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MageTower : Tower
{
    [SerializeField] public int maxTargets;
    protected override void Start()
    {
        base.Start();
        arrowTimeToHit = 0.0000001f;
    }

    protected override void  Update()
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
                    var enemyPosition = enemiesToHit.ElementAt(i).gameObject.transform.position;
                    Instantiate(Arrow, enemyPosition + new Vector3(0, 100, 0), Quaternion.identity);
                    PlayShootSound(enemyPosition);
                }
            }
        }
    }
}
