using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Goblin : Enemy
{


    protected override void Update()
    {
        Movement();

        healthBar.SetValue(100 * hp / maxHp);
    }

    protected override void Movement()
    {
        // chwilowe rozwiazanie poruszania
        transform.position += new Vector3(Time.deltaTime * (speed/10), 0, 0);


        // fix it later
        //if (pathVectorList != null)
        //{
        //    Vector3 targetPosition = pathVectorList[currentPathIndex];
        //    if (Vector3.Distance(transform.position, targetPosition) > 1f)
        //    {
        //        Vector3 direction = (targetPosition - transform.position).normalized;
        //        transform.position = transform.position + direction * speed * Time.deltaTime;
        //    }
        //    else
        //    {
        //        currentPathIndex++;
        //        if (currentPathIndex >= pathVectorList.Count)
        //        {
        //            pathVectorList = null;
        //        }
        //    }
        //}


    }

}
