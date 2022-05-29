using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Threading.Tasks;

public class Skillbase : MonoBehaviour
{
    
    public Dictionary <int, UnityAction> SkillActionList = new Dictionary<int, UnityAction>();
    private void Awake()
    {
        SkillActionList.Add(0, ASSHOLE);
        SkillActionList.Add(1, Smite_Skill);
        SkillActionList.Add(2, Medicine_Skill);
    }
    private void Start()
    {
        
    }
    public UnityAction FindSkillFunction(int _skillid)
    {
        return SkillActionList[_skillid];
    }
    public void ASSHOLE()
    {
        Debug.Log("asshole");
    }
    public int GetDamage(float basic, float stat, float BaseDamage,float eight,float crit)
    {
        return Mathf.RoundToInt((Mathf.RoundToInt(basic * stat) + BaseDamage) * crit * eight);
    }
    public void Smite_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {            
            MainBattleSystem.instance.MinusMana(0);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]!=null)//確認陣上是否有陣法
            {
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Smite"; //確認動作
            MainBattleSystem.instance.LastSkill = "Smite";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(1).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Gen)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 2f, 1f, (float)MainBattleSystem.instance.CritOrNot*0.1f) );
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 2f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
            }

                Debug.Log("BBBBBB");
            
        }
              
    }
    public void Medicine_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(0);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Medicine"; //確認動作
            MainBattleSystem.instance.LastSkill = "Medicine";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(2).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Li)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 2f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 2f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
            }

            Debug.Log("BBBBBB");

        }

    }

}
[System.Serializable]
public enum EightTrigrams
{
    Qian,
    Xun,
    Kan,
    Gen,
    Kun,
    Zhen,
    Li,
    Dui,
}
[System.Serializable]
public enum SkillVariable
{
    Instance,
    Field,
}

