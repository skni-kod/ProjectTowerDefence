using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    private Grid<GridNode> grid;
    private int x;
    public int X { get { return x; } set { x = value; } }
    private int y;
    public int Y { get { return y; } set { y = value; } }

    public bool isAvailable;

    public int gCost;
    public int hCost;
    public int fCost;

    public GridNode previoseNode;

    public GridNode(Grid<GridNode> grid , int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isAvailable = true;
        //Grid<int> worldGrid = new Grid<int>(2, 5, 5f, new Vector3(0, 0, 0), () => new int());
    }

    public override string ToString()
    {
        return x + " , " + y;
    }

    /// <summary>
    /// Metoda oblicza fCost 
    /// </summary>
    public void fCostCalculate()
    {
        fCost = gCost + hCost;
    }
}
