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
    [SerializeField] private StateType stateType;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Reset()
    {
        valueText = transform.Find("StateValue").GetComponent<TextMeshProUGUI>();
    }

    public void StateUpdate(Character player)
    {
        string equipValue = null;
        switch (stateType)
        {
            case StateType.Attack:
                valueText.text = player.totalAttack.ToString();
                if (player.equipAttack > 0)
                {
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
        valueText.text = valueText.text + (equipValue != null ? $" (+{equipValue})" : string.Empty);
    }
}
