using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    //캐릭터 정보 문자열
    public string characterClass { get; private set; }
    public string characterName  { get; private set; }
    public string description  { get; private set; }
    //캐릭터 레벨, 돈
    public int level { get; private set; } = 1;
    public int curExp { get; private set; } = 0;
    public int requiredExp { get; private set; } = 10;
    public int money  { get; private set; }
    //캐릭터 속성값 (기본속성, 장비속성, 통합속성)
    public int attack  { get; private set; }
    public int equipAttack { get; private set; }
    public int totalAttack => attack + equipAttack;
    public int defense  { get; private set; }
    public int equipDefense { get; private set; }
    public int totalDefense => defense + equipDefense;
    public int health  { get; private set; }
    public int equipHealth { get; private set; }
    public int totalHealth => health + equipHealth;
    public int critical  { get; private set; }
    public int equipCritical { get; private set; }
    public int totalCritical => critical + equipCritical;
    //플레이어 장비 저장용 값
    public Item weapon { get; private set; }
    public Item armor { get; private set; }
    public Item accessory { get; private set; }
    
    //테스트용으로 추가한 부분 ============================================
    public List<Item> randomItems;

    private void Start()
    {
        randomItems = new List<Item>()
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
        Item randomItem = randomItems[Random.Range(0, randomItems.Count)];
        Item addedItem = new Item(randomItem);
        UIManager.Instance.Inventory.AddItem(addedItem);
        Debug.Log($"{addedItem.itemName},{addedItem.itemType},{addedItem.itemValue}를 추가.");
    }
    
    public static ItemType GetRandomItemType()
    {
        Array values = Enum.GetValues(typeof(ItemType));
        return (ItemType)values.GetValue(Random.Range(0, values.Length));
    }
    //==================================================================
    
    //플레이어 정보 입력
    public void Initialize(string characterClass, string characterName, string description, int money, int attack, int defense, int health, int critical)
    {
        this.characterClass = characterClass;
        this.characterName = characterName;
        this.description = description;
        this.money = money;
        this.attack = attack;
        this.defense = defense;
        this.health = health;
        this.critical = critical;
    }
    
    //플레이어 아이템 획득(기존의 List<Item> Inventory는 추가만 하고 따로 활용은 안하기에 삭제)
    public void AddItem(Item item)
    {
        UIManager.Instance.Inventory.AddItem(item);
    }
    
    //플레이어 장비 장착
    public void EquipItem(Item item)
    {
        //아이템의 종류에 따라 분류
        switch (item.itemType)
        {
            case ItemType.Weapon:
                //이미 해당 장비를 장착한 상태라면 해제
                if (weapon != null)
                {
                    UnequipItem(weapon);
                }
                //장착하고 장비값 적용
                weapon = item;
                equipAttack = item.itemValue;
                break;
            case ItemType.Armor:
                if (armor != null)
                {
                    UnequipItem(armor);
                }
                armor = item;
                equipDefense = item.itemValue;
                break;
            case ItemType.Accessory:
                if (accessory != null)
                {
                    UnequipItem(accessory);
                }
                equipHealth = item.itemValue;
                accessory = item;
                break;
        }
        //해당 아이템 장착상태 바꾸고 해당 슬롯과 상태창 UI업데이트
        item.equiped = true;
        UIManager.Instance.Inventory.UpdateSlotUI(item);
        UIManager.Instance.Status.UpdateStateUI(this);
    }

    public void UnequipItem(Item item)
    {
        //아이템 종류 구분
        switch (item.itemType)
        {
            case ItemType.Weapon:
                //해제한 장비값 적용 후 해당 장비 빼기
                equipAttack = 0;
                weapon = null;
                break;
            case ItemType.Armor:
                equipDefense = 0;
                armor = null;
                break;
            case ItemType.Accessory:
                equipHealth = 0;
                accessory = null;
                break;
        }
        //해당 아이템 장착 상태 바꾸고 슬롯과 상태창 업데이트
        item.equiped = false;
        UIManager.Instance.Inventory.UpdateSlotUI(item);
        UIManager.Instance.Status.UpdateStateUI(this);
    }
}
