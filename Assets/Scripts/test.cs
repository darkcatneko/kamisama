using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    
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

