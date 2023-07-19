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
        switch (curState)
        {
            case BlobState.Idle:
                break;
            case BlobState.Wandering:
                moveTargetPos = transform.position; //타겟 포지션 초기화
                ESSManager.instance.tickRate += FoodFinding;
                break;
            case BlobState.FoodTracing:
                moveTargetPos = foundFood.transform.position;
                agent.SetDestination(moveTargetPos);
                break;
            case BlobState.Eating:
                // if(foundFood != null)
                //     foundFood.BlobRegistration(this);
                foundFood?.BlobRegistration(this);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void StateUpdate()
    {
        switch (curState)
        {
            case BlobState.Idle:
                break;
            case BlobState.Wandering:

                if (Vector3.Distance(transform.position, moveTargetPos) < 0.1f)
                {
                    var randomCircle = Random.insideUnitCircle;
                    moveTargetPos = new Vector3(randomCircle.x, 0f, randomCircle.y) * WanderingRange;
                    agent.SetDestination(moveTargetPos);
                }
                
                break;
            case BlobState.FoodTracing:
                break;
            case BlobState.Eating:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void StateExit()
    {
        switch (curState)
        {
            case BlobState.Idle:
                break;
            case BlobState.Wandering:
                ESSManager.instance.tickRate -= FoodFinding;
                break;
            case BlobState.FoodTracing:
                break;
            case BlobState.Eating:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override bool TransitionCheck()
    {
        switch (curState)
        {
            case BlobState.Idle:
                nextState = BlobState.Wandering;
                return true;
            case BlobState.Wandering:
                if (foundFood != null)
                {
                    nextState = BlobState.FoodTracing;
                    return true;
                }
                break;
            case BlobState.FoodTracing:
                if (Vector3.Distance(transform.position, moveTargetPos) < 0.1f)
                {
                    nextState = BlobState.Eating;
                    return true;
                }
                break;
            case BlobState.Eating:
                if (foundFood == null)
                {
                    nextState = BlobState.Idle;
                }
                
                return true;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return false;
    }
}
