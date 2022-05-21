using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillbase : MonoBehaviour
{
   public float Smite_Skill(PlayerStats _stats)
    {
        float dmg = 0;
        //扣魔力
        //確認陣上是否有陣法
        //做動作
        //生成特效
        //確認爆擊
        //確認王的閃避
        //輸出一個傷害值
        return dmg;
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
public delegate int CoolSkill(PlayerStats _stats);
