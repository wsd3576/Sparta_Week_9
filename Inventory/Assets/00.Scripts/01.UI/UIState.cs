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
        switch (stateType)
        {
            case StateType.Attack:
                valueText.text = player.attack.ToString();
                break;
            case StateType.Defense:
                valueText.text = player.defense.ToString();
                break;
            case StateType.Health:
                valueText.text = player.health.ToString();
                break;
            case StateType.Critical:
                valueText.text = player.critical.ToString();
                break;
        }
    }
}
