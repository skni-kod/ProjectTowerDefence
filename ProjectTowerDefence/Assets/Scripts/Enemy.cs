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

    protected float hp;
    protected int lvl;
    protected float deltaTime;

    protected Rigidbody rigidbodyComponent;
    protected BarControl healthBar;

    // Start is called before the first frame update
    void Start()
    {

        deltaTime = GetEnemyDeltaTime();

        rigidbodyComponent = GetComponent<Rigidbody>();

        healthBar = transform.GetComponentInChildren<BarControl>();
        //healthBar.Initialize();

        hp = maxHp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement(GetEnemyDeltaTime());

        // Aktualizacja paska zdrowia
        healthBar.SetValue(100 * hp / maxHp);

        // Tymczasowe wyświetlanie ilości zdrowia dla ułatwienia testowania
        //Debug.Log(gameObject.name + "'s health: " + hp);
    }

    /// <summary>
    /// poruszanie sie postaci
    /// </summary>
    protected virtual void Movement(float deltaTime)
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
    protected virtual void InitStats()
    {
        Debug.Log("coś poszło nie tak, Zjebałeś");
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
        }
    }
    protected float GetEnemyDeltaTime()
    {
        return Time.deltaTime;
    }

}
