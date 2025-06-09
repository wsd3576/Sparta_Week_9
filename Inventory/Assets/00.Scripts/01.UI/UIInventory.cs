using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usedSlotText;
    [SerializeField] private TextMeshProUGUI allSlotText;
    
    [SerializeField] private UISlot slotPrefab;
    private List<UISlot> emptySlots = new List<UISlot>();
    private Dictionary<Item, UISlot> itemSlots = new Dictionary<Item, UISlot>();
    [SerializeField] private Transform slotParent;

    [SerializeField] private int usedSlotCount = 0;
    [SerializeField] private int slotCount = 120;
    
    public Sprite[] itemSprites;
    
    public void AddItem(Item item)
    {
        if (emptySlots.Count == 0)
        {
            Debug.LogWarning("빈 슬롯 없음");
            return;
        }
        
        UISlot slot = emptySlots[0];
        emptySlots.Remove(slot);
        
        slot.SetItem(item);
        itemSlots[item] = slot;
        
        usedSlotCount++;
        usedSlotText.text = usedSlotCount.ToString();
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
            slot.SetItem(null);
            emptySlots.Add(slot);
        }
        
        allSlotText.text = "/ " + slotCount.ToString();
    }

    public void UpdateSlotUI(Item itemData)
    {
        if (itemSlots.TryGetValue(itemData, out UISlot slot))
        {
            slot.RefreshUI();
        }
    }
}
