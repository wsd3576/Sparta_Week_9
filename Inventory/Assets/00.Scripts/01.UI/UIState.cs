using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum StateType
{
    Attack,
    Defense,
    Health,
    Critical,
}

public class UIState : MonoBehaviour
{
    //스탯이 표기할 종류와 표기할 텍스트 받아오기
    [SerializeField] private StateType stateType;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Reset()
    {
        valueText = transform.Find("StateValue").GetComponent<TextMeshProUGUI>();
    }

    public void StateUpdate(Character player)
    {
        //장비 능력치 표기용 문자열 선언
        string equipValue = null;
        //각 종류별로 구분
        switch (stateType)
        {
            case StateType.Attack:
                //총합 능력치를 받아옴
                valueText.text = player.totalAttack.ToString();
                if (player.equipAttack > 0)
                {
                    //장비 능력치가 있다면 받아옴
                    equipValue = player.equipAttack.ToString();
                }
                break;
            case StateType.Defense:
                valueText.text = player.totalDefense.ToString();
                if (player.equipDefense > 0)
                {
                    equipValue = player.equipDefense.ToString();
                }
                break;
            case StateType.Health:
                valueText.text = player.totalHealth.ToString();
                if (player.equipHealth > 0)
                {
                    equipValue = player.equipHealth.ToString();
                }
                break;
            case StateType.Critical:
                valueText.text = player.totalCritical.ToString();
                if (player.equipCritical > 0)
                {
                    equipValue = player.equipCritical.ToString();
                }
                break;
        }
        //장비 능력치가 있다면 추가적으로 표기함
        valueText.text = valueText.text + (equipValue != null ? $" (+{equipValue})" : string.Empty);
    }
}
