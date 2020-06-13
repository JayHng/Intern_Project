using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber) 
public class FiniteStateMachine
{
    public State currentState { get; private set;}

    public void Initialize(State startingState){
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(State newState){
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
