using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpellLogic : MonoBehaviour
{
    void Explode()
    {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, exp.main.duration);
    }

    void OnCollisionEnter(Collision coll)
    {        
        Explode();
    }
}
