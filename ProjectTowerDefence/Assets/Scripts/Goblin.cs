using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Goblin : Enemy
{
    [SerializeField]
    private float Hp=100f;
    [SerializeField]
    private float Speed = 20f;
    
    protected override void initStats()
    {
        base.hp = Hp;

        base.speed = Speed;
    }

}
