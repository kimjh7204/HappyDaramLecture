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

    protected int energy = 50;
    
    private const float Scaler = 0.01f;
    private const float MinimumScale = 0.5f;
    
    protected BlobState curState = BlobState.Idle;
    protected BlobState nextState = BlobState.Idle;

    protected Vector3 moveTargetPos;
    protected const float WanderingRange = 10f;

    private bool isStateChanged = true;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        curState = nextState;
        
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

    public void EatFood()
    {
        energy++;
        transform.localScale = Vector3.one * (energy * Scaler + MinimumScale);
    }

    public void FinishEating()
    {
        foundFood = null;
    }
}
