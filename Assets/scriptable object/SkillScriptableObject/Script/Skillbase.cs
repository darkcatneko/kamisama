using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skillbase : MonoBehaviour
{
    public List<UnityAction> SkillActionList;
    private void Awake()
    {
        SkillActionList.Insert(1, ()=> Smite_Skill()); 
        
    }
    public float Smite_Skill()
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

