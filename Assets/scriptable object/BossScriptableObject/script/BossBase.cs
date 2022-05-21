using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BossBase : ScriptableObject
{
    public int BossID;
    public GameObject BossGameObjectPrefab;
    public Sprite BossPicture;
    public List<PlayerStats> BossStats;
    public List<Action> BossAttackSkill;
}
