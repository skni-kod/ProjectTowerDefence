using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTowerAnimCtr : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }
    //method called on animation event - on crystal 2 animation
    public void OnAnimationEnd()
    {
        anim.SetInteger("Crystal1", Random.Range(0,2));
        anim.SetInteger("Crystal2", Random.Range(0,2));
        anim.SetInteger("Crystal3", Random.Range(0,2));

    }
    //method called on animation event- on glowny animation
    public void OnAnimationEnd2()
    {
        Debug.Log("Its me mario");
        anim.SetInteger("Crystal1", Random.Range(0,2));
        anim.SetInteger("Crystal2", Random.Range(0,3));
        anim.SetInteger("Crystal3", Random.Range(0,3));
        anim.SetInteger("Glowny", Random.Range(0,3));
        anim.SetInteger("Okrag", Random.Range(0,4));

    }
}
