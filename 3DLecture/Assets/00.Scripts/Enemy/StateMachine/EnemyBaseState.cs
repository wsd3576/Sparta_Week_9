using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine; 
        groundData = stateMachine.Enemy.Data.GroundData;
    }
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void PhysicsUpdate() { }
    
    public virtual void Update()
    {
        Move();
    }
    
    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Enemy.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Enemy.Animator.SetBool(animatorHash, false);
    }
    
    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        
        Move(movementDirection);

        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 direction = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position);
        return direction;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.Controller.Move(((direction * movementSpeed) + stateMachine.Enemy.ForceReciver.Movement) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Enemy.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }
    
    protected void ForceMove()
    {
        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.ForceReciver.Movement * Time.deltaTime);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

    protected bool IsPlayerDetected()
    {
        if (stateMachine.Target.IsDead) return false;
        
        float playerDistanceSqr =
            (
                stateMachine.Target.transform.position
                - stateMachine.Enemy.transform.position
            )
            .sqrMagnitude;
        return playerDistanceSqr <=
               (
                   stateMachine.Enemy.Data.PlayerChasingRange
                   * stateMachine.Enemy.Data.PlayerChasingRange
               );
    }
}
