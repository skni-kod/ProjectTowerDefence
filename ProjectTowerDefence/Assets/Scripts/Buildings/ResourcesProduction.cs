using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
put into object to make production building
*/
public class ResourcesProduction : MonoBehaviour {
    [SerializeField] float timeToProduction;
    [SerializeField] List<Resource> produced_Resources;
    [SerializeField] ResourcesController owner;

    
    void Start()
    {
        if(owner!=null)
        {
            StartCoroutine(ProduceResource());
        }
    }
    void Update()
    {
        
    }
    IEnumerator ProduceResource(){
        yield return new WaitForSecondsRealtime(timeToProduction);
        if(owner != null)
        {
            
            StartCoroutine(ProduceResource());
            owner.AddResourcesAmount(produced_Resources);
        }
    }
    void OnReleaseOwner(){
        owner = null;
    }
    void OnChangeTeam(ResourcesController newOwner){
        owner = newOwner;
        StartCoroutine(ProduceResource());
    }
}