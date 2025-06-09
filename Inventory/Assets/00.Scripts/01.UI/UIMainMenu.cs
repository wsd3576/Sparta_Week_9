using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("PlayerInfos")]
    [SerializeField] private TextMeshProUGUI playerClassText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerExpText;
    [SerializeField] private TextMeshProUGUI playerDescriptionText;
    
    private string lv = "lv.";
    
    [SerializeField] private TextMeshProUGUI playerMoneyText;
    
    [Header("Buttons")]
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button backButton;

    private void Reset()
    {
        playerClassText = transform.Find("InfoPanel/PlayerClassImage/ClassNameText").GetComponent<TextMeshProUGUI>();
        playerNameText = transform.Find("InfoPanel/PlayerNameText").GetComponent<TextMeshProUGUI>();
        playerLevelText = transform.Find("InfoPanel/LevelText").GetComponent<TextMeshProUGUI>();
        playerExpText = transform.Find("InfoPanel/EXPBarImage/EXPText").GetComponent<TextMeshProUGUI>();
        playerDescriptionText = transform.Find("InfoPanel/Description").GetComponent<TextMeshProUGUI>();
        playerMoneyText = transform.Find("MoneyPanel/MoneyText").GetComponent<TextMeshProUGUI>();
        
        statusButton = transform.Find("Buttons/StatusButton").GetComponent<Button>();
        inventoryButton = transform.Find("Buttons/InventoryButton").GetComponent<Button>();
        backButton = transform.Find("Buttons/BackButton").GetComponent<Button>();
        
        backButton.gameObject.SetActive(false);
    }

    private void Awake()
    {
        statusButton.onClick.AddListener(OpenStatus);
        inventoryButton.onClick.AddListener(OpenInventory);
        backButton.onClick.AddListener(OpenMainMenu);
    }
    
    private void SetButtons(bool isMainMenu = false)
    {
        statusButton.gameObject.SetActive(isMainMenu);
        inventoryButton.gameObject.SetActive(isMainMenu);
        backButton.gameObject.SetActive(!isMainMenu);
    }

    public void OpenMainMenu()
    {
        SetButtons(true);
        UIManager.Instance.Status.gameObject.SetActive(false);
        UIManager.Instance.Inventory.gameObject.SetActive(false);
    }

    public void OpenStatus()
    {
        SetButtons();
        UIManager.Instance.Status.gameObject.SetActive(true);
    }

    public void OpenInventory()
    {
        SetButtons();
        UIManager.Instance.Inventory.gameObject.SetActive(true);
    }
}
