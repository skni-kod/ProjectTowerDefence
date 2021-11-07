using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Goblin : Enemy
{

    /*chwilowe rozwiazanie do poruszania sie tam gdzie sie kliknie*/
    RaycastHit hit;
    int tmp = 0;
    /*************************************************************/

    protected override void Update()
    {
        Movement();

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

            //  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
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

}
