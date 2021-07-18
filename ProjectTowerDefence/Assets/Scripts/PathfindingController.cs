using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    public Camera worldCamera;
    RaycastHit hit;

    public Pathfinding pathfinding;
    public Terrain terrain;

    
    private bool showGridGizmos = false;
    private List<Vector2Int> blockedNodeIndexes;

    private void Awake()
    {
        
    }
    private void Start()
    {
        Vector3 mapsize = terrain.terrainData.size;
        Debug.Log(mapsize);
        pathfinding = new Pathfinding((int)mapsize.x, (int)mapsize.z);
        UbdateBlockedNodes();
    }

    public void UbdateBlockedNodes()
    {
        blockedNodeIndexes = new List<Vector2Int>();
        for (int i = 0; i < pathfinding.GetGrid().girdArraySize.x; i++)
        {
            for (int j = 0; j < pathfinding.GetGrid().girdArraySize.y; j++)
            {
                if (pathfinding.GetGrid().GetObject(i, j).isAvailable == false)
                {
                    blockedNodeIndexes.Add(new Vector2Int(i, j));
                }
            }
        }
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
                UbdateBlockedNodes();


                //foreach (GameObject enemy in Enemy.listOfEnemies)
                //{
                //    Enemy enemyComponent = enemy.GetComponent<Enemy>();
                //    enemyComponent.SetDestinationPosition(enemyComponent.destination);
                //}
                foreach (Enemy enemy in Enemy.listOfEnemies)
                {
                    enemy.SetDestinationPosition(enemy.destination);
                }

                //Debug.DrawLine(new Vector3(gridNode.X - 0.5f, 0, gridNode.Y - 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, new Vector3(gridNode.X + 0.5f, 0, gridNode.Y+ 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, Color.red , 20f);
                //Debug.DrawLine(new Vector3(gridNode.X -0.5f, 0, gridNode.Y + 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, new Vector3(gridNode.X+ 0.5f, 0, gridNode.Y - 0.5f) * grid.CellSize + new Vector3(1, 0, 1) * grid.CellSize * 0.5f, Color.red, 20f);

                


            }
        }
        if (Input.GetKeyDown("m"))
        {
            showGridGizmos = !showGridGizmos;
        }
        if (showGridGizmos)
        {
            Grid<GridNode> grid = pathfinding.GetGrid();
            grid.drawGridGizmos();
            grid.drawGridBlockedNodes(blockedNodeIndexes);
        }
    }
}
