using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field : SerializeField] public EnemySO Data { get; private set; }
    [field: Header("Animation")]
    [field : SerializeField] public PlayerAnimationData AnimationData {get; private set;}

    [field : SerializeField] public Animator Animator {get; private set;}
    
    [field : SerializeField] public CharacterController Controller {get; private set;}
    [field : SerializeField] public ForceReciver ForceReciver {get; private set;}
    [field : SerializeField] public Weapon Weapon { get; private set; }
    
    private EnemyStateMachine stateMachine;

    private void Reset()
    {
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReciver = GetComponent<ForceReciver>();
        
        Weapon = GetComponentInChildren<Weapon>();
    }

    private void Awake()
    {
        AnimationData.Initialize();
        
        Weapon.SetAttack(Data.Damage, Data.Force);
        Weapon.gameObject.SetActive(false);
        
        stateMachine = new EnemyStateMachine(this);
    }
    
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
