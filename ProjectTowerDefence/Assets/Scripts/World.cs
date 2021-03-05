using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class World : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Grid<int> worldGrid = new Grid<int>(2, 5, 5f, new Vector3(0,0,0), () => new int());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
