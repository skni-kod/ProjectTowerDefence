using UnityEngine;

public class NewPathFinding : MonoBehaviour
{
    //Do prawidłowego działania potrzebne jest jeszcze ustawienie skryptu WayPoints
    //Przeciwników można zrespić przyciskiem F

    private Transform target;
    private int wavepointIndex = 0; //indeks aktualnego punktu
    protected NexusHealth nexusHealth;
    protected Enemy enemy;

    private void Start()
    {
        GameObject nexus = GameObject.FindWithTag("Nexus");
        nexusHealth = nexus.GetComponent<NexusHealth>();
        target = WayPoints.points[0];
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (!enemy.IsDead)
        {
            //kierowanie się do wyznaczonego punktu
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.Speed * 10 * Time.deltaTime, Space.World);
            Vector3 rotateDir = Vector3.RotateTowards(transform.position, dir, 10f, 0f);
            transform.rotation = Quaternion.LookRotation(rotateDir);

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }
        }
    }

    void GetNextWaypoint() //zmiana indeksu
    {
        if (wavepointIndex >= WayPoints.points.Length - 1) //czynność po dotarciu do ostatniego punktu
        {
            nexusHealth.Hit(20f);
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = WayPoints.points[wavepointIndex];
    } 
}
