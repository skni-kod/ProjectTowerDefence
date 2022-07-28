using UnityEngine;
using UnityEngine.AI;

public class NewPathFinding : MonoBehaviour
{
    /// <summary>
    /// aktualny indeks punktu, do którego zmierza przeciwnik
    /// </summary>
    protected int wavepointIndex = 0;

    /// <summary>
    /// cel, do którego zmierza przeciwnik
    /// </summary>
    private Transform target;

    protected NexusHealth nexusHealth;
    protected Enemy enemy;
    protected WayPoints wayPoints;
    protected LevelController levelController;

    NavMeshAgent agent;

    private void Start()
    {
        OnStartGet();

        //obranie pierwszego punktu za cel
        target = wayPoints.points[0];
        Debug.Log(GameObject.Find("Paths").gameObject.transform.GetChild(levelController.randomSpawnPointIndex));
    }

    private void Update()
    {
        if (!enemy.IsDead)
        {

            //dawny skrypt kierowania się do punktu
            /*Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.Speed * 10 * Time.deltaTime, Space.World);
            Vector3 rotateDir = Vector3.RotateTowards(transform.position, dir, 10f, 0f);
            transform.rotation = Quaternion.LookRotation(rotateDir);*/

            //kierowanie się do aktualnie wyznaczonego punktu
            agent.destination = target.position;
            if (Vector3.Distance(transform.position, target.position) <= 1f)
            {
                GetNextWaypoint();
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    private void GetNextWaypoint() //zmiana indeksu
    {
        if (wavepointIndex >= wayPoints.points.Length - 1) //czynność po dotarciu do ostatniego punktu
        {
            nexusHealth.Hit(20f);
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = wayPoints.points[wavepointIndex];
        
    }

    /// <summary>
    /// pobranie potrzebnych elementów
    /// </summary>
    private void OnStartGet()
    {
        levelController = GameObject.Find("LevelController").gameObject.GetComponent<LevelController>();
        wayPoints = GameObject.Find("Paths").gameObject.transform.GetChild(levelController.randomSpawnPointIndex).GetComponent<WayPoints>();
        nexusHealth = GameObject.FindWithTag("Nexus").gameObject.GetComponent<NexusHealth>();
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
    }
}
