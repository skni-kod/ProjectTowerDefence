using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Pathfinding pathfinding;
    public Camera worldCamera;

    RaycastHit hit;
    void Start()
    {
        pathfinding = Pathfinding.Instance;
        //worldCamera = Camera.main;

    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Debug.Log("The ray hit at: " + hit.point);
                
        //        //Debug.Log("m "+ hit.point);
        //        List<GridNode> path = pathfinding.Path(0, 0, pathfinding.GetGrid().GetCoordinate(hit.point).x, pathfinding.GetGrid().GetCoordinate(hit.point).y);
        //        if (path != null)
        //        {
        //            for (int i = 0; i < path.Count -1 ; i++)
        //            {
        //                Debug.DrawLine(new Vector3(path[i].X,0, path[i].Y) * 10f + new Vector3(1,0,1) * 5f, new Vector3(path[i + 1].X,0, path[i + 1].Y) *10f+ new Vector3(1, 0, 1) * 5f, Color.green,2f);
        //            }
        //        }
        //    }
        //}
        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Zablokowano na: " + hit.point);
                int x = pathfinding.GetGrid().GetCoordinate(hit.point).x;
                int y = pathfinding.GetGrid().GetCoordinate(hit.point).y;

                pathfinding.GetGrid().GetObject(x,y).isAvailable = false;
                

            }
        }
    }
}
