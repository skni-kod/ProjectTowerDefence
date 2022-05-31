using UnityEngine;

public class WayPoints : MonoBehaviour
{
    //Scena level_2 ma przykładowe ustawienie

    //ustawiamy na emptyobject
    //pozycje children będą punktami dla skrypthu pathfinding
    //pobranie children
    public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
            print(points[i].transform.position);
        }
    }
}
