using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            if (MainBattleSystem.instance.BattleUseStats.Current_MP < 5)//確認魔力量
            {
                Debug.Log("白癡7414");
            }
            else
            {
                MainBattleSystem.instance.MinusMana(5);
                //確認陣上是否有陣法
                //做動作
                //生成特效
                //確認爆擊
                //確認王的閃避
                //輸出一個傷害值
                Debug.Log("BBBBBB");
            }
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

