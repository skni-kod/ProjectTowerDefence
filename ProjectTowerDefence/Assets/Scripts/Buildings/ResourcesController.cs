using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using System;
/*
TODO: update surowców w gui, update surowców w programie, lista budynków do "ściągania" surowców,
zarządzanie listą budynków do surowca
*/
public class ResourcesController : MonoBehaviour
{
    
    [SerializeField] private List<Resource> resources = new List<Resource>();
    void Start()
    {
        
        for(int i=0; i< (int)Resource.ResourcesTypes.End; i++)
        {
            resources.Add(new Resource((uint)i));
        }
    }
    private void Update() {
        
    }
    public void AddResourcesAmount(List<Resource> res)
    {
        for(int i=0; i<res.Count; i++)
        {
            if(res[i].ResourceID < resources.Count){
                            //programowanie bezgałęziowe, zamiast if else if zostąło uzyte zwykłe mnożenie 
                bool condition = resources[(int)res[i].ResourceID].MaxAmmount == 0;
                resources[(int)res[i].ResourceID].amount += res[i].amount * Convert.ToInt32(condition) + 
                Mathf.Clamp(res[i].amount, -99999,   resources[(int)res[i].ResourceID].MaxAmmount-resources[(int)res[i].ResourceID].amount) * Convert.ToInt32(!condition);
            }
        }
        Debug.Log("resources added");
    }
    public void AddSingleResource(Resource res)
    {
        if(res.ResourceID < resources.Count)
        {
            //programowanie bezgałęziowe, zamiast if else if zostąło uzyte zwykłe mnożenie 
            bool condition = resources[(int)res.ResourceID].MaxAmmount == 0;
            resources[(int)res.ResourceID].amount += res.amount*Convert.ToInt32(condition) + 
            Mathf.Clamp(res.amount, -99999,   resources[(int)res.ResourceID].MaxAmmount-resources[(int)res.ResourceID].amount) * Convert.ToInt32(!condition);
            
        }
    }
    public ref List<Resource> GetResourcesRef() {
        return ref resources;
    } 
}

[System.Serializable]
/*
    Helper class to contain resource in other class
*/
public class Resource{
    public  enum ResourcesTypes {
        Gold = 0,
        Wood, 
        Supply,
        Mana,
        End
    }
    public string name;
    /*
    Tutaj wrzucasz wszystkie rzeczy które mają być w grze. Ostatni musi być End
    */
    public uint ResourceID;
    public float amount;
    //jezeli ta wartość jest równa 0, to maksymalna wartośc jest równa nieskończoność
    public int MaxAmmount;

    /*
    Konsxtruktor, gdzie wartość startowa będzie równa 0
    */
    public Resource(uint resourceID)
    {
        ResourceID = resourceID;
        amount = 0.0f;
        MaxAmmount = 0;
    }
    /*
    Konsturktor z wartością startową 
    */
    public Resource(uint resourceID, float startAmount)
    {
        ResourceID = resourceID;
        amount = startAmount;
        MaxAmmount = 0;
    }
    public Resource(uint resourceID, float startAmount, int maxAmmount)
    {
        ResourceID = resourceID;
        amount = startAmount;
        MaxAmmount = maxAmmount;
    }
}
