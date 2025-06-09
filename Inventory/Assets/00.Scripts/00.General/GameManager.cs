using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [field : SerializeField] public Character Player { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        SetData();
    }

    public void SetData()
    {
        Player = new GameObject("Player").AddComponent<Character>();

        Player.Initialize
        (
            "스탠다드",
            "전창민",
            "스탠다드반의 전창민이다.\n챌린지반에서 스탠다드가 되어 기분이 좋아보인다.\n'챌린지라는거 하나로 기대받는게 부담스러웠거든'",
            1,
            0,
            10,
            1000000,
            10,
            5,
            100,
            25
        );

        UIManager.Instance.MainMenu.UpdateMainMenuUI(Player);
        UIManager.Instance.Status.UpdateStateUI(Player);
    }
}
