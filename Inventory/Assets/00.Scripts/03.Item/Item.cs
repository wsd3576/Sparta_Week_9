using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Accessory,
}

[Serializable]
public class Item
{ 
    public string itemName {get; private set;}
    public ItemType itemType {get; private set;}
    public bool equiped {get; private set;}
    public int itemValue {get; private set;}

    public Item(string itemName, ItemType itemType, int itemValue, bool equiped = false)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemValue = itemValue;
        this.equiped = equiped;
    }

    public string GetItemInfo()
    {
        string result = $"{itemName},{itemType},{equiped},{itemValue}";
        return result;
    }
}
