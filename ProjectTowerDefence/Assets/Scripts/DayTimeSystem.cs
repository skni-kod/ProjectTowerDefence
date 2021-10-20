using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeSystem : MonoBehaviour
{
    // bool potrzebny do ustawiania dnia albo nocy 
    private bool set;
    private bool night;
    [Header("intensity in night")]
    [Range(0f,1f)]
    public float minIntensity;
    private float intensityCounterForWave;
    [Header("how fast change day or night , animation")]
    [Range(0.1f,1f)]
    public float howFastChangeDay;
    [Header("how much day or night have waves ")]
    public int howMuchDayHaveWaves;
    private int waveCounter; 
    private float intensityWhenTorchOn;
    private bool setTorch;
    private void Start()
    {
        intensityCounterForWave = (1f - minIntensity)/howMuchDayHaveWaves;
        intensityWhenTorchOn = (1 + minIntensity)/2 ;
        
        night = true;
    }
    // Update is called once per frame
    void Update()
    {
       if(FindObjectOfType<LevelController>().GetCurrentPhase==LevelController.levelPhase.preparations)//sprawdza czy jest nowa fala
        {
            set = true;
            waveCounter++;
            if (waveCounter > howMuchDayHaveWaves) 
            {
                waveCounter = 0;
                night = !night;
                setTorch = false;
            }
            
        }
        if (set) { SetDayOrNight(); }
    }
    void SetDayOrNight()//set light for day or night --------------------------------------------------------------------------------------------------------
    {
        
        if(night)
        {
            GetComponent<Light>().intensity = Mathf.Max(minIntensity +(howMuchDayHaveWaves- waveCounter) * intensityCounterForWave, GetComponent<Light>().intensity - howFastChangeDay * Time.deltaTime);
            if(GetComponent<Light>().intensity== minIntensity + (howMuchDayHaveWaves - waveCounter) * intensityCounterForWave)
            {
                set = false;
            }
            if (GetComponent<Light>().intensity < intensityWhenTorchOn && !setTorch) { setTorch = true;setTorches(true); }
        }
        else
        {
            GetComponent<Light>().intensity = Mathf.Min(1f - (howMuchDayHaveWaves-waveCounter) * intensityCounterForWave, GetComponent<Light>().intensity + howFastChangeDay * Time.deltaTime);
            if (GetComponent<Light>().intensity == 1f - (howMuchDayHaveWaves - waveCounter) * intensityCounterForWave)
            {
                set = false;
            }
            if (GetComponent<Light>().intensity > intensityWhenTorchOn && !setTorch) { setTorch = true; setTorches(false); }
        }
 
    }
    void setTorches(bool on)//set on or off torch light or camprire
    {
        foreach(var i in FindObjectsOfType<Light>())
        {
            if(i.gameObject!=gameObject) //check if it isn't main source of light or smth like that xd 
            {
                i.GetComponent<Light>().enabled = on;
            }
        }
    }
}
