using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uIManager;
    public virtual void Init(UIManager uIManager)
    {
        this.uIManager = uIManager;
    }

    protected abstract UIState GetUIState();    //UI 종류 반환
    public abstract void UpdateUI();    //UI 업데이트 

    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}
