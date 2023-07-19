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
}
