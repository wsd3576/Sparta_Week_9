using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private GameObject equipIcon;
    [SerializeField] private Item itemData;
    [SerializeField] private bool isItemExist = false;
    
    [SerializeField] private Button button;
    

    private void Reset()
    {
        icon = transform.Find("ItemIcon").GetComponent<Image>();
        equipIcon = transform.Find("EquipIcon").gameObject;
        button = GetComponent<Button>();
        equipIcon.SetActive(false);
    }

    private void OnEnable()
    {
        RefreshUI();
        button.onClick.AddListener(UseItem);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void UseItem()
    {
        if (itemData == null) return;
        switch (itemData.equiped)
        {
            case false: 
                GameManager.Instance.Player.EquipItem(itemData);
                break;
            case true :
                GameManager.Instance.Player.UnequipItem(itemData);
                break;
        }
    }
    
    public void SetItem(Item itemData)
    {
        this.itemData = itemData;
        
        if (itemData == null)
        {
            isItemExist = false;
        }
        else
        {
            isItemExist = true;
        }
        
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (!isItemExist)
        {
            icon.enabled = false;
            equipIcon.SetActive(false);
        }
        else
        {
            icon.enabled = true;
            icon.sprite = itemData.itemSprite;
            
            switch (itemData.equiped)
            {
                case true:
                    equipIcon.SetActive(true);
                    break;
                case false:
                    equipIcon.SetActive(false);
                    break;
            }
        }
    }
}