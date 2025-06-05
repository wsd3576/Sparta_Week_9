using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInputs {get; private set;}
    public PlayerInputs.InGameActions inGameActions {get; private set;}
    
    void Start()
    {
        playerInputs = new PlayerInputs();
        inGameActions = playerInputs.InGame;
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
