using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BlobState
{
    Idle,
    Wandering,
    FoodTracing,
    Eating
}

public abstract class Blob : MonoBehaviour
{
    protected NavMeshAgent agent;

    protected Food foundFood;
    
    protected BlobState state = BlobState.Idle;

    protected Vector3 wanderingTargetPos;
    protected const float WanderingRange = 5f;

    private bool isStateChanged = true;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(isStateChanged) StateEnter();
        isStateChanged = false;
        
        StateUpdate();
    
        isStateChanged = TransitionCheck();
        
        if(isStateChanged) StateExit();
    }

    protected abstract void StateEnter();
    protected abstract void StateUpdate();
    protected abstract void StateExit();

    protected abstract bool TransitionCheck();

    protected void FoodFinding()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            WanderingRange,
            Vector3.up,
            0f, 1<<3);

        if (hits.Length > 0)
            foundFood = hits[0].transform.GetComponent<Food>();
        else
            foundFood = null;
    }
}
