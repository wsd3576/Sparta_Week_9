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
    public string itemName {get; private set;} //아이템 이름값. 따로 UI에 표시되지는 않음(추가시 디버그로 확인용)
	//아이템 아이콘, 타입, 장착시 적용값, 장착상태
    public Sprite itemSprite {get; private set;}
    public ItemType itemType {get; private set;}
    public int itemValue {get; private set;}
    public bool equiped { get; set; } = false;

	//아이템 생성자(이름, 타입, 적용값만 받아옴)
    public Item(string itemName, ItemType itemType, int itemValue)
    {
		//이름, 타입, 값을 받아옴
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemValue = itemValue;
		//받아온 타입에 따라 아이콘 적용
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
    }
    
	//복제용 생성자
    public Item(Item other)
    {
        itemName = other.itemName;
        itemType = other.itemType;
        itemValue = other.itemValue;
        itemSprite = other.itemSprite;
        equiped = other.equiped;
    }
}
