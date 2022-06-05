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
            MainBattleSystem.instance.MinusMana(1);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]!=null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Smite"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Smite";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(1).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.CritCheck();//�T�{�z��
            //�z���ʵe
            //�T�{�����{��
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Gen)//��X�h�Ӷˮ`��
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Medicine"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Medicine";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(2).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.CritCheck();//�T�{�z��
            //�z���ʵe
            //�T�{�����{��
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Li)//��X�h�Ӷˮ`��
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "MagicArray"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "MagicArray";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(3).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(3).FieldPrefab;//�T�{�ͦ����a            
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Goast"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Goast";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(4).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(4).FieldPrefab;//�T�{�ͦ����a            
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Needle"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Needle";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(5).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.CritCheck();//�T�{�z��
            //�z���ʵe
            //�T�{�����{��
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Qian)//��X�h�Ӷˮ`��
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Punch"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Punch";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(6).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.CritCheck();//�T�{�z��
            //�z���ʵe
            //�T�{�����{��
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Zhen)//��X�h�Ӷˮ`��
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Sheep"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Sheep";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(7).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(7).FieldPrefab;//�T�{�ͦ����a            
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "RockHand"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "RockHand";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(8).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.CritCheck();//�T�{�z��
            //�z���ʵe
            //�T�{�����{��
            MainBattleSystem.instance.battleAnimationContents.DamageDelt = new List<int>();
            if (MainBattleSystem.instance.NowFocusTrigrams == EightTrigrams.Zhen)//��X�h�Ӷˮ`��
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
            MainBattleSystem.instance.MinusMana(7);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Spring"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Spring";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).FieldPrefab;//�T�{�ͦ����a            
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
            MainBattleSystem.instance.MinusMana(5);//���]
            MainBattleSystem.instance.m_battleStatus = BattleStatus.DamageStep;
            if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] != null)//�T�{�}�W�O�_���}�k
            {
                MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().CallDestroy();
                Destroy(MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams]);//�������a�ĪG
            }
            MainBattleSystem.instance.battleAnimationContents.TheAnimateBePlayed = "Spring"; //�T�{�ʧ@
            MainBattleSystem.instance.LastSkill = "Spring";//�W�@�l�����ϥ�
            MainBattleSystem.instance.battleAnimationContents.AnimationTime = 1.5f;//�T�{�ʧ@�ɪ�
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).AnimationPrefab;//�T�{�ͦ��S��
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(9).FieldPrefab;//�T�{�ͦ����a            
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

