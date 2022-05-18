using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    private Grid<GridNode> grid;
    private List<GridNode> openList;
    private List<GridNode> closedList;


    public Pathfinding(int width, int height)
    {
        Instance = this;
        grid = new Grid<GridNode>(width, height, 1f, Vector3.zero, (Grid<GridNode> g, int x, int y) => new GridNode(g, x, y));
    }

    /// <summary>
    /// Metoda oblicza najkrótsza dorgę miedzy punktamim o podanych współrzędnych
    /// </summary>
    /// <param name="startX"> współrzędna x punktu początkowego </param>
    /// <param name="startZ"> współrzędna y punktu początkowego </param>
    /// <param name="endX"> współrzędna x punktu końcowego </param>
    /// <param name="endZ"> współrzędna y punktu początkowego </param>
    /// <returns> zwraca liste węzłów wyznaczajacych najkrótsza trase </returns>
    public List<GridNode> Path(int startX, int startZ, int endX, int endZ)
    {
        GridNode startNode = grid.GetObject(startX, startZ);
        GridNode endNode = grid.GetObject(endX, endZ);

        openList = new List<GridNode> { startNode };
        closedList = new List<GridNode>();

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Heigth; y++)
            {
                GridNode gridNode = grid.GetObject(x, y);
                gridNode.gCost = int.MaxValue;

                gridNode.fCostCalculate();

                gridNode.previoseNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = DistanceCost(startNode, endNode);
        startNode.fCostCalculate();

        while (openList.Count > 0)
        {
            GridNode currentNode = GetLowestCostNode(openList);
            if (currentNode == endNode)
            {
                return FinalPath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);


            foreach (GridNode node in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(node))
                {
                    continue;
                }
                if (!node.isAvailable)
                {
                    closedList.Add(node);
                    continue;
                }
                int tmpGCost = currentNode.gCost + DistanceCost(currentNode, node);
                if (tmpGCost < node.gCost)
                {
                    node.previoseNode = currentNode;
                    node.gCost = tmpGCost;
                    node.hCost = DistanceCost(node, endNode);
                    node.fCostCalculate();

                    if (!openList.Contains(node))
                    {
                        openList.Add(node);
                    }
                }

            }
        }
        return null;
    }
    /// <summary>
    /// Metoda oblicza najkrótsza dorgę miedzy punktamim o podanych współrzędnych
    /// </summary>
    /// <param name="startWorldPosition"> współrzędne punktu startowego</param>
    /// <param name="endWorldPosition"> współrzędne punktu koncowego </param>
    /// <returns></returns>
    public List<Vector3> Path(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        List<GridNode> path = Path(grid.GetCoordinate(startWorldPosition).x, grid.GetCoordinate(startWorldPosition).y, grid.GetCoordinate(endWorldPosition).x, grid.GetCoordinate(endWorldPosition).y);
        
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (GridNode node in path)
            {
                vectorPath.Add(new Vector3(node.X,0, node.Y) * grid.CellSize + new Vector3(1,0,1) * grid.CellSize * 0.5f);

            }
            
            return vectorPath;
        }
    }
    /// <summary>
    /// Metoda przechodzi od koncowego wezla scierzki poprzez poprzednie wezy i zwraca najkrótsza liste wezłów takich aby dotrzec podanego wezła
    /// </summary>
    /// <param name="gridNode"></param>
    /// <returns> zwraca liste wezłów potrzebnych do osiagniecia podanego wezła </returns>
    private List<GridNode> FinalPath(GridNode gridNode)
    {
        List<GridNode> path = new List<GridNode>();
        path.Add(gridNode);
        GridNode currentNode = gridNode;

        while (currentNode.previoseNode != null)
        {
            path.Add(currentNode.previoseNode);
            currentNode = currentNode.previoseNode;
        }
        path.Reverse();
        return path;
    }
    /// <summary>
    /// Metoda zwraca koszt przejscia pomiedzy wezłami
    /// </summary>
    /// <param name="from">wezel z którego wyruszamy</param>
    /// <param name="where">wezel do ktrorego zmierzamy </param>
    /// <returns> zwraca koszt przejscia miedzy wezłami </returns>
    private int DistanceCost(GridNode from, GridNode where)
    {
        int distanceX = Mathf.Abs(from.X - where.X);
        int distanceY = Mathf.Abs(from.Y - where.Y);
        int rest = Mathf.Abs(distanceX - distanceY);
        return DIAGONAL_COST * Mathf.Min(distanceY, distanceX) + STRAIGHT_COST * rest;
    }

    /// <summary>
    /// Metoda sprawdza który wezeł z podanej listy ma najmniejszy FCost
    /// </summary>
    /// <param name="gridNodeList">lista węzłów do sprawdzenia</param>
    /// <returns> zwraca węzeł który z podanych ma najmniejszy fCost</returns>
    private GridNode GetLowestCostNode(List<GridNode> gridNodeList)
    {
        GridNode lowerstCostNode = gridNodeList[0];

        for (int i = 1; i < gridNodeList.Count; i++)
        {
            if (gridNodeList[i].fCost < lowerstCostNode.fCost)
            {
                lowerstCostNode = gridNodeList[i];
            }
        }
        return lowerstCostNode;
    }
    /// <summary>
    /// Metoda zwraca listę wezłow z ktorymi sasiaduje podany wezel
    /// </summary>
    /// <param name="currentNode">wezeł ktorego sasiadow chcesz znalesc</param>
    /// <returns> zwraca liste wezlow sasadujacych z podanym wezlem </returns>
    private List<GridNode> GetNeighbourList(GridNode currentNode)
    {
        List<GridNode> neighbourList = new List<GridNode>();

        if (currentNode.X - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
            if (currentNode.Y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
            }
            if (currentNode.Y + 1 < grid.Heigth)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
            }
        }
        if (currentNode.X + 1 < grid.Width)
        {
            neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
            if (currentNode.Y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
            }
            if (currentNode.Y + 1 < grid.Heigth)
            {
                neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
            }
        }
        if (currentNode.Y - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
        }
        if (currentNode.Y + 1 < grid.Heigth)
        {
            neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));
        }
        return neighbourList;
    }
    /// <summary>
    /// Metoda zwraca wskazany wezeł grida
    /// </summary>
    /// <param name="x">współrzędna x gridaz</param>
    /// <param name="y">współrzędna y grida</param>
    /// <returns> zwraca wskazany węzeł grida</returns>
    private GridNode GetNode(int x, int y)
    {
        return grid.GetObject(x, y);
    }
    /// <summary>
    /// Metoda zwraca grid uzywany przez PathFinding
    /// </summary>
    /// <returns> grid uzywany przez PathFinding</returns>
    public Grid<GridNode> GetGrid()
    {
        return grid;
    }
}
