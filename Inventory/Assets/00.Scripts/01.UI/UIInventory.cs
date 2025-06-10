using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    //빈 슬록과 찬 슬롯 표기용 텍스트 받아오기
    [SerializeField] private TextMeshProUGUI usedSlotText;
    [SerializeField] private TextMeshProUGUI allSlotText;
    //슬롯 프리팹, 빈 슬롯 리스트, 차있는 스롯 저장할 딕셔너리(아이템에 따른 슬롯 찾기용), 슬롯을 생성할 부모 개체
    [SerializeField] private UISlot slotPrefab;
    private List<UISlot> emptySlots = new List<UISlot>();
    private Dictionary<Item, UISlot> Inventory = new Dictionary<Item, UISlot>();
    [SerializeField] private Transform slotParent;
    //차 있는 슬롯 표기용 과 슬롯 생성용 정수
    [SerializeField] private int usedSlotCount = 0;
    [SerializeField] private int slotCount = 120;
    //각 아이템별 아이콘 저장용
    public Sprite[] itemSprites;
    
    //인벤토리 초기 설정용 함수
    public void InitInventoryUI()
    {
        //프리팹의 높이를 재서 스크롤 길이에 적용
        float prefabHeight = slotPrefab.GetComponent<RectTransform>().rect.height;
        float slotHeight = (prefabHeight * (slotCount / 3)) + (10 * (slotCount / 3 - 1));
        
        RectTransform scrollHeight = slotParent.GetComponent<RectTransform>();
        
        scrollHeight.sizeDelta = new Vector2(scrollHeight.sizeDelta.x, slotHeight);
        //=====
        //슬롯 카운트 갯수만큼 슬롯 생성
        for (int i = 0; i < slotCount; i++)
        {
            UISlot slot = Instantiate(slotPrefab, slotParent);
            slot.SetItem(null);
            emptySlots.Add(slot);
        }
        //전체 슬롯 수 텍스트 적용
        allSlotText.text = " / " + slotCount.ToString();
    }
    
    //아이템 추가
    public void AddItem(Item item)
    {
        //빈 슬롯이 없다면 넘어감
        if (emptySlots.Count == 0)
        {
            Debug.LogWarning("빈 슬롯 없음");
            return;
        }
        //빈 슬롯중 첫 번째를 가져오고 해당 슬롯을 리스트에서 제거
        UISlot slot = emptySlots[0];
        emptySlots.Remove(slot);
        //해당 슬롯에 아이템 적용 후 딕셔너리에 추가
        slot.SetItem(item);
        Inventory[item] = slot;
        //차 있는 슬롯 카운트 증가 후 텍스트에 적용
        usedSlotCount++;
        usedSlotText.text = usedSlotCount.ToString();
    }
    
    //특정 슬롯 표시 업데이트용 함수
    public void UpdateSlotUI(Item itemData)
    {
        //해당 아템 데이터에서 슬롯을 찾아냄
        if (Inventory.TryGetValue(itemData, out UISlot slot))
        {
            //슬롯을 찾으면 해당슬롯 표시 업데이트
            slot.RefreshUI();
        }
    }
}
