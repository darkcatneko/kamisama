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
                MainBattleSystem.instance.MinusMana(5);//���]
                MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
                if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]!=null)//�T�{�}�W�O�_���}�k
                {
                    Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
                }
                MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Smite"; //�T�{�ʧ@
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1;
            //�T�{�ͦ��S��
            MainBattleSystem.instance.CritCheck();//�T�{�z��
                //�z���ʵe
                //�T�{�����{��
                //��X�h�Ӷˮ`��

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

