using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobDove : Blob
{
    void Start()
    {
        
    }

    protected override void StateEnter()
    {
        switch (state)
        {
            case BlobState.Idle:
                break;
            case BlobState.Wandering:
                wanderingTargetPos = transform.position; //타겟 포지션 초기화
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
            case BlobState.Wandering:

                if (Vector3.Distance(transform.position, wanderingTargetPos) < 0.1f)
                {
                    var randomCircle = Random.insideUnitCircle;
                    wanderingTargetPos = new Vector3(randomCircle.x, 0f, randomCircle.y) * WanderingRange;
                    agent.SetDestination(wanderingTargetPos);
                }

                FoodFinding();
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
            case BlobState.Wandering:
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
            state = BlobState.Wandering;
            return true;
        }
        
        return false;
    }
}
