using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
	//기존 스탯 머신의 함수는 그대로 사용하며 피요한 부분만 가져옴.
    public Player Player { get; } //스탯머신을 플레이어에서 직접 생성하며 집어넣음
    public Vector2 MoveInput { get; set; } //플레이어 입력값 PlayerBaseState 97번째에서 할당
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    
    public float JumpForce { get; set; }
    
    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }
    
    public Transform MainCameraTransform { get; set; }
    //각 상태들을 저장
    public PlayerIdleState IdleState { get;}
    public PlayerWalkState WalkState { get;}
    public PlayerRunState RunState { get;}
    public PlayerJumpState JumpState { get;}
    public PlayerFallState FallState { get;}
    public PlayerComboAttackState ComboAttackState { get; }
    
    //플레이어에서 생성하고 할당
    public PlayerStateMachine(Player Player)
    {
        this.Player = Player;
        MainCameraTransform = Camera.main.transform;
        //각 상태들을 새로 생성
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
