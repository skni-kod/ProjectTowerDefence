using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    public Camera worldCamera;
    RaycastHit hit;

    public Pathfinding pathfinding;

    


    private void Awake()
    {
        pathfinding = new Pathfinding(100, 100);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Zablokowano na: " + hit.point);
                int x = pathfinding.GetGrid().GetCoordinate(hit.point).x;
                int y = pathfinding.GetGrid().GetCoordinate(hit.point).y;
                Grid<GridNode> grid = pathfinding.GetGrid();
                GridNode gridNode = grid.GetObject(x, y);

                gridNode.isAvailable = !gridNode.isAvailable;



                //foreach (GameObject enemy in Enemy.listOfEnemies)
                //{
                //    Enemy enemyComponent = enemy.GetComponent<Enemy>();
                //    enemyComponent.SetDestinationPosition(enemyComponent.destination);
                //}
                foreach (Enemy enemy in Enemy.listOfEnemies)
                {
                    enemy.SetDestinationPosition(enemy.destination);
                }

                Debug.DrawLine(new Vector3(gridNode.X - 0.5f, 0, gridNode.Y - 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, new Vector3(gridNode.X + 0.5f, 0, gridNode.Y+ 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, Color.red , 20f);
                Debug.DrawLine(new Vector3(gridNode.X -0.5f, 0, gridNode.Y + 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, new Vector3(gridNode.X+ 0.5f, 0, gridNode.Y - 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, Color.red, 20f);

                


            }
        }
    }
}
