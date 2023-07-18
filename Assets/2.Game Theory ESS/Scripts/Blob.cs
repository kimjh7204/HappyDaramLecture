using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlobState
{
    Idle,
    FoodSearching,
    Eating
}

public abstract class Blob : MonoBehaviour
{
    protected BlobState state = BlobState.Idle;
    private bool isStateChanged = true;
    
    void Start()
    {
        
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
