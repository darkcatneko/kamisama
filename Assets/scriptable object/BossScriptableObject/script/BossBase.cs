using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossBase : ScriptableObject
{
    public int BossID;
    public GameObject BossGameObjectPrefab;
    public Sprite BossPicture;
    public List<PlayerStats> BossStats;
    public List<UnityAction> BossAttackSkill;
}
