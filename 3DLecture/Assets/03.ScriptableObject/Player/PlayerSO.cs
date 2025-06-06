using System;
using System.Collections.Generic;
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

[Serializable]
public class PlayerAttackData
{
    [field : Header("AttackData")]
    [field : SerializeField] public List<AttackInfoData> AttackInfoDatas {get; private set;}
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttacInfoData(int index) { return AttackInfoDatas[index]; }
}

[Serializable]
public class AttackInfoData
{
    [field : Header("JumpData")]
    [field : SerializeField] public string AttackName { get; private set; }
    [field : SerializeField] public int ComboStateIndex { get; private set; }
    [field : SerializeField][field : Range(0f,1f)] public float ComboTransitionTime { get; private set; }
    [field : SerializeField][field : Range(0f,3f)] public float ForceTransitionTime { get; private set; }
    [field : SerializeField][field : Range(-10f,10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;
    [field : SerializeField][field : Range(0f,1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field : SerializeField][field : Range(0f,1f)] public float Dealing_End_TransitionTime { get; private set; }
    
}

[CreateAssetMenu(fileName = "Player", menuName = "Character/Player")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private PlayerGroundData groundData = new PlayerGroundData();
    [SerializeField] private PlayerAirData airData = new PlayerAirData();
    [SerializeField] public PlayerAttackData attackData = new PlayerAttackData();

    public PlayerGroundData GroundData => groundData;
    public PlayerAirData AirData => airData;
    public PlayerAttackData AttackData => attackData;
}
