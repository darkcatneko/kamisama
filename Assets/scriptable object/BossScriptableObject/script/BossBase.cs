using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossBase : ScriptableObject
{
    public BossClass m_base;
    
}
[System.Serializable]
public class BossClass
{
    public int BossID;
    public GameObject BossGameObjectPrefab;
    public Sprite BossPicture;
    public float BaseBlock;
    public List<PlayerStats> BossStats;
    public List<UnityAction> BossAttackSkill;
    public void ChangeBossStats(Stats _stat, int end)
    {
        for (int i = 0; i <BossStats.Count; i++)
        {
            if (BossStats[i].m_stat == _stat)
            {
                BossStats[i].m_currentstat = end;
            }
        }
    }
    public float FindStat(Stats _stat)
    {
        float y = 0;
        for (int i = 0; i < BossStats.Count; i++)
        {
            if (BossStats[i].m_stat == _stat)
            {
               y = BossStats[i].m_currentstat;
            }
        }
        return y;
    }
}
