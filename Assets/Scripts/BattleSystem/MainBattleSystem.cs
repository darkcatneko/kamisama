using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class MainBattleSystem : MonoBehaviour
{
    public TextMeshProUGUI ManaCountText;
    public Image ManaBar;private float TempMana;
    public int NowTurn;                                                 //�^�X��
    public BossClass ThisBoss;
    public int ManaTired = 0;                                           //�h�ҭ�
    public Animator SkillButtonLeverAnimation;                          //�Ա�ʵe��
    public GameObject PlayerSprite;                                     //���a����
    [System.NonSerialized]public List<SpriteRenderer> PlayerSprites = new List<SpriteRenderer>();
    [System.NonSerialized] public List<SpriteRenderer> BossSprites = new List<SpriteRenderer>();
    public Animator PlayerAnimator;                                     //���a�ʵe��
    public GameObject BossSprite;                                       //�]������
    public static MainBattleSystem instance;                            //�W��
    public BattleStatus m_battleStatus = BattleStatus.ReadyFight;       //�^�X���A
    public SaveScriptableObject m_player;                               //���a�O����
    public SaveScriptableObject AutoSave;                               //�۰ʦs��
    public PlayerInformation BattleUseStats;                            //�԰��μƭ�
    public Skillbase SKbase;                                            //�ޯ�w
    public SkillDatabaseOBJ skillDatabaseOBJ;                           //�ޯ��T�w
    public List<GameObject> SkillButtons;                               //�ޯ���s��
    public List<GameObject> EightTrimSpawnPoint;
    public UnityEvent SkillEvent = new UnityEvent();                    //��X�ۦ����t��
    public UnityEvent BuffClear = new UnityEvent();
    //public List<int> SkillDamage;                                     ////�ˮ`�s��
    public string LastSkill;                                            //�W�@�l
    public GameObject[] FieldSkills = new GameObject[8];
    public EightTrigrams NowFocusTrigrams;public bool ReadyAttack = false;//���a����� //��n��l
    public float BuffAmount;                                               //�[����
    public moonblocks CritOrNot = moonblocks.None;
    public BattleAnimationContents battleAnimationContents;
    private int NowSkillPage = 1;public bool CanChangePage = false;
    public SceneControllerOBJ sceneControllerOBJ;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        m_battleStatus = BattleStatus.ReadyFight;
        for (int i = 0; i < PlayerSprite.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            PlayerSprites.Add(PlayerSprite.GetComponentsInChildren<SpriteRenderer>()[i]);
        }
        for (int i = 0; i < BossSprite.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            BossSprites.Add(BossSprite.GetComponentsInChildren<SpriteRenderer>()[i]);
        }        
        m_player.Load();
      
        ThisBoss = sceneControllerOBJ.NextBoss.m_base;
        BattleUseStats = m_player.m_Player.Setup_battleInformation(m_player.m_Player);
        TempMana = BattleUseStats.Current_MP;
        for (int i = 0; i < SkillButtons.Count; i++)
        {
            SkillButtonSetUp(i,m_player.SkillBackPack[i]);
        }
        m_battleStatus = BattleStatus.PlayerTurn;        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y!=0)
        {
            ChangePageLever();
        }
        ManaUpdate();
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
                        if (NowSkillPage  == SkillButton/5+1)
                        {
                            if (skillDatabaseOBJ.GetSkillInformation(skillid).ManaCost+ManaTired <= BattleUseStats.Current_MP)
                            {
                                
                                StartCoroutine("PlayerAttack", SkillButton);
                            }
                            else
                            {
                                Debug.Log("�̹G�]�O����");
                            }
                        }
                        else
                        {
                            Debug.Log("�ح���");
                        }
                    }
                    else if (m_battleStatus == BattleStatus.ChooseEightTrigram)
                    {
                        StopAllCoroutines();
                        Debug.Log(123);
                        if (NowSkillPage == SkillButton / 5 + 1)
                        {
                            if (skillDatabaseOBJ.GetSkillInformation(skillid).ManaCost + ManaTired <= BattleUseStats.Current_MP)
                            {

                                StartCoroutine("PlayerAttack", SkillButton);
                            }
                            else
                            {
                                Debug.Log("�̹G�]�O����");
                            }
                        }
                        else
                        {
                            Debug.Log("�ح���");
                        }
                    }
                }
                
                
                );
        }       
    }
    public IEnumerator PlayerAttack(int _id)
    {
        m_battleStatus = BattleStatus.ChooseEightTrigram;
        yield return new WaitUntil(() =>ReadyAttack == true);
        MainBattleSystem.instance.m_battleStatus = BattleStatus.PlayerTurn;
        SkillEvent.AddListener(SKbase.FindSkillFunction(m_player.SkillBackPack[_id]));
        SkillEvent.Invoke();//���]�O�åB����a���
        //�t�Xanimation
        PlayerAnimator.SetBool(battleAnimationContents.TheAnimateBePlayed,true);
        yield return new WaitForSeconds(battleAnimationContents.AnimationTime);
        PlayerAnimator.SetBool(battleAnimationContents.TheAnimateBePlayed, false);
        yield return new WaitForSeconds(battleAnimationContents.BattleEffectTime);
        battleAnimationContents.DamageDelt = new List<int>();
        battleAnimationContents.NowDisplayDamage = 0;
        m_battleStatus = BattleStatus.PlayerTurn;
        SkillEvent.RemoveAllListeners();
        ReadyAttack = false;
    }
    public void MinusMana(int ManaCost)
    {
        TempMana = BattleUseStats.Current_MP;        
        BattleUseStats.Current_MP -= ManaCost+ManaTired;
        DOTween.To(() => { return TempMana; }, x => TempMana = x, BattleUseStats.Current_MP, 1f);
        ManaTired++;
    }
    public void ManaUpdate()
    {
        ManaCountText.text = BattleUseStats.Current_MP.ToString() + "/" + Mathf.RoundToInt(BattleUseStats.INT.m_currentstat * 0.1f + 5).ToString();
        ManaBar.fillAmount = TempMana / (BattleUseStats.INT.m_currentstat * 0.1f + 5);
    }
    public void CritCheck3() 
    {
        int Luck;
        Luck = 30 + Random.Range(-10, BattleUseStats.LUK.m_currentstat);
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
        Debug.Log(CritOrNot.ToString());
    }
    public void CritCheck()
    {
        int Luck;int Luck2;
        Luck = Random.Range(0, 101);
        if (MainBattleSystem.instance.BattleUseStats.LUK.m_currentstat>Luck)
        {
            Luck2 = Random.Range(0, 101);            
            if (MainBattleSystem.instance.BattleUseStats.LUK.m_currentstat > Luck2)
            {
                CritOrNot = moonblocks.OneUpOneDown;
            }
            else
            {
                CritOrNot = moonblocks.TwoUp;
            }
        }
        else
        {
            Luck2 = Random.Range(0, 101);
            if (Luck2 % 2==0)
            {
                CritOrNot = moonblocks.TwoUp;
            }
            else
            {
                CritOrNot = moonblocks.TwoDown;
            }           
        }
        Debug.Log(CritOrNot.ToString());
    }
    public void EndPlayerTurn()
    {
        if(m_battleStatus == BattleStatus.PlayerTurn)
        {
            m_battleStatus = BattleStatus.EnemyTurn;
            //��������
            //check���a�O�_���`
            //���^���a�^�X
            BackToPlayerTurn();
        }
    }
    public void ChangeHP(int value)
    {
        int TempHP;
        TempHP = BattleUseStats.Current_HP;
        BattleUseStats.Current_HP += value;
        DOTween.To(() => { return TempHP; }, x => TempHP = x, BattleUseStats.Current_HP, 1f);
    }
    public void BackToPlayerTurn()
    {       
       NowTurn++;
        ChangeHP(BattleUseStats.Regen);
        TempMana = BattleUseStats.Current_MP;
        ManaTired = 0;
        BattleUseStats.Current_MP = Mathf.Clamp(BattleUseStats.Current_MP + BattleUseStats.Mana_regen_speed, 0, Mathf.RoundToInt(BattleUseStats.INT.m_currentstat * 0.1f + 5)) ;
        DOTween.To(() => { return TempMana; }, x => TempMana = x, BattleUseStats.Current_MP, 1f);
        for (int i = 0; i < FieldSkills.Length; i++)
        {
            if (FieldSkills[i]!=null)
            {
                if (FieldSkills[i].GetComponent<OnFieldDestroy>().SelfDestroyCountDown != -1)
                {
                    if (FieldSkills[i].GetComponent<OnFieldDestroy>().EndTurn == NowTurn)
                    {
                        FieldSkills[i].GetComponent<OnFieldDestroy>().CallDestroy();
                        Destroy(FieldSkills[i]);
                    }
                }
            }
            
        }//�T�{�W�L��buff�O�_�ݭn�^��
        m_battleStatus = BattleStatus.PlayerTurn;

    }
    
}
[System.Serializable]
public class BattleAnimationContents
{
    public int NowDisplayDamage = 0;
    public string TheAnimateBePlayed;
    public GameObject BattleEffect;
    public GameObject FieldPrefab;
    public float AnimationTime;
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
    TwoUp = 10,
    TwoDown = 9 ,
    OneUpOneDown = 11,
    None = 0,
}
[System.Serializable]
public class PlayerBuff
{
    public Stats BuffStat;
    public float Amount;
    public int StartTurn;
    public int EndTurn;
    public void Buffend()
    {
        
    }
}
