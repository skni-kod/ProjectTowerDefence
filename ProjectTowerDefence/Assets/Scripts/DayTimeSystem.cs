using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeSystem : MonoBehaviour
{
    // bool potrzebny do ustawiania dnia albo nocy 
    private bool set;
    [Header("intensity w nocy")]
    [Range(0f,1f)]
    public float minIntensity;
    [Header("predkosc zmiany dnia na noc albo noc na dzien")]
    [Range(0.1f,1f)]
    public float predkoscZmianyDnia;



    // Update is called once per frame
    void Update()
    {
       if(FindObjectOfType<LevelController>().GetCurrentPhase==LevelController.levelPhase.preparations)//sprawdza czy jest nowa fala
        {
            set = true;

        }
        if (set) { SetDayOrNight(); }
    }
    void SetDayOrNight()//ustawia dzien albo noc w zaleznosci czy fala jest parzysta (nieparzyste fale sa w nocy a parzyste w dzien) --------------------------------------------------------------------------------------------------------
    {
        if(FindObjectOfType<LevelController>().GetCurrentWave%2==1)
        {
            GetComponent<Light>().intensity = Mathf.Max(minIntensity, GetComponent<Light>().intensity - predkoscZmianyDnia * Time.deltaTime);
            if (GetComponent<Light>().intensity == minIntensity) { set = false; ustawPochodnie(true); }
        }
        if (FindObjectOfType<LevelController>().GetCurrentWave % 2 == 0)
        {
            GetComponent<Light>().intensity = Mathf.Min(1f, GetComponent<Light>().intensity + predkoscZmianyDnia * Time.deltaTime);
            if (GetComponent<Light>().intensity == 1f) { set = false; ustawPochodnie(false); }
        }
    }
    void ustawPochodnie(bool on)//wlacza albo wylacza swiatlo pochodni albo ognisk
    {
        foreach(var i in FindObjectsOfType<Light>())
        {
            if(i.gameObject!=gameObject) //sprawdza czy to nie jest ogolne swiatlo 
            {
                i.GetComponent<Light>().enabled = on;
            }
        }
    }
}
