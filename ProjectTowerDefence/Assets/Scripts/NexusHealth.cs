using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NexusHealth : MonoBehaviour
{
    public float maxHp;
    protected float hp;
    protected BarController healthBar;
    protected UiController uiController;
    protected Text healthText;
    protected GameObject gameOver;


    void Start()
    {
        gameOver = GameObject.Find("GameOverButton");
        gameOver.SetActive(false);

        Time.timeScale = 1;
        hp = maxHp;
        HealthBarStart();
        HealthTextStart();
    }

    

    private void HealthBarStart()
    {
        uiController = GetComponent<UiController>();
        healthBar = transform.GetComponentInChildren<BarController>();
        healthBar.Initialize(16f, 6f);
        healthBar.SetValue(100 * hp / maxHp);
    }
    private void HealthTextStart()
    {
        healthText = GameObject.Find("HealthText").GetComponentInChildren<Text>();
        healthText.text = hp.ToString();
    }

    public bool Hit(float damageAmount)
    {

        hp -= damageAmount;
        healthBar.SetValue(100 * hp / maxHp);
        healthText.text = hp.ToString();
        if (hp <= 0)
        {

            //GameOver
            gameOver.SetActive(true);
            Time.timeScale = 0;
            return true;
        }
        // Aktualizacja paska zdrowia
        return false;
    }
}
