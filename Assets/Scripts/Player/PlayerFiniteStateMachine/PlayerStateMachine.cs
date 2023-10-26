using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerSate CurrentState { get; private set; }
    
    public void OnInit(PlayerSate startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    
    public void ChangeState(PlayerSate newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        
        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.Enter();
        }
    }
}
