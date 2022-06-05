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
        //SkillActionList.Add(0, Test);
        SkillActionList.Add(1, Smite_Skill);
        SkillActionList.Add(2, Medicine_Skill);
        SkillActionList.Add(3, MagicArraySkill);
        SkillActionList.Add(4, GoastSkill);
        SkillActionList.Add(5, Needle_Skill);
        SkillActionList.Add(6, Punch_Skill);
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
            MainBattleSystem.instance.MinusMana(1);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]!=null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
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
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
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
    public void MagicArraySkill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "MagicArray"; //確認動作
            MainBattleSystem.instance.LastSkill = "MagicArray";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(3).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(3).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Kun)
            {
                MainBattleSystem.instance.BattleUseStats.Shield += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat * 0.1f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat * 0.1f);
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.Shield += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat * 0.1f*0.9f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat * 0.1f);
            }

            Debug.Log("BBBBBB");

        }

    }
    public void GoastSkill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Goast"; //確認動作
            MainBattleSystem.instance.LastSkill = "Goast";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(4).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(4).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Kan)
            {
                MainBattleSystem.instance.BattleUseStats.Shield += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.MaxHP * 0.2f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.MaxHP * 0.2f);
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.Shield += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.MaxHP * 0.2f*0.9f); 
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.MaxHP * 0.2f * 0.9f); 
            }
           

            Debug.Log("BBBBBB");

        }

    }
    public void Needle_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Needle"; //確認動作
            MainBattleSystem.instance.LastSkill = "Needle";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(5).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Qian)//輸出多個傷害值
            {
                if (MainBattleSystem.instance.CritOrNot == moonblocks.OneUpOneDown)
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.1f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 10f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                    Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                }
                else
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.005f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 1f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                    Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                }
            }
            else
            {
                if (MainBattleSystem.instance.CritOrNot == moonblocks.OneUpOneDown)
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.1f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 10f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                    Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                }
                else
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.005f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 1f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                    Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                }
            }

            Debug.Log("BBBBBB");

        }

    }
    public void Punch_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Punch"; //確認動作
            MainBattleSystem.instance.LastSkill = "Punch";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(6).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Zhen)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 1f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.03f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 3f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[1]);
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 1f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.03f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 3f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[1]);
            }
            MainBattleSystem.instance.ManaTired++;
            Debug.Log("BBBBBB");

        }
    }
    public void SheepSkill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Sheep"; //確認動作
            MainBattleSystem.instance.LastSkill = "Sheep";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(7).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(7).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Gen)
            {
                MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.1f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.1f);
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.1f * 0.9f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.1f * 0.9f);
            }
           

            Debug.Log("BBBBBB");

        }

    }
    public void RockHand_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "RockHand"; //確認動作
            MainBattleSystem.instance.LastSkill = "RockHand";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(8).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Zhen)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.3f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 15f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(GetDamage(0.3f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 15f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);

            }
            MainBattleSystem.instance.ManaTired++;
            Debug.Log("BBBBBB");

        }
    }
    public void SpringSkill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(7);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Spring"; //確認動作
            MainBattleSystem.instance.LastSkill = "Spring";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Kun)
            {
                MainBattleSystem.instance.BattleUseStats.Regen += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f);
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.Regen += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f*0.9f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f * 0.9f);
            }


            Debug.Log("BBBBBB");

        }

    }
    public void KingKongSkill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(5);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Spring"; //確認動作
            MainBattleSystem.instance.LastSkill = "Spring";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Kun)
            {
                MainBattleSystem.instance.BattleUseStats.Regen += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f);
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.Regen += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f * 0.9f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.INT.m_currentstat * 0.1f * 0.9f);
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

