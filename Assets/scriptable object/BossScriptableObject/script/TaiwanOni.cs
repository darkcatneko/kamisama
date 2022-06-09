using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TaiwanOni", menuName = "Boss/DayBoss/TaiwanOni")]
public class TaiwanOni : BossBase
{
    private void Awake()
    {
        m_base.ChangeBossStats(Stats.HP, m_base.BossMaxHealth);
    }

    public  void TaiwanOni_AttackPattern()
    {
        int Pattern;
        Pattern = Random.Range(1, 3);
        switch(Pattern)
        {
            case 1: //¼²À»                
                m_base.BossDamageGen(Stats.ATK, 7, Stats.DEF);
                return;
            case 2: //¥¢¯«
                m_base.BossDamageGen(Stats.SPI, 7, Stats.POW);
                return;
        }
    }
}
