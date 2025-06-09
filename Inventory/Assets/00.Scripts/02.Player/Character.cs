using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public string characterClass { get; private set; }
    public string characterName  { get; private set; }
    public string description  { get; private set; }
    public int level { get; private set; } = 1;
    public int curExp { get; private set; } = 0;
    public int requiredExp { get; private set; } = 10;
    public int money  { get; private set; }
    
    public int attack  { get; private set; }
    public int equipAttack { get; private set; }

    public int totalAttack
    {
        get
        {
            return attack + equipAttack;
        }
    }
    public int defense  { get; private set; }
    public int equipDefense { get; private set; }

    public int totalDefense
    {
        get
        {
            return defense + equipDefense;
        }
    }
    public int health  { get; private set; }
    public int equipHealth { get; private set; }

    public int totalHealth
    {
        get
        {
            return health + equipHealth;
        }
    }
    public int critical  { get; private set; }
    public int equipCritical { get; private set; }

    public int totalCritical
    {
        get
        {
            return critical + equipCritical;
        }
    }

    public List<Item> inventory {get; private set;}
    //테스트용으로 추가한 부분 ============================================
    public List<Item> RandomItems;

    private void Start()
    {
        RandomItems = new List<Item>()
        {
            new Item("랜덤1", GetRandomItemType(), 1),
            new Item("랜덤2", GetRandomItemType(), 2),
            new Item("랜덤3", GetRandomItemType(), 3),
            new Item("랜덤4", GetRandomItemType(), 4),
            new Item("랜덤5", GetRandomItemType(), 5),
        };
    }
    
    public void AddRandomItem()
    {
        Item randomItem = RandomItems[Random.Range(0, RandomItems.Count)];
        Item addedItem = new Item(randomItem);
        UIManager.Instance.Inventory.AddItem(addedItem);
        inventory.Add(addedItem);
        Debug.Log($"{addedItem.itemName},{addedItem.itemType},{addedItem.itemValue}를 추가.");
    }
    //==================================================================
    
    public static ItemType GetRandomItemType()
    {
        Array values = Enum.GetValues(typeof(ItemType));
        return (ItemType)values.GetValue(Random.Range(0, values.Length));
    }

    public void Initialize(string characterClass, string characterName, string description, int money, int attack, int defense, int health, int critical, List<Item> inventory)
    {
        this.characterClass = characterClass;
        this.characterName = characterName;
        this.description = description;
        this.money = money;
        this.attack = attack;
        this.defense = defense;
        this.health = health;
        this.critical = critical;
        this.inventory = inventory;
    }

    public void EquipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                equipAttack += item.itemValue;
                break;
            case ItemType.Armor:
                equipDefense += item.itemValue;
                break;
            case ItemType.Accessory:
                equipHealth += item.itemValue;
                break;
        }

        UIManager.Instance.Status.UpdateStateUI(this);
    }

    public void UnequipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                equipAttack -= item.itemValue;
                break;
            case ItemType.Armor:
                equipDefense -= item.itemValue;
                break;
            case ItemType.Accessory:
                equipHealth -= item.itemValue;
                break;
        }
        
        UIManager.Instance.Status.UpdateStateUI(this);
    }
}
