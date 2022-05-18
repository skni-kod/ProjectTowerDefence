using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : SpellBook
{
    private float areaRadius = 5f;
    private Vector3 position;
    private bool isRunning = false;

    public override void Run()
    {
        //wykonanie zaklecia
    }

    public override void DealDMG()
    {

    }

    public override float AreaRadius
    {
        get
        {
            return areaRadius;
        }
    }
    public override Vector3 Position 
    { 
        get
        {
            return position;
        }
        set
        {
            position = value;
        }
    }
    public override bool IsRunning
    {
        get
        {
            return isRunning;
        }
    }
}
