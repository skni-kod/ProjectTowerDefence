using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
TODO: update surowców w gui, update surowców w programie, lista budynków do "ściągania" surowców,
zarządzanie listą budynków do surowca
*/
public class ResourcesController : MonoBehaviour
{
    
    List<Resource> resources = new List<Resource>();
    void Start()
    {
        
        for(int i=0; i< (int)Resource.ResourcesTypes.EndNumber; i++)
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
            if(res[i].ResourceID < resources.Count)
                resources[(int)res[i].ResourceID].amount += res[i].amount;
        }

        string tmp = resources.Count.ToString()+"\n";
        for(int i=0; i<resources.Count;i++)
        {
            tmp+= resources[i].ResourceID.ToString()+" : "+ resources[i].amount.ToString()+"\n";
        }

    }
    public void addSingleResource(Resource res)
    {
        if(res.ResourceID < resources.Count)
        {
            resources[(int)res.ResourceID].amount += res.amount;
        }
    }
}

[System.Serializable]
/*
    Helper class to contain resource in other class
*/
public class Resource{

    /*
    Tutaj wrzucasz wszystkie rzeczy które mają być w grze. Ostatni musi być EndNumber
    */
    public enum ResourcesTypes {
        Gold = 0,
        Wood, 
        Mana,
        EndNumber
    }
    public uint ResourceID;
    public float amount;
    public Resource(uint resourceID)
    {
        ResourceID = resourceID;
        amount = 0.0f;
    }
    public Resource(uint resourceID, float startAmount)
    {
        ResourceID = resourceID;
        amount = startAmount;
    }
}
