using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float hp;
    protected float speed;
    protected int lvl;


    public Enemy()
    {

    }

    /// <summary>
    /// poruszanie sie postaci
    /// </summary>
    protected virtual void movement()
    {
        Debug.Log("coś poszło nie tak, Zjebałeś");
    }
    /// <summary>
    /// atak przeciwnika
    /// </summary>
    protected virtual void atack()
    {
        Debug.Log("coś poszło nie tak, Zjebałeś");
    }
    /// <summary>
    /// inicjalizacja statystyk
    /// </summary>
    protected virtual void initStats()
    {
        Debug.Log("coś poszło nie tak, Zjebałeś");
    }
    
}
