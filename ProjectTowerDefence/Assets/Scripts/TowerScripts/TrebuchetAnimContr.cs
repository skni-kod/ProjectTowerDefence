using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TrebuchetAnimContr : MonoBehaviour
{
    public Assets.Scripts.Trebuchet controller;
    // Start is called before the first frame update
    private void Start(){

    }
    private void Update() {
        
    }
    private void ThrowRock(){
        Debug.Log("Gitara siema");
        controller.callFire();
    }
    private void ResetFire(){
        
        Debug.Log("Gitar Sevus");
        controller.setPerformFire(true);
    }
    public void setController(Trebuchet ctr) {
        controller = ctr;
    }
}
