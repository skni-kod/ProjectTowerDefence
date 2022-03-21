using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Goblin : Enemy
{
    public Animator animator;

    /*chwilowe rozwiazanie do poruszania sie tam gdzie sie kliknie*/
    RaycastHit hit;
    int tmp = 0;
    /*************************************************************/

    //void Start()
    //{
    //    this.animator = GetComponent<Animator>();
    //}

    protected override void Update()
    {
        if (!IsDead)
        {
            Movement();
        }
        else
        {
            DestroyOnDeathAnimationEnd();
        }

        healthBar.SetValue(100 * hp / maxHp);
    }

    protected override void Movement()
    {
        // chwilowe rozwiazanie poruszania
        //transform.position += new Vector3(Time.deltaTime * (speed/10), 0, 0);

        /*chwilowe rozwiazanie do poruszania sie tam gdzie sie kliknie*/
        //  return;
        if (Mouse.current.leftButton.wasPressedThisFrame)//Input.GetMouseButtonDown(0))
        {

            Debug.Log("Movement:Goblin");
            //  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
                transform.forward = new Vector3(destination.x -transform.position.x , destination.y-transform.position.y  , destination.z -transform.position.z );//obrot goblina w kierunku w którym podąża
                Debug.Log("The ray hit at: " + destination);


                int x = pathfinding.GetGrid().GetCoordinate(hit.point).x;
                int y = pathfinding.GetGrid().GetCoordinate(hit.point).y;
                Grid<GridNode> grid = pathfinding.GetGrid();
                GridNode gridNode = grid.GetObject(x, y);

                if (gridNode.isAvailable)
                {
                    Debug.Log(gridNode.isAvailable);
                    SetDestinationPosition(destination);
                    tmp = 0;

                    // turn on running animation
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isDead", false);
                }
                else
                {
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isDead", false);
                }


            }
        }
        /*************************************************************/

        if (pathVectorList != null)
        {
            /**chwilowe rozwiazanie do poruszania sie tam gdzie sie kliknie*/
            Debug.DrawLine(GetPositionXZ(), pathVectorList[tmp], Color.green);
            for (int i = tmp; i < pathVectorList.Count - 1; i++)
            {
                if (Vector3.Distance(GetPositionXZ(), pathVectorList[i]) < 1f)
                {
                    tmp++;
                }
                Debug.DrawLine(pathVectorList[i], pathVectorList[i + 1], Color.green);
            }
            /*************************************************************/

            Vector3 targetPosition = pathVectorList[currentPathIndex];

            if (Vector3.Distance(GetPositionXZ(), targetPosition) > 1f)
            {
                Vector3 direction = (targetPosition - GetPositionXZ()).normalized;
                transform.position = transform.position + direction * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    pathVectorList = null;
                }
            }
        }

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
        Debug.Log("dupa");

        // turn on dead animation
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);

        // wait until the animation is finised than delete object from the scene
        //StartCoroutine(ExecuteAfterSec(30));

        //Destroy(this.gameObject);
    }

    private IEnumerator ExecuteAfterSec(float time)
    {
        Debug.Log("chuj");
        yield return new WaitForSeconds(time);
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
