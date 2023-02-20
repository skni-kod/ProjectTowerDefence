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
        private Animator animator;

        protected override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            base.Update();
            var currentAnimationState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentAnimationState.IsName("Shooting") && currentAnimationState.normalizedTime > 1)
            {
                animator.SetBool("isShooting", false);
            }
            Debug.Log(currentAnimationState.IsName("Shooting"));
            Debug.Log(currentAnimationState.normalizedTime);
        }

        protected override void Fire()
        {
            fireTimer -= Time.deltaTime;
            //if tower can fire and have a target
            if (fireTimer <= 0.0 && enemiesToHit.Length > 0)
            {
                //if enemie exists
                if (currEnemieToHit)
                {
                    animator.SetBool("isShooting", true);
                    fireTimer = maxCooldown;
                    GameObject tmp = Instantiate(Arrow);
                    //call  constructor of BasicArrow
                    tmp.GetComponent<BasicArrow>().Init(stats.dmgLvl + damageBase, hitRange / arrowTimeToHit, transform.position + BulletOffset,
                    Quaternion.FromToRotation(Vector3.left, transform.position + BulletOffset - currEnemieToHit.transform.position),
                    currEnemieToHit.gameObject);
                }
                else currEnemieToHit = enemiesToHit.ElementAt(0);
            }
        }
    }
}
