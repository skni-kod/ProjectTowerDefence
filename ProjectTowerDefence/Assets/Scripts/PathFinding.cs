using UnityEngine;

public class PathFinding : MonoBehaviour
{
    //Do prawidłowego działania potrzebne jest jeszcze ustawienie skryptu WayPoints
    //Przeciwników można zrespić przyciskiem F

    public float speed = 10f; //prędkość poruszania

    private Transform target;
    private int wavepointIndex = 0; //indeks aktualnego punktu

    private void Start()
    {
        target = WayPoints.points[0];
    }

    private void Update()
    {
        //kierowanie się do wyznaczonego punktu
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() //zmiana indeksu
    {
        if (wavepointIndex >= WayPoints.points.Length - 1) //czynność po dotarciu do ostatniego punktu
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;

        target = WayPoints.points[wavepointIndex];
    } 
}
