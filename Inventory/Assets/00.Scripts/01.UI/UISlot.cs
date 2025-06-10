using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    //각 슬롯에 들어갈 아이템 아이콘, 장착 표시 등등 받아옴
    [SerializeField] private Image icon;
    [SerializeField] private GameObject equipIcon;
    [SerializeField] private Item itemData;
    
    [SerializeField] private Button button;
    

    private void Reset()
    {
        //각 요소들 찾아옴
        icon = transform.Find("ItemIcon").GetComponent<Image>();
        equipIcon = transform.Find("EquipIcon").gameObject;
        button = GetComponent<Button>();
        equipIcon.SetActive(false);
    }

    //활성화 시 표시 업데이트 후 버튼에 기능 추가
    private void OnEnable()
    {
        RefreshUI();
        button.onClick.AddListener(UseItem);
    }

    //비활성화 시 버튼 기능 제거
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
    
    //아이템 사용
    private void UseItem()
    {
        //현 슬롯 아이템이 없으면 넘어감
        if (itemData == null) return;
        //현 슬롯 아이템 장착 상태를 구분
        switch (itemData.equiped)
        {
            case false: 
                //장착한 아이템이 아니면 장착
                GameManager.Instance.Player.EquipItem(itemData);
                break;
            case true :
                //장착한 아이템이라면 해제
                GameManager.Instance.Player.UnequipItem(itemData);
                break;
        }
    }
    
    //슬롯에 아이템 정보 적용
    public void SetItem(Item itemData = null)
    {
        //현 슬롯에 정보넣고 표기 업데이트
        this.itemData = itemData;
        RefreshUI();
    }

    //표기 업데이트
    public void RefreshUI()
    {
        //현 아이템이 없으면
        if (itemData == null)
        {
            //아이템 아이콘과 장착 아이콘 비활성화
            icon.enabled = false;
            equipIcon.SetActive(false);
        }
        else
        {
            //있다면 아이콘 활성화 후 아이콘 적용
            icon.enabled = true;
            icon.sprite = itemData.itemSprite;
            //장착 상태에 따라 장착 아이콘 표시
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