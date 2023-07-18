using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobDove : Blob
{
    public GameObject target;
    
    void Start()
    {
        
    }

    protected override void StateEnter()
    {
        switch (state)
        {
            case BlobState.Idle:
                break;
            case BlobState.FoodSearching:
                break;
            case BlobState.Eating:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void StateUpdate()
    {
        switch (state)
        {
            case BlobState.Idle:
                break;
            case BlobState.FoodSearching:
                var foodPos = ESSManager.instance.NearestFood(transform.position);
                target.transform.position = foodPos;
                
                
                
                
                
                break;
            case BlobState.Eating:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void StateExit()
    {
        switch (state)
        {
            case BlobState.Idle:
                break;
            case BlobState.FoodSearching:
                break;
            case BlobState.Eating:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override bool TransitionCheck()
    {
        if (state == BlobState.Idle)
        {
            state = BlobState.FoodSearching;
            return true;
        }
        
        return false;
    }
}
