using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour
{
    Color32 activeColor, inactiveColor;

    LineRenderer targetLine;
    Collider[] towerEnemiesInRange;
    float towerHitCooldown;

    void Start()
    {
        targetLine = GetComponent<LineRenderer>();
        targetLine.positionCount = 2;
        targetLine.SetPosition(0, GetComponent<Transform>().position);
        towerHitCooldown = GetComponent<Tower>().hitCooldown;

        activeColor = new Color32(255, 0, 0, 255);
        inactiveColor = new Color32(128, 0, 0, 255);
        targetLine.startColor = inactiveColor;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargetLine();
    }
    /// <summary>
    /// Rysowanie lini pomiędzy wieżą a a targetowanym przeciwnikiem
    /// </summary>
    private void UpdateTargetLine()
    {
        //Debug.Log(GetComponent<Tower>().enemiesInRange.Length);
        if (GetComponent<Tower>().enemiesToHit.Length > 0)
        {
            try
            {
                targetLine.enabled = true;
                towerEnemiesInRange = GetComponent<Tower>().enemiesToHit;
                targetLine.SetPosition(1, towerEnemiesInRange[0].GetComponent<Transform>().position);

                if (Time.time - GetComponent<Tower>().lastHit < 0.25f) targetLine.endColor = activeColor;
                else targetLine.endColor = inactiveColor;
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
