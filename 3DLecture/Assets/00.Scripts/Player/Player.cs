using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field : SerializeField] public PlayerSO Data { get; private set; }
    [field: Header("Animation")]
    [field : SerializeField] public PlayerAnimationData AnimationData {get; private set;}

    [field : SerializeField] public Animator Animator {get; private set;}
    [field : SerializeField] public PlayerController Input {get; private set;}
    [field : SerializeField] public CharacterController Controller {get; private set;}
    [field : SerializeField] public ForceReciver ForceReciver {get; private set;}
    [field : SerializeField] public Health Health {get; private set;}
    
    private PlayerStateMachine stateMachine;

    private void Reset()
    {
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReciver = GetComponent<ForceReciver>();
        Health = GetComponent<Health>();
    }

    private void Awake()
    {
        AnimationData.Initialize();
        
        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnDie += OnDie;
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

    private void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }
}
