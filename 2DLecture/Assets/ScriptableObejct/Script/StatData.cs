using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Speed,
    BulletCount,
}

[CreateAssetMenu(fileName = "New StatData", menuName = "Stats/Character Stats")]
public class StatData : ScriptableObject
{
    public string characterName;
    public List<StatEntry> stats;
}

[Serializable]
public class StatEntry
{
    public StatType statType;
    public float baseValue;
}
