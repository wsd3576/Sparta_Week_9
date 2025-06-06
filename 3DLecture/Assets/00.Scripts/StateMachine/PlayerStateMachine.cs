using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public Vector2 MoveInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    
    public float JumpForce { get; set; }
    
    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }
    
    public Transform MainCameraTransform { get; set; }
    
    public PlayerIdleState IdleState { get;}
    public PlayerWalkState WalkState { get;}
    public PlayerRunState RunState { get;}
    public PlayerJumpState JumpState { get;}
    public PlayerFallState FallState { get;}
    public PlayerComboAttackState ComboAttackState { get; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        MainCameraTransform = Camera.main.transform;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        ComboAttackState = new PlayerComboAttackState(this);
        
        MovementSpeed = Player.Data.GroundData.BaseSpeed;
        RotationDamping = Player.Data.GroundData.BaseRotationDamping;
    }
}
