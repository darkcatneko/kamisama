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
    public void Smite_Skill()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {            
                MainBattleSystem.instance.MinusMana(5);//扣魔
                MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
                if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]!=null)//確認陣上是否有陣法
                {
                    Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//移除場地效果
                }
                MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Smite"; //確認動作
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1;
            //確認生成特效
            MainBattleSystem.instance.CritCheck();//確認爆擊
                //爆擊動畫
                //確認王的閃避
                //輸出多個傷害值

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

