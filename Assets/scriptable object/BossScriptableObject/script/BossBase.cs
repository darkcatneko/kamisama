using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossBase : ScriptableObject
{
    public BossClass m_base;
    public void ChangeBossStats(Stats _stat,int end)
    {
        for (int i = 0; i < m_base.BossStats.Count; i++)
        {
            if (m_base.BossStats[i].m_stat == _stat)
            {
                m_base.BossStats[i].m_currentstat = end;
            }
        }
    }
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
}
