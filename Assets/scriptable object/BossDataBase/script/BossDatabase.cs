using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BossData", menuName = "Boss/BossData")]
public class BossDatabase : ScriptableObject
{
    public List<BossBase> BossDataBase;

    public BossBase GetBossInformation(int _id)
    {
        for (int i = 0; i < BossDataBase.Count; i++)
        {
            if (BossDataBase[i].m_base.BossID == _id)
            {
                return BossDataBase[i];
            }
        }
        return null;
    }
}
