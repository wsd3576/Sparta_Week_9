using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ItemData", menuName = "Items/Stat Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public List<StatEntry> stats;
    public bool isTemporary;
    public float duration;
}
