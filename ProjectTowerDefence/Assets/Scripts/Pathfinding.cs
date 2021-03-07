using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;

    private Grid<GridNode> grid;
    private List<GridNode> openList;
    private List<GridNode> closedList;

    public Pathfinding(int width, int height)
    {
        grid = new Grid<GridNode>(width, height, 10f, new Vector3(0, 0, 0), (Grid<GridNode> g, int x, int y) => new GridNode(g, x, y));
    }

    private List<GridNode> Path(int startX ,int startY, int endX, int endY)
    {
        GridNode startNode = grid.GetObject(startX, startY);
        GridNode endNode = grid.GetObject(endX, endY);

        openList = new List<GridNode> {startNode};
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

        while (openList.Count>0)
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
    private int DistanceCost(GridNode from, GridNode where)
    {
        int distanceX = Mathf.Abs(from.X - where.X);
        int distanceY = Mathf.Abs(from.Y - where.Y);
        int rest = Mathf.Abs(distanceX - distanceY);
        return DIAGONAL_COST * Mathf.Min(distanceY, distanceX) + STRAIGHT_COST * rest;
    }

    private GridNode GetLowestCostNode(List<GridNode> gridNodeList)
    {
        GridNode lowerstCostNode = gridNodeList[0];

        for (int i = 0; i < gridNodeList.Count; i++)
        {
            if (gridNodeList[i].fCost < lowerstCostNode.fCost)
            {
                lowerstCostNode = gridNodeList[i];
            }
        }
        return lowerstCostNode;
    }

    private List<GridNode> GetNeighbourList(GridNode currentNode)
    {
        List<GridNode> neighbourList = new List<GridNode>();

        if (currentNode.X - 1 >= 0 )
        {
            neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
            if (currentNode.Y -1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
            }
            if (currentNode.Y + 1 < grid.Heigth)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y+1));
            }
        }
        if (currentNode.X + 1 < grid.Width)
        {
            neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
            if (currentNode.Y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
            }
            if (currentNode.Y + 1 < grid.Heigth)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
            }
        }
        if (currentNode.Y -1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.X, currentNode.Y -1));
        }
        if (currentNode.Y +1 <grid.Heigth)
        {
            neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));
        }
        return neighbourList;
    }

    private GridNode GetNode(int x, int y)
    {
        return grid.GetObject(x, y);
    }
}
