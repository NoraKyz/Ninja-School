using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSate
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    
    protected float startTime;
    
    private string animBoolName;
    
    public PlayerSate(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        startTime = Time.time;
        player.Anim.SetBool(animBoolName, true);
        DoChecks();
    }
    
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);

    }
    
    public virtual void LogicUpdate()
    {
        
    }
    
    public virtual void PhysicsUpdate()
    {
        
    }
    
    public virtual void DoChecks()
    {
        
    }
}