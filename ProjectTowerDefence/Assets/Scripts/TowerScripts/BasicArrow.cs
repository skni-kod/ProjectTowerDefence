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

    /// <summary>
    ///initiation method, need damage bullet does, speed of bullet, initial positon and roation and target
    /// </summary>
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
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        Vector3 tmp1 =  Vector3.zero;
        if(primTarget)
        {
            //calculation of path Vector
            tmp1 = primTarget.transform.position+ new Vector3(0.0f, 1.0f, 0.0f) - transform.position;
            tmp1 = tmp1.normalized * mvSpeed;
            rb.velocity = (tmp1+rb.velocity)/2;
            transform.rotation = Quaternion.FromToRotation(Vector3.left, rb.velocity);
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
    /// <summary>
    /// Do damage whe hit target
    /// </summary>
    void OnCollisionEnter(Collision other) {
        other.gameObject.GetComponent<Enemy>().Hit(damage);
        Destroy(gameObject);
    }
}
