using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class MainBattleSystem : MonoBehaviour
{
    public int ManaTired = 0;//�h�ҭ�
    public Animator SkillButtonLeverAnimation;
    public Animator PlayerAnimator;
    public static MainBattleSystem instance;//�W��
    public BattleStatus m_battleStatus = BattleStatus.ReadyFight;//�^�X���A
    public SaveScriptableObject m_player;//���a�O����
    public PlayerInformation BattleUseStats;//�԰��μƭ�
    public Skillbase SKbase;//�ޯ�w
    public SkillDatabaseOBJ skillDatabaseOBJ;//�ޯ��T�w
    public List<GameObject> SkillButtons;//�ޯ���s��
    public UnityEvent SkillEvent = new UnityEvent();//��X�ۦ����t��
    public GameObject[] FieldSkills = new GameObject[8];
    public EightTrigrams NowFocusTrigrams;public bool ReadyAttack = false;//���a���
    public int SkillPain;//�h�ҭ�
    public moonblocks CritOrNot = moonblocks.None;
    public BattleAnimationContents battleAnimationContents;
    private int NowSkillPage = 1;public bool CanChangePage = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        m_battleStatus = BattleStatus.ReadyFight;
        m_player.Load();
        BattleUseStats = m_player.m_Player.Setup_battleInformation(m_player.m_Player);
        for (int i = 0; i < SkillButtons.Count; i++)
        {
            SkillButtonSetUp(i,m_player.SkillBackPack[i]);
        }
        m_battleStatus = BattleStatus.PlayerTurn;        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangePageLever();
        }
    }
    public void ChangePageLever()
    {
        if (CanChangePage == false)
        {
            if (NowSkillPage == 1)
            {
                CanChangePage = true;
                SkillButtonLeverAnimation.SetTrigger("Second");
                NowSkillPage = 2;
                Invoke("CanChangeButton", 0.7f);
            }
            else
            {
                CanChangePage = true;
                SkillButtonLeverAnimation.SetTrigger("First");
                NowSkillPage = 1;
                Invoke("CanChangeButton", 0.7f);
            }
        }        
    }
    public void CanChangeButton()
    {
        CanChangePage = false;
    }
    public void WhichBoss_DayBoss(int _whichDay)
    {
        switch(_whichDay)
        {
            case 2:
                return;
            case 3:
                return;
            case 4:
                return;
            case 5:
                return;
            case 6:
                return;
            case 7:
                return;
            case 8:
                return;

        }

    }
    public void SkillButtonSetUp(int SkillButton,int skillid)
    {
        if (skillid!=0)
        {
            SkillButtons[SkillButton].GetComponentsInChildren<Image>()[1].sprite = skillDatabaseOBJ.GetSkillInformation(skillid).SkillPic;
            SkillButtons[SkillButton].GetComponent<Button>().onClick.AddListener(
                () => 
                {
                    if (m_battleStatus == BattleStatus.PlayerTurn)
                    {
                        if (skillDatabaseOBJ.GetSkillInformation(skillid).ManaCost<=BattleUseStats.Current_MP)
                        {
                            StartCoroutine("PlayerAttack", SkillButton);
                        }
                        else
                        {
                            Debug.Log("�̹G�]�O����");
                        }                     
                    }                    
                }
                
                
                );
        }       
    }
    public IEnumerator PlayerAttack(int _id)
    {
        yield return new WaitUntil(() =>ReadyAttack == true);
        SkillEvent.AddListener(SKbase.FindSkillFunction(m_player.SkillBackPack[_id]));
        SkillEvent.Invoke();//���]�O�åB����a���
        //�t�Xanimation
        PlayerAnimator.SetBool(battleAnimationContents.TheAnimateBePlayed,true);
        yield return new WaitForSeconds(battleAnimationContents.AnimationTime);
        PlayerAnimator.SetBool(battleAnimationContents.TheAnimateBePlayed, false);
        yield return new WaitForSeconds(battleAnimationContents.BattleEffectTime);
        SkillEvent.RemoveAllListeners();
        ReadyAttack = false;
    }
    public void MinusMana(int ManaCost)
    {        
         BattleUseStats.Current_MP -= ManaCost+ManaTired;        
    }
    public void CritCheck()
    {
        int Luck;
        Luck = 30 + Random.Range(-10, BattleUseStats.LUK.m_currentstat*3);
        if (Luck >= 60)
        {
            CritOrNot = moonblocks.OneUpOneDown;
        }
        else if (Luck>=20)
        {
            CritOrNot = moonblocks.TwoUp;
        }
        else
        {
            CritOrNot = moonblocks.TwoDown;
        }
    }
    
}
[System.Serializable]
public class BattleAnimationContents
{
    public int NowDisplayDamage = 0;
    public string TheAnimateBePlayed;
    public GameObject BattleEffect;
    public int AnimationTime;
    public int BattleEffectTime;
    public List<int> DamageDelt;
}

[System.Serializable]
public enum BattleStatus
{
    ReadyFight,
    PlayerTurn,
    DamageStep,
    EnemyTurn,
    ChooseEightTrigram,
}
[System.Serializable]
public enum moonblocks
{
    TwoUp,
    TwoDown,
    OneUpOneDown,
    None,
}
