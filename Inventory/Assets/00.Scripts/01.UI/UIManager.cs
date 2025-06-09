using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private UIMainMenu mainMenu;
    public UIMainMenu MainMenu => mainMenu;
    [SerializeField] private UIStatus status;
    public UIStatus Status => status;
    [SerializeField] private UIInventory inventory;
    public UIInventory Inventory => inventory;
    
    private void Reset()
    {
        mainMenu = FindAnyObjectByType<UIMainMenu>();
        status = FindAnyObjectByType<UIStatus>();
        inventory = FindAnyObjectByType<UIInventory>();
        
        status.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        status.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
    }
}
