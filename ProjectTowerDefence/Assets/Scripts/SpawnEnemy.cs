using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    private Transform spawnPos;

    private void Start()
    {
        spawnPos = GetComponent<Transform>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(Enemy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
