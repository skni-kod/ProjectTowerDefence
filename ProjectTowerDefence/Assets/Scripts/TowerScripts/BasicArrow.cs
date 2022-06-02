using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArrow : MonoBehaviour
{
    protected bool isPrimTargetAlive = true;
    protected GameObject primTarget;
    protected Vector3 secTarget;
    Rigidbody rb;
    float damage,mvSpeed;

    public void Init(float dmg,float mvs,Vector3 position, Quaternion rotation, GameObject primT)
    {
        transform.position = position;
        transform.rotation = rotation;
        damage = dmg;
        mvSpeed = mvs;

        rb = GetComponent<Rigidbody>();
        
        primTarget = primT;
        
        secTarget = primTarget.transform.position;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp1 =  Vector3.zero;
        if(primTarget)
        {
            //calculation of path Vector
            tmp1 = primTarget.transform.position+ new Vector3(0.0f, 0.7f, 0.0f) - transform.position;
            tmp1 = tmp1.normalized * mvSpeed;
            rb.velocity = (tmp1+rb.velocity)/2;
            secTarget = primTarget.transform.position;


        }
        else 
        {
            if((secTarget - transform.position).sqrMagnitude <=0.2f)
            {
                Destroy(gameObject);
            }
            rb.velocity = (secTarget - transform.position).normalized * mvSpeed;
            isPrimTargetAlive = false;
        }
        
    }
    void OnCollisionEnter(Collision other) {
        Debug.Log("Damage is done");
        other.gameObject.GetComponent<Enemy>().Hit(damage);
        Destroy(gameObject);
    }
}
