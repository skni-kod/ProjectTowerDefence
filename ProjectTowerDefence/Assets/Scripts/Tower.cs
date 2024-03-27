using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private BarController cooldownBar;
    public int towerId;

    [SerializeField] protected float damageBase, hitRange;
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
    [SerializeField] public float arrowTimeToHit;
    protected float fireTimer;
    public Collider[] enemiesToHit;
    protected Collider currEnemieToHit;
    [SerializeField] protected Vector3 BulletOffset = new Vector3(0,0,0);
    // arrow prefab
    public GameObject Arrow;
    public AudioClip placingSound;
    public AudioClip[] shootSounds;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Ustawienie statystyk wieży
        hitCooldown = maxCooldown;
        // Ustawienie tego na czas "z przeszłości" aby od razu wieża mogła strzelać
        lastHit = -hitCooldown;

        
        cooldownBar = GetComponentInChildren<BarController>();
        //cooldownBar.Initialize();

        AudioSource.PlayClipAtPoint(placingSound, transform.position);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        EnemiesDetection();

        Fire();
        //TowerDealingDamage();
        CooldownBarUpdate();
    }

    /// <summary>
    /// wykrywanie przeciwników w danym promieniu oraz dodawanie ich do tablicy
    /// </summary>
    protected void EnemiesDetection()
    {
        var enemiesInRange = Physics.OverlapSphere(gameObject.transform.position, hitRange, 1 << LayerMask.NameToLayer("Enemies"));
        enemiesToHit = enemiesInRange.ToList().FindAll(enemyCollider => !enemyCollider.GetComponent<Enemy>().IsDead).ToArray();
    }
    /// <summary>
    //Fire arrow method, need detected enemy earlier
    /// </summary>
    protected virtual void Fire()
    {
        fireTimer -= Time.deltaTime;
        //if tower can fire and have a target
        if(fireTimer<=0.0 && enemiesToHit.Length>0)
        {
            //if enemie exists
            if(currEnemieToHit)
            {
                
                fireTimer = maxCooldown;
                GameObject tmp =Instantiate(Arrow);
                //call  constructor of BasicArrow
                tmp.GetComponent<BasicArrow>().Init(stats.dmgLvl+damageBase, hitRange/arrowTimeToHit, transform.position+BulletOffset, 
                Quaternion.FromToRotation(Vector3.left, transform.position+BulletOffset-currEnemieToHit.transform.position),
                currEnemieToHit.gameObject);
                PlayShootSound();
            }
            else currEnemieToHit = enemiesToHit.ElementAt(0);
        }
    }

    /// <summary>
    /// Aktualizacja paska czasu oczekiwania
    /// </summary>
    protected void CooldownBarUpdate()
    {
        //cooldownBar.SetValue(100 * (1 - ((Time.time - lastHit) / hitCooldown)));
    }

    protected void PlayShootSound()
    {
        PlayShootSound(transform.position);
    }

    protected void PlayShootSound(Vector3 position)
    {
        if (shootSounds.Length > 0)
        {
            AudioSource.PlayClipAtPoint(shootSounds[Random.Range(0, shootSounds.Length)], position);
        }
    }
}
