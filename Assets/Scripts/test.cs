using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    private void Start()
    {
        this.transform.DOShakePosition(0.5f);
    }
    public void MathTest()
    {        
        for (int i = 0; i < 1; i++)
        {
            MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, Stats.ATK);
            MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, Stats.HP);                      
            MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, Stats.POW);
            MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, Stats.DEX);
            MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, Stats.INT);
            MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, Stats.DEF);
            MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, Stats.SPI);
        }
        
    }
}

