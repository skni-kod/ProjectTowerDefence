using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    public int Width { get { return width; }}
    public int Heigth{ get {return height; }}

    private float cellSize;

    public float CellSize { get { return cellSize; } }
    private TGridObject[,] gridArray;
    private Vector3 startPosition;
    private TextMesh[,] debugGridTextArray;
    public Vector2 girdArraySize;

    public Grid(int width,int height,float cellSize, Vector3 startPosition, Func<Grid<TGridObject> , int, int, TGridObject> gridObjectType)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        
        gridArray = new TGridObject[width, height];
        debugGridTextArray = new TextMesh[width, height];
        this.startPosition = startPosition;

        girdArraySize = new Vector2(gridArray.GetLength(0), gridArray.GetLength(1));


        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = gridObjectType(this,x,y);
            }
        }
        //drawGridGizmos();


    }
    /// <summary>
    /// Metoda rysuje siekte jako gizmos
    /// </summary>
    public void drawGridGizmos()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                // debugGridTextArray[x,y] = CreateGridText(null, gridArray[x, y]?.ToString(), GetWorldPosition(x, y) + (new Vector3(cellSize,0,cellSize)/2f));
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white);

                
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white);
    }

    public void drawGridBlockedNodes(List<Vector2Int> indexes)
    {
        for (int n = 0; n < indexes.Count; n++)
        {
            int x = indexes[n].x;
            int y = indexes[n].y;
            Debug.DrawLine(new Vector3(GetWorldPosition(x, y).x - 0.5f, 0, GetWorldPosition(x, y).z - 0.5f) * cellSize + new Vector3(1, 0, 1) * cellSize * 0.5f, new Vector3(GetWorldPosition(x, y).x + 0.5f, 0, GetWorldPosition(x, y).z + 0.5f) * cellSize + new Vector3(1, 0, 1) * cellSize * 0.5f, Color.red);
            Debug.DrawLine(new Vector3(GetWorldPosition(x, y).x - 0.5f, 0, GetWorldPosition(x, y).z + 0.5f) * cellSize + new Vector3(1, 0, 1) * cellSize * 0.5f, new Vector3(GetWorldPosition(x, y).x + 0.5f, 0, GetWorldPosition(x, y).z - 0.5f) * cellSize + new Vector3(1, 0, 1) * cellSize * 0.5f, Color.red);
            //Gizmos.color = Color.red;
            //Gizmos.DrawCube(GetWorldPosition(x, y) + new Vector3(0.5f * CellSize, 0, 0.5f * CellSize), new Vector3(0.5f * CellSize, 0, 0.5f * CellSize));
            //EditorGUI.DrawRect(new Rect(GetWorldPosition(x, y), GetWorldPosition(x, y) + new Vector3(0.5f * CellSize, 0, 0.5f * CellSize)),Color.red);
        }


    }


    /// <summary>
    /// Metoda ustawia wyswietalnie sie tekstu kazdego wezła grida
    /// </summary>
    /// <param name="parent"> komponent za którym bedzie podazac tekst</param>
    /// <param name="text">jaki text ma sie wyswietlic</param>
    /// <param name="localPosition"> współrzędne textu </param>
    /// <returns></returns>
    private TextMesh CreateGridText(Transform parent, string text, Vector3 localPosition)
    {
        GameObject textMeshObject = new GameObject("GridText", typeof(TextMesh));
        
        Transform transform = textMeshObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.Rotate(90, 0, 0);

        TextMesh textMesh = textMeshObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = Color.white;
        textMesh.fontSize = 20;
        textMesh.alignment = TextAlignment.Left;

        return textMesh;
    }

    private Vector3 GetWorldPosition(int x ,int z )
    {
        return new Vector3(x, 0, z) * cellSize + startPosition;
    }

    public void SetObject(int x, int y, TGridObject value)
    {
        if (x >=0 && y >= 0 && y < height && x < width)
        {
            gridArray[x, y] = value;
            debugGridTextArray[x, y].text = gridArray[x, y]?.ToString();
        }
    }
    public void SetObject(Vector3 worldPosition , TGridObject value)
    {
        Vector2Int coordinate =  GetCoordinate(worldPosition);
        SetObject(coordinate.x, coordinate.y, value);
    }
    
    /// <summary>
    /// Metoda oblicza koordynaty grida na podstawie podanego wektora
    /// </summary>
    /// <param name="worldPosition"> vektor/pozycja z którego oblicznane sa koordynaty</param>
    /// <returns> zwraca vektor2 jako koordynaty grida </returns>
    public Vector2Int GetCoordinate(Vector3 worldPosition)
    {
        Vector2Int coordinate = new Vector2Int();

        coordinate.x = Mathf.FloorToInt((worldPosition - startPosition).x/ cellSize);
        coordinate.y = Mathf.FloorToInt((worldPosition - startPosition).z/ cellSize);

        //Debug.Log("y "+(worldPosition - startPosition).y);
        //Debug.Log("z "+(worldPosition - startPosition).z);
        return coordinate;
    }

    public TGridObject GetObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && y < height && x < width)
        {
            return gridArray[x,y];
        }
        else
        {
            return default(TGridObject);
        }
    }

}
