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
    public Sprite itemSprite {get; private set;}
    public ItemType itemType {get; private set;}
    public int itemValue {get; private set;}
    public bool equiped { get; set; } = false;

    public Item(string itemName, ItemType itemType, int itemValue)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        switch (itemType)
        {
            case ItemType.Weapon:
                itemSprite = UIManager.Instance.Inventory.itemSprites[0];
                break;
            case ItemType.Armor:
                itemSprite = UIManager.Instance.Inventory.itemSprites[1];
                break;
            case ItemType.Accessory:
                itemSprite = UIManager.Instance.Inventory.itemSprites[2];
                break;
        }
        this.itemValue = itemValue;
    }
    
    public Item(Item other)
    {
        itemName = other.itemName;
        itemType = other.itemType;
        itemValue = other.itemValue;
        itemSprite = other.itemSprite;
        equiped = other.equiped;
    }
    
    public string GetItemInfo()
    {
        string result = $"{itemName},{itemType},{equiped},{itemValue}";
        return result;
    }
}
