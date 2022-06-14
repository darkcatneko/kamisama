using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

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
    public List<GameObject> BossSkillPrefab;
    public int BossMaxHealth;
    public UnityEvent BossAttack;

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
    public void BossDamageGen(Stats BossAttackType,float SkillDamage, Stats DefenceType)
    {
        
        MainBattleSystem.instance.TempHP = MainBattleSystem.instance.BattleUseStats.Current_HP;
        PlayerHealthBarUpdate.instance.TempWhite = MainBattleSystem.instance.BattleUseStats.Current_HP;
        if (DefenceType ==  Stats.DEF)
        {            
            MainBattleSystem.instance.BattleUseStats.Current_HP -= Mathf.RoundToInt(FindStat(BossAttackType) * SkillDamage - (MainBattleSystem.instance.BattleUseStats.Shield + (MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat)));
            DOTween.To(() => { return MainBattleSystem.instance.TempHP; }, x => MainBattleSystem.instance.TempHP = x, MainBattleSystem.instance.BattleUseStats.Current_HP, 0.5f)
                .OnStepComplete(()=> 
                { 
                    DOTween.To(() => { return PlayerHealthBarUpdate.instance.TempWhite; }, x => PlayerHealthBarUpdate.instance.TempWhite = x, MainBattleSystem.instance.BattleUseStats.Current_HP, 1f); 
                });            
            
        }
        else if(DefenceType == Stats.POW)
        {
            MainBattleSystem.instance.BattleUseStats.Current_HP -= Mathf.RoundToInt(FindStat(BossAttackType) * SkillDamage - (MainBattleSystem.instance.BattleUseStats.Shield + (MainBattleSystem.instance.BattleUseStats.POW.m_currentstat)));
            //DOTween.To(() => { return MainBattleSystem.instance.TempHP; }, x => MainBattleSystem.instance.TempHP = x, MainBattleSystem.instance.BattleUseStats.Current_HP, 1f);
            //DOTween.To(() => { return PlayerHealthBarUpdate.instance.TempWhite; }, x => PlayerHealthBarUpdate.instance.TempWhite = x, MainBattleSystem.instance.BattleUseStats.Current_HP, 2f);
            DOTween.To(() => { return MainBattleSystem.instance.TempHP; }, x => MainBattleSystem.instance.TempHP = x, MainBattleSystem.instance.BattleUseStats.Current_HP, 0.5f)
                 .OnStepComplete(() =>
                 {
                     DOTween.To(() => { return PlayerHealthBarUpdate.instance.TempWhite; }, x => PlayerHealthBarUpdate.instance.TempWhite = x, MainBattleSystem.instance.BattleUseStats.Current_HP, 1f);
                 });
        }
       
    }
    public void CallBossDamage(int Damage)
    {
        BossHealthUpdate.instance.TempBossHealth =(int)MainBattleSystem.instance.ThisBoss.FindStat(Stats.HP);
        BossHealthUpdate.instance.TempWhite = (int)MainBattleSystem.instance.ThisBoss.FindStat(Stats.HP);
        ChangeBossStats(Stats.HP, Mathf.RoundToInt(FindStat(Stats.HP) - Damage));
        DOTween.To(() => { return BossHealthUpdate.instance.TempBossHealth; }, x => BossHealthUpdate.instance.TempBossHealth = x, MainBattleSystem.instance.ThisBoss.FindStat(Stats.HP), 0.5f).OnStepComplete
            (()=>
            {
                DOTween.To(() => { return BossHealthUpdate.instance.TempWhite; }, x => BossHealthUpdate.instance.TempWhite = x, MainBattleSystem.instance.ThisBoss.FindStat(Stats.HP), 0.7f);
            }
            );
        if (MainBattleSystem.instance.ThisBoss.FindStat(Stats.HP)<=0)
        {
            MainBattleSystem.instance.m_battleStatus = BattleStatus.BossDie;
            MainBattleSystem.instance.StartCoroutine("BossDie");//boss³Q±þ¦º
        }
    }
    public void Register( BossClass This,BossClass bb)
    {
        This.BossID = bb.BossID;
        This.BaseBlock = bb.BaseBlock;
        This.BossStats = new List<PlayerStats>();
        for (int i = 0; i < bb.BossStats.Count; i++)
        {
            this.BossStats.Add(new PlayerStats(bb.BossStats[i].m_stat, bb.BossStats[i].m_currentstat));
        }
        This.BossMaxHealth = bb.BossMaxHealth;
        This.BossAttack = bb.BossAttack;
    }
}

