using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; }

    private Dictionary<StateType, int> states = new Dictionary<StateType, int>();

    [SerializeField] private int attack = 10;
    public int Attack => attack;
    [SerializeField] private int defense = 10;
    public int Defense => defense;
    [SerializeField] private int health = 10;
    public int Health => health;
    [SerializeField] private int critical = 10;
    public int Critical => critical;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
