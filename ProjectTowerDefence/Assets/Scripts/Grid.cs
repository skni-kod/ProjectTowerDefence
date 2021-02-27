using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;

    public Grid(int width,int height,float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                CreateGridText(null, gridArray[x, y].ToString(), GetWorldPosition(x, y) + (new Vector3(cellSize,0,cellSize)/2f));
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white,10f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 10f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 10f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width,height), Color.white, 10f);
    }
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

    private Vector3 GetWorldPosition(int x ,int y )
    {
        return new Vector3(x,0, y) * cellSize;
    }

}
