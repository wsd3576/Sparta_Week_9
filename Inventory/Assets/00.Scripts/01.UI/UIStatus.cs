using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    public UIState[] States;

    private void Reset()
    {
        Transform statesParent = transform.Find("StatusPanel/States");
        int count = statesParent.childCount;
        States = new UIState[count];
        
        for (int i = 0; i < States.Length; i++)
        {
            Transform child = statesParent.GetChild(i);
            States[i] = child.GetComponent<UIState>();
        }
    }

    public void UpdateStates()
    {
        foreach (UIState state in States)
        {
            state.updateAction?.Invoke();
        }
    }
}
