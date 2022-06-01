using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnFieldDestroy : MonoBehaviour
{
    public UnityEvent OnDestroy = new UnityEvent();
    public int SelfDestroyCountDown = -1;
    public int StartTurn;
    public int EndTurn;
    public void CallDestroyTurn()
    {
        if (SelfDestroyCountDown != -1)
        {
            if (MainBattleSystem.instance.NowTurn == EndTurn)
            {
                OnDestroy.Invoke();
            }
        }
    }
    public void CallDestroy()
    {       
        OnDestroy.Invoke();
    }
    public void MagicArrayDestroy()
    {
        MainBattleSystem.instance.BattleUseStats.Shield -= Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat * 0.1f);
    }
    public void GoastDestroy()
    {
        MainBattleSystem.instance.BattleUseStats.Shield -= Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.MaxHP * 0.2f);
    }
}
