using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("PlayerInfos")] //플레이어 정보를 적을 텍스트 할당용 변수들
    [SerializeField] private TextMeshProUGUI playerClassText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerExpText;
    [SerializeField] private TextMeshProUGUI playerDescriptionText;
    //레벨 텍스트 넣을 때 쓸 문자열
    private string lv = "lv.";
    //플레이어 돈 텍스트
    [SerializeField] private TextMeshProUGUI playerMoneyText;
    
    [Header("Buttons")] //버튼들
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button backButton;

    private void Reset()
    {
        //각 이름과 타입에 맞춰 받아오기
        playerClassText = transform.Find("InfoPanel/PlayerClassImage/ClassNameText").GetComponent<TextMeshProUGUI>();
        playerNameText = transform.Find("InfoPanel/PlayerNameText").GetComponent<TextMeshProUGUI>();
        playerLevelText = transform.Find("InfoPanel/LevelText").GetComponent<TextMeshProUGUI>();
        playerExpText = transform.Find("InfoPanel/EXPBarImage/EXPText").GetComponent<TextMeshProUGUI>();
        playerDescriptionText = transform.Find("InfoPanel/Description").GetComponent<TextMeshProUGUI>();
        playerMoneyText = transform.Find("MoneyPanel/MoneyText").GetComponent<TextMeshProUGUI>();
        
        statusButton = transform.Find("StatusButton").GetComponent<Button>();
        inventoryButton = transform.Find("InventoryButton").GetComponent<Button>();
        backButton = transform.Find("BackButton").GetComponent<Button>();
    }

    private void Start()
    {
        //각 기능 버튼에 할당
        statusButton.onClick.AddListener(OpenStatus);
        inventoryButton.onClick.AddListener(OpenInventory);
        backButton.onClick.AddListener(OpenMainMenu);
    }

    public void UpdateMainMenuUI(Character player)
    {
        //받아온 정보 토대로 표시 변경
        playerClassText.text = player.characterClass;
        playerNameText.text = player.characterName;
        playerDescriptionText.text = player.description;
        //9이하일땐 09처럼 표시되도록 구성
        string level = (player.level < 10? "0" + player.level : player.level.ToString());
        playerLevelText.text = lv + level;
        string curExp = (player.curExp < 10? "0" + player.curExp : player.curExp.ToString());
        string requireExp = player.requiredExp.ToString();
        playerExpText.text = $"{curExp}/{requireExp}";
        //단위 나눈 돈 표시
        string unitMoney = GetUnitMoneyString(player.money);
        playerMoneyText.text = unitMoney;
    }

    //돈 단위 나누기
    private string GetUnitMoneyString(int rawMoney)
    {
        string unitMoney = rawMoney.ToString("N0");
        return unitMoney;
    }
    
    //버튼 토글용 함수
    private void SetButtons(bool isMainMenu = false)
    {
        statusButton.gameObject.SetActive(isMainMenu);
        inventoryButton.gameObject.SetActive(isMainMenu);
        backButton.gameObject.SetActive(!isMainMenu);
    }

    //돌아오기 버튼용
    public void OpenMainMenu()
    {
        SetButtons(true);
        UIManager.Instance.Status.gameObject.SetActive(false);
        UIManager.Instance.Inventory.gameObject.SetActive(false);
    }
    
    //상태창 버튼용
    public void OpenStatus()
    {
        SetButtons();
        UIManager.Instance.Status.gameObject.SetActive(true);
    }

    //인벤토리 버튼용
    public void OpenInventory()
    {
        SetButtons();
        UIManager.Instance.Inventory.gameObject.SetActive(true);
    }
}
