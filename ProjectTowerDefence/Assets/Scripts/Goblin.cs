using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Goblin : Enemy
{

    protected override void Update()
    {
        Movement(base.GetEnemyDeltaTime());
    }


    protected override void InitStats()
    {
    }

    protected override void Movement(float deltaTime)
    {
        // chwilowe rozwiazanie poruszania
        transform.position += new Vector3(deltaTime * (speed/10), 0, 0);
    }

}
