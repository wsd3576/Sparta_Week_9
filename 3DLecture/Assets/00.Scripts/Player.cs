using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Animation")]
    [field : SerializeField] public PlayerAnimationData AnimationData {get; private set;}

    public Animator Animator {get; private set;}
    public PlayerController Input {get; private set;}
    public CharacterController Controller {get; private set;}

    private void Reset()
    {
        Animator = GetComponent<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        AnimationData.Initialize();
    }

    private void Start()
    {
        
    }
}
