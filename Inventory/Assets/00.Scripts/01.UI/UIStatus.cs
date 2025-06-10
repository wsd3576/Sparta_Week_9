using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    //각 상태창 받아올 변수
    public UIState[] States;

    private void Reset()
    {
        //여러개의 스탯들을 받아오는 용 리셋
        Transform statesParent = transform.Find("StatusPanel/States");
        int count = statesParent.childCount;
        States = new UIState[count];
        
        for (int i = 0; i < States.Length; i++)
        {
            Transform child = statesParent.GetChild(i);
            States[i] = child.GetComponent<UIState>();
        }
    }
    
    //스텟 표기 업데이트
    public void UpdateStateUI(Character player)
    {
        //모든 스텟의 표기업데이트 함수 호출
        foreach (UIState state in States)
        {
            state.StateUpdate(player);
        }
    }
}
