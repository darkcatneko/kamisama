using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillbase : MonoBehaviour
{
   public float Smite_Skill(PlayerStats _stats)
    {
        float dmg = 0;
        //���]�O
        //�T�{�}�W�O�_���}�k
        //���ʧ@
        //�ͦ��S��
        //�T�{�z��
        //�T�{�����{��
        //��X�@�Ӷˮ`��
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
