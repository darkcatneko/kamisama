using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaiwanOni", menuName = "Boss/DayBoss/TaiwanOni")]
public class TaiwanOni : BossBase
{
    private void Awake()
    {
        m_base.BossAttackSkill.Add(TaiwanOni_AttackPattern);
    }    
    public void TaiwanOni_AttackPattern()
    {
        int Pattern = 1;
        Pattern = Random.Range(1, 3);
        switch(Pattern)
        {
            case 1:
                return;
            case 2:
                return;
        }
    }
}
