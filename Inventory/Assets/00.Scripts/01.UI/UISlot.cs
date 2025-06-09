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
    

    private void Reset()
    {
        icon = transform.Find("ItemIcon").GetComponent<Image>();
        equipIcon = transform.Find("EquipIcon").gameObject;
        equipIcon.SetActive(false);
    }

    private void OnEnable()
    {
        RefreshUI();
    }
    
    public bool HasItem()
    {
        return isItemExist;
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

    private void RefreshUI()
    {
        if (!isItemExist)
        {
            icon.enabled = false;
            equipIcon.SetActive(false);
        }
        else
        {
            icon.enabled = true;
            //아이템 아이콘 적용
            if (itemData.equiped)
            {
                equipIcon.SetActive(true);
            }
            else
            {
                equipIcon.SetActive(false);
            }
        }
    }
}