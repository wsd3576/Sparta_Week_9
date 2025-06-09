using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usedSlotText;
    [SerializeField] private TextMeshProUGUI allSlotText;
    
    [SerializeField] private Transform slotParent;
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private List<UISlot> slotList = new List<UISlot>();

    [SerializeField] private int slotCount = 120;

    private void Start()
    {
        InitInventoryUI();
    }

    private void InitInventoryUI()
    {
        float prefabHeight = slotPrefab.GetComponent<RectTransform>().rect.height;
        float slotHeight = (prefabHeight * (slotCount / 3)) + (10 * (slotCount / 3 - 1));
        
        RectTransform scrollHeight = slotParent.GetComponent<RectTransform>();
        
        scrollHeight.sizeDelta = new Vector2(scrollHeight.sizeDelta.x, slotHeight);
        
        for (int i = 0; i < slotCount; i++)
        {
            UISlot slot = Instantiate(slotPrefab, slotParent);
            slotList.Add(slot);
        }
        
        allSlotText.text = "/ " + slotCount.ToString();
    }
}
