using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Pathfinding pathfinding;
    private Camera worldCamera;
    void Start()
    {
        pathfinding = new Pathfinding(10, 10);
        worldCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseposition = worldCamera.ScreenToWorldPoint(Input.mousePosition);

            List<GridNode> path = pathfinding.Path(0, 0, pathfinding.GetGrid().GetCoordinate(mouseposition).x, pathfinding.GetGrid().GetCoordinate(mouseposition).y);
            if (path != null)
            {
                for (int i = 0; i < path.Count -1 ; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].X, path[i].Y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].X, path[i + 1].Y) + Vector3.one * 5f, Color.red);
                }
            }
        }
    }
}
