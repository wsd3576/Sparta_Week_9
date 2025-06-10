using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance{get; private set;}
    
    //각 UI별 할당 함수
    [SerializeField] private UIMainMenu mainMenu;
    public UIMainMenu MainMenu => mainMenu;
    [SerializeField] private UIStatus status;
    public UIStatus Status => status;
    [SerializeField] private UIInventory inventory;
    public UIInventory Inventory => inventory;
    
    private void Reset()
    {
        //각 UI들 불러오기
        mainMenu = FindAnyObjectByType<UIMainMenu>(FindObjectsInactive.Include);
        status = FindAnyObjectByType<UIStatus>(FindObjectsInactive.Include);
        inventory = FindAnyObjectByType<UIInventory>(FindObjectsInactive.Include);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        //인벤토리 슬롯 구성 함수 호출 및 매인메뉴 열기
        inventory.InitInventoryUI();
        mainMenu.OpenMainMenu();
    }
}
