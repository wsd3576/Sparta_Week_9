using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterClass { get; private set; }
    public string characterName  { get; private set; }
    public string description  { get; private set; }
    public int level  { get; private set; }
    public int curExp  { get; private set; }
    public int requiredExp  { get; private set; }
    public int money  { get; private set; }
    
    public int attack  { get; private set; }
    public int defense  { get; private set; }
    public int health  { get; private set; }
    public int critical  { get; private set; }

    public List<Item> inventory {get; private set;}

    public void Initialize(string characterClass, string characterName, string description,int level, int curExp, int requiredExp, int money, int attack, int defense, int health, int critical, List<Item> inventory)
    {
        this.characterClass = characterClass;
        this.characterName = characterName;
        this.description = description;
        this.level = level;
        this.curExp = curExp;
        this.requiredExp = requiredExp;
        this.money = money;
        this.attack = attack;
        this.defense = defense;
        this.health = health;
        this.critical = critical;
        this.inventory = inventory;
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public void EquipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                attack += item.itemValue;
                break;
            case ItemType.Armor:
                defense += item.itemValue;
                break;
            case ItemType.Accessory:
                health += item.itemValue;
                break;
        }
    }

    public void UnequipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                attack -= item.itemValue;
                break;
            case ItemType.Armor:
                defense -= item.itemValue;
                break;
            case ItemType.Accessory:
                health -= item.itemValue;
                break;
        }
    }

    public void InventoryCheck()
    {
        foreach (var item in inventory)
        {
            Debug.Log(item.GetItemInfo());
        }
    }
}
