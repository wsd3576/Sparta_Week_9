using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private GameObject equipIcon;
    //[SerializeField] private ItemData itemData;

    private void Reset()
    {
        icon = transform.Find("ItemIcon").GetComponent<Image>();
        equipIcon = transform.Find("EquipIcon").gameObject;
        equipIcon.SetActive(false);
    }
    
    // public void SetItem(ItemData itemData)
    // {
    //      this.itemData = itemData;
    //      icon.sprite = itemData.icon;
    // }

    public void RefreshUI()
    {
        
    }
}