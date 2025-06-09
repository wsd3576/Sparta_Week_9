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

    public void Initialize(string characterClass, string characterName, string description,int level, int curExp, int requiredExp, int money, int attack, int defense, int health, int critical)
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
    }
}
