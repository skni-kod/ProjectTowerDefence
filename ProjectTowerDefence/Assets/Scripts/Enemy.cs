using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float maxHp;

    [SerializeField]
    [Range(0,100)]
    protected float speed;
    protected List<Vector3> pathVectorList;
    protected int currentPathIndex;

    protected float hp;
    protected int lvl;

    protected Rigidbody rigidbodyComponent;
    protected BarController healthBar;

    protected Pathfinding pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = new Pathfinding(100, 100);
        SetDestinationPosition(new Vector3(33, 0, 7));


        rigidbodyComponent = GetComponent<Rigidbody>();

        healthBar = transform.GetComponentInChildren<BarController>();
        //healthBar.Initialize();

        hp = maxHp;

        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();

        // Tymczasowe wyświetlanie ilości zdrowia dla ułatwienia testowania
        //Debug.Log(gameObject.name + "'s health: " + hp);
    }

    /// <summary>
    /// poruszanie sie postaci
    /// </summary>
    protected virtual void Movement()
    {
        // Tymczasowe rozwiązanie, aby obiekt się poruszał
        rigidbodyComponent.velocity = Vector3.right;
    }

    /// <summary>
    /// atak przeciwnika
    /// </summary>
    protected virtual void Atack()
    {
        Debug.Log("coś poszło nie tak, Zjebałeś");
    }

    /// <summary>
    /// inicjalizacja statystyk
    /// </summary>
    /// <param name="maxHp">Ilość punktów zdrowia</param>
    /// <param name="speed">Szybkość</param>
    public void InitStats(float maxHp, float speed)
    {
        this.maxHp = maxHp;
        this.speed = speed;
    }

    /// <summary>
    /// Zadawanie obrażeń przeciwnikowi
    /// </summary>
    /// <param name="damageAmount">Ilość obrażeń</param>
    public void Hit(float damageAmount)
    {
        hp -= damageAmount;
        if (hp <= 0)
        {
            // Usunięcie obiektu, bo umarł
            Destroy(gameObject);
            return;
        }
        // Aktualizacja paska zdrowia
        healthBar.SetValue(100 * hp / maxHp);
    }

    /**
     * metoda ustawia punkt docelowy sciezki
     * @param targetPosition Vector3 współrzędnych punktu docelowego
     */
    protected void SetDestinationPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        //pathVectorList = Pathfinding.Instance.Path(transform.position, targetPosition);
        pathVectorList = pathfinding.Path(transform.position, targetPosition);
        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    /**
     * metoda zwraca współrzedne X oraz Z obiektu
     * @return Vektor3 pozycji obiektu o współrzędnej Y równej 0
     */
    protected Vector3 GetPositionXZ()
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

}
