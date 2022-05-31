using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MenuScript : MonoBehaviour
{
    public AudioMixer audiomixer;        

    public void PlayLevel(string name)
    {
        SceneManager.LoadScene(name);           //załaduj scenę nazwa
    }

    public void MasterVolume(float Master)
    {
        audiomixer.SetFloat("Master", Master);       //1 to nazwa mixera w dźwiękach, 2 to wartość slidera w opcjach
        Debug.Log(Master);
    }

    public void QuitGame()
    {
        Application.Quit();                     //no to zamknięcie apki
        Debug.Log("quit!");
    }
}
