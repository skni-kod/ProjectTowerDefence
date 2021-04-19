using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook
{
    public virtual float AreaRadius { get; }    
    public virtual Vector3 Position { get; set; }
    public virtual bool IsRunning { get; }

    public virtual void Run()
    {
        ;
    }

    public virtual void DealDMG()
    {
        ;
    }
}
