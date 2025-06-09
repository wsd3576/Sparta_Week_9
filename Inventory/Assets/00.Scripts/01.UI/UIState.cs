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
    public Action updateAction;

    private void Reset()
    {
        valueText = transform.Find("StateValue").GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        updateAction += StateUpdate;
    }

    private void StateUpdate()
    {
        switch (stateType)
        {
            case StateType.Attack:
                valueText.text = Character.Instance.Attack.ToString();
                break;
            case StateType.Defense:
                valueText.text = Character.Instance.Defense.ToString();
                break;
            case StateType.Health:
                valueText.text = Character.Instance.Health.ToString();
                break;
            case StateType.Critical:
                valueText.text = Character.Instance.Critical.ToString();
                break;
        }
    }
}
