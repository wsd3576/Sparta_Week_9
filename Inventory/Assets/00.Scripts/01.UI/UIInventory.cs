using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usedSlotText;
    [SerializeField] private TextMeshProUGUI allSlotText;
    
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private List<UISlot> slotList = new List<UISlot>();
    [SerializeField] private Transform slotParent;

    [SerializeField] private int usedSlotCount = 0;
    [SerializeField] private int slotCount = 120;
    
    public void AddItem(Item item)
    {
        foreach (UISlot slot in slotList)
        {
            if (!slot.HasItem())
            {
                slot.SetItem(item);
                usedSlotCount++;
                return;
            }
        }
    }
    
    public void InitInventoryUI()
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
