using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Trebuchet : Tower
    {
        private static readonly float shootingAnimationLength = 9.3f;
        private static readonly float bulletAnimationDelay = 0.1f;
        private Animator animator;
        private bool awaitingBulletFire = false;

        protected override void Start()
        {
            base.Start();
            animator = transform.Find("Trebuchet.fbx").GetComponent<Animator>();
            animator.speed = shootingAnimationLength / maxCooldown;
        }

        protected override void Update()
        {
            base.Update();
            var currentAnimationState = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log(currentAnimationState.IsName("Shooting"));
            Debug.Log(currentAnimationState.normalizedTime);
        }

        protected override void Fire()
        {
            fireTimer -= Time.deltaTime;
            //if tower can fire and have a target
            if (fireTimer <= 0.0 && enemiesToHit.Length > 0)
            {
                currEnemieToHit = enemiesToHit.ElementAt(0);
                animator.SetTrigger("Shoot");
                fireTimer = maxCooldown;
                awaitingBulletFire = true;
            }
            else if (awaitingBulletFire && maxCooldown - fireTimer > maxCooldown * bulletAnimationDelay)
            {
                GameObject tmp = Instantiate(Arrow);
                //call constructor of BasicArrow
                tmp.GetComponent<BasicArrow>().Init(
                    stats.dmgLvl + damageBase,
                    hitRange / arrowTimeToHit,
                    transform.position + BulletOffset,
                    Quaternion.FromToRotation(Vector3.left, transform.position + BulletOffset - currEnemieToHit.transform.position),
                    currEnemieToHit.gameObject);
                awaitingBulletFire = false;
            }
        }
    }
}
