using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour
{
    LineRenderer targetLine;
    Collider[] towerEnemiesInRange;
    float towerHitCooldown;

    void Start()
    {
        targetLine = GetComponent<LineRenderer>();
        targetLine.positionCount = 2;
        targetLine.SetPosition(0, GetComponent<Transform>().position);
        towerHitCooldown = GetComponent<Tower>().hitCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        updateTargetLine();
    }
    /// <summary>
    /// Rysowanie lini pomiędzy wieżą a a targetowanym przeciwnikiem
    /// </summary>
    private void updateTargetLine()
    {
        //Debug.Log(GetComponent<Tower>().enemiesInRange.Length);
        if (GetComponent<Tower>().enemiesInRange.Length > 0)
        {
            try
            {
                targetLine.enabled = true;
                towerEnemiesInRange = GetComponent<Tower>().enemiesInRange;
                targetLine.SetPosition(1, towerEnemiesInRange[0].GetComponent<Transform>().position);
            }
            catch (MissingReferenceException)
            {
                targetLine.enabled = false;
            }
            catch (System.Exception)
            {
                Debug.Log("coś sie zepsuło, prawdopodobnie obiek tostał zniszczony");
            }
        }
        else
        {
            targetLine.enabled = false;
        }
    }
}
