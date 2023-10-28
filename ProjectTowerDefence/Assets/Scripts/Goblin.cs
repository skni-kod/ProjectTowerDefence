using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Goblin : Enemy
{
    public Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsDead)
        {
            DestroyOnDeathAnimationEnd();
        }

        healthBar.SetValue(100 * hp / maxHp);
    }

    protected override void Movement()
    {
        // turn on running animation
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", false);
    }

    protected override void Attack()
    {
        // do something...

        // turn on attack animation
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        animator.SetBool("isDead", false);
    }

    protected override void EnemyKilled()
    {
        // turn on dead animation
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
    }

    private void DestroyOnDeathAnimationEnd()
    {
        var currentAnimationState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentAnimationState.IsName("Death") && currentAnimationState.normalizedTime > 1)
        {
            Destroy(gameObject);
        }
    }
}
