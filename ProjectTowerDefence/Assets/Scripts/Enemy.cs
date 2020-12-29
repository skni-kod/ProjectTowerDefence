using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float hp;
    protected float speed;
    protected int lvl;

    Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100f;

        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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

}
