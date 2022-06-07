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
        SkillActionList.Add(7, Sheep_Skill);
        SkillActionList.Add(8, RockHand_Skill);
        SkillActionList.Add(9, Spring_Skill);
        SkillActionList.Add(10, KingKong_Skill);
        SkillActionList.Add(11, Moon_Skill);
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Gen)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber( GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 2f, 1f, (float)MainBattleSystem.instance.CritOrNot*0.1f),DamageType.Physics ));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 2f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Li)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 2f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f),DamageType.Magic));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 2f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Magic));
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Qian)//輸出多個傷害值
            {
                if (MainBattleSystem.instance.CritOrNot == moonblocks.OneUpOneDown)
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.1f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 10f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f),DamageType.Physics));
                    Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                }
                else
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.005f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 1f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                    Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                }
            }
            else
            {
                if (MainBattleSystem.instance.CritOrNot == moonblocks.OneUpOneDown)
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.1f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 10f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                    Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                }
                else
                {
                    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.005f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 1f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Zhen)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 1f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f),DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.03f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 3f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[1]);
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 1f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.03f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 3f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[1]);
            }
            MainBattleSystem.instance.ManaTired++;
            Debug.Log("BBBBBB");

        }
    }
    public void Sheep_Skill()
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Zhen)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.3f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 15f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f),DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.3f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 15f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);

            }
            MainBattleSystem.instance.ManaTired++;
            Debug.Log("BBBBBB");

        }
    }
    public void Spring_Skill()
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
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
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
    public void KingKong_Skill()
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
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "KingKong"; //確認動作
            MainBattleSystem.instance.LastSkill = "KingKong";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(10).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(10).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Gen)
            {
                MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.5f);
                MainBattleSystem.instance.Buffs.Add(new PlayerBuff(Stats.DEF, Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.5f),MainBattleSystem.instance.NowTurn, MainBattleSystem.instance.NowTurn+1));
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.5f*0.9f);
                MainBattleSystem.instance.Buffs.Add(new PlayerBuff(Stats.DEF, Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat * 0.5f * 0.9f), MainBattleSystem.instance.NowTurn, MainBattleSystem.instance.NowTurn + 1));
            }


            Debug.Log("BBBBBB");

        }

    }
    public void Moon_Skill()
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
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Moon"; //確認動作
            MainBattleSystem.instance.LastSkill = "Moon";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(11).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Dui)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 5f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f),DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.03f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 8f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[1]);
            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.02f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 5f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.03f, MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat, 8f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[1]);
            }
            MainBattleSystem.instance.ManaTired++;
            Debug.Log("BBBBBB");

        }
    }
    public void Wind_Skill()
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
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Wind"; //確認動作
            MainBattleSystem.instance.LastSkill = "Wind";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(12).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(12).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Kan)
            {
                MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEX.m_currentstat * 0.2f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEX.m_currentstat * 0.2f);
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEX.m_currentstat * 0.2f * 0.9f);
                MainBattleSystem.instance.BuffAmount = Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.DEX.m_currentstat * 0.2f * 0.9f);
            }


            Debug.Log("BBBBBB");

        }

    }
    public void SunSet_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(8);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "SunSet"; //確認動作
            MainBattleSystem.instance.LastSkill = "SunSet";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(13).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Qian)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.3f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 30f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);

            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.3f, MainBattleSystem.instance.BattleUseStats.ATK.m_currentstat, 30f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);

            }
            MainBattleSystem.instance.ManaTired++;
            Debug.Log("BBBBBB");

        }
    }
    public void WeatherTurn_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            MainBattleSystem.instance.MinusMana(10);//扣魔
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//確認陣上是否有陣法
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "WeatherTurn"; //確認動作
            MainBattleSystem.instance.LastSkill = "WeatherTurn";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(14).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
            //爆擊動畫
            //確認王的閃避
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();

            int FieldCount = 0;
            for (int i = 0; i < MainBattleSystem.instance.FieldSkills.Length; i++)
            {
                if (MainBattleSystem.instance.FieldSkills[i]!=null)
                {
                    FieldCount++;
                }
            }

            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Xun)//輸出多個傷害值
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.1f*FieldCount, MainBattleSystem.instance.BattleUseStats.DEX.m_currentstat, 5f, 1f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);

            }
            else
            {
                MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(new DamageNumber(GetDamage(0.1f*FieldCount, MainBattleSystem.instance.BattleUseStats.DEX.m_currentstat, 5f, 0.9f, (float)MainBattleSystem.instance.CritOrNot * 0.1f), DamageType.Physics));
                Debug.Log(MainBattleSystem.instance.battleAnimationContents.DamageDelt[0]);

            }
            MainBattleSystem.instance.ManaTired++;
            Debug.Log("BBBBBB");

        }
    }
    public void Get_Skill()
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
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Get"; //確認動作
            MainBattleSystem.instance.LastSkill = "Get";//上一召紀錄使用
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//確認動作時長
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(15).AnimationPrefab;//確認生成特效
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(15).FieldPrefab;//確認生成場地            
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<DamageNumber>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Dui)
            {
                MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.POW.m_currentstat);
                MainBattleSystem.instance.Buffs.Add(new PlayerBuff(Stats.SPI, Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.POW.m_currentstat), MainBattleSystem.instance.NowTurn, MainBattleSystem.instance.NowTurn + 1));
                MainBattleSystem.instance.Buffs.Add(new PlayerBuff(Stats.POW, Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.POW.m_currentstat*-1f), MainBattleSystem.instance.NowTurn, MainBattleSystem.instance.NowTurn + 1));
                MainBattleSystem.instance.BattleUseStats.POW.m_currentstat = 0;
            }
            else
            {
                MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat += Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.POW.m_currentstat*0.9f);
                MainBattleSystem.instance.Buffs.Add(new PlayerBuff(Stats.SPI, Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.POW.m_currentstat * 0.9f), MainBattleSystem.instance.NowTurn, MainBattleSystem.instance.NowTurn + 1));
                MainBattleSystem.instance.Buffs.Add(new PlayerBuff(Stats.POW, Mathf.RoundToInt(MainBattleSystem.instance.BattleUseStats.POW.m_currentstat * -1f), MainBattleSystem.instance.NowTurn, MainBattleSystem.instance.NowTurn + 1));
                MainBattleSystem.instance.BattleUseStats.POW.m_currentstat = 0;
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
[System.Serializable]
public enum DamageType
{
    Physics,
    Magic,
}

