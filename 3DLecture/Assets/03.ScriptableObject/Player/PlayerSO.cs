using System;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field : SerializeField] [field : Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field : SerializeField] [field : Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;
    
    [field : Header("IdleData")]

    [field : Header("WalkData")]
    [field : SerializeField] [field : Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;
    
    [field : Header("RunData")]
    [field : SerializeField] [field : Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}

[Serializable]
public class PlayerAirData
{
    [field : Header("JumpData")]
    [field : SerializeField] [field : Range(0f, 2f)] public float JumpForce { get; private set; } = 5f;
}

[CreateAssetMenu(fileName = "Player", menuName = "Character/Player")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private PlayerGroundData groundData = new PlayerGroundData();
    [SerializeField] private PlayerAirData airData = new PlayerAirData();

    public PlayerGroundData GroundData => groundData;
    public PlayerAirData AirData => airData;
}
