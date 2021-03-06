using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class MainBattleSystem : MonoBehaviour
{
    public Dialogue_Data_Object DialogueOBJ;
    public TextMeshProUGUI ManaCountText;
    public Image ManaBar;private float TempMana; public int TempHP;
    public int NowTurn;                                                 //回合數
    public BossClass ThisBoss;
    public int ManaTired = 0;                                           //疲勞值
    public Animator SkillButtonLeverAnimation;                          //拉桿動畫器
    public GameObject PlayerSprite;                                     //玩家物件
    [System.NonSerialized]public List<SpriteRenderer> PlayerSprites = new List<SpriteRenderer>();
    [System.NonSerialized] public List<SpriteRenderer> BossSprites = new List<SpriteRenderer>();
    public Animator PlayerAnimator;                                     //玩家動畫器
    public GameObject BossSprite;                                       //魔王物件
    public static MainBattleSystem instance;                            //獨體
    public BattleStatus m_battleStatus = BattleStatus.ReadyFight;       //回合狀態
    public SaveScriptableObject m_player;                               //玩家記錄檔
    public SaveScriptableObject AutoSave;                               //自動存檔
    public PlayerInformation BattleUseStats;                            //戰鬥用數值
    public Skillbase SKbase;                                            //技能庫
    public SkillDatabaseOBJ skillDatabaseOBJ;                           //技能資訊庫
    public List<GameObject> SkillButtons;                               //技能按鈕們
    public List<GameObject> EightTrimSpawnPoint;
    private UnityEvent SkillEvent = new UnityEvent();                    //放出招式的系統
    public UnityEvent BossMoveSet = new UnityEvent();
    public UnityEvent BuffClear = new UnityEvent();
    //public List<int> SkillDamage;                                     ////傷害存檔
    public string LastSkill;                                            //上一召
    public GameObject[] FieldSkills = new GameObject[8];
    public EightTrigrams NowFocusTrigrams;public bool ReadyAttack = false;//場地的選擇 //選好格子
    public float BuffAmount;                                               //加成值
    public List<PlayerBuff> Buffs;
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
        BossSprite = Instantiate(sceneControllerOBJ.NextBoss.m_base.BossGameObjectPrefab);
        for (int i = 0; i < PlayerSprite.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            PlayerSprites.Add(PlayerSprite.GetComponentsInChildren<SpriteRenderer>()[i]);
        }
        for (int i = 0; i < BossSprite.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            BossSprites.Add(BossSprite.GetComponentsInChildren<SpriteRenderer>()[i]);
        }        
        m_player.Load();

        //ThisBoss = sceneControllerOBJ.NextBoss.m_base;
        ThisBoss.Register(ThisBoss, sceneControllerOBJ.NextBoss.m_base);               
        BattleUseStats = m_player.m_Player.Setup_battleInformation(m_player.m_Player);
        TempMana = BattleUseStats.Current_MP;
        TempHP = BattleUseStats.Current_HP;
        BossHealthUpdate.instance.TempBossHealth =Mathf.RoundToInt( ThisBoss.FindStat(Stats.HP));
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            ThisBoss.CallBossDamage(10);
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
                            if (skillDatabaseOBJ.GetSkillInformation(skillid).ManaCost+ManaTired <= BattleUseStats.Current_MP)//檢查魔力量
                            {
                                
                                StartCoroutine("PlayerAttack", SkillButton);
                            }
                            else
                            {
                                Debug.Log("傻逼魔力不夠");
                            }
                        }
                        else
                        {
                            Debug.Log("煥頁拉");
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
                                Debug.Log("傻逼魔力不夠");
                            }
                        }
                        else
                        {
                            Debug.Log("煥頁拉");
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
        SkillEvent.Invoke();//扣魔力並且鎖住玩家行動 //前提條件判定
        //演出animation
        PlayerAnimator.SetBool(battleAnimationContents.TheAnimateBePlayed,true);
        yield return new WaitForSeconds(battleAnimationContents.AnimationTime);
        PlayerAnimator.SetBool(battleAnimationContents.TheAnimateBePlayed, false);
        yield return new WaitForSeconds(battleAnimationContents.BattleEffectTime+1);        
        battleAnimationContents.DamageDelt = new List<DamageNumber>();
        battleAnimationContents.NowDisplayDamage = 0;
        battleAnimationContents.LifeSteal = new List<int>();
        battleAnimationContents.NowRegenNumber = 0;
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
        ManaBar.fillAmount = TempMana / Mathf.RoundToInt(BattleUseStats.INT.m_currentstat * 0.1f + 5);
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
    public void EndPlayerTurnButton()
    {
        StartCoroutine("EndPlayerTurn");
    }
    public IEnumerator EndPlayerTurn()
    {
        if(m_battleStatus == BattleStatus.PlayerTurn)
        {
            m_battleStatus = BattleStatus.EnemyTurn;
            Instantiate(Resources.Load<GameObject>("BattleScene/UI/Boss'sTurn"));
            yield return new WaitForSeconds(2f);
            ThisBoss.BossAttack.Invoke();
           
            //check玩家是否死亡
            //換回玩家回合            
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
        Instantiate(Resources.Load<GameObject>("BattleScene/UI/Player'sTurn"));
        NowTurn++;
        ChangeHP(BattleUseStats.Regen);
        TempMana = BattleUseStats.Current_MP;
        ManaTired = 0;
        BattleUseStats.Current_MP = Mathf.Clamp(BattleUseStats.Current_MP + BattleUseStats.Mana_regen_speed, 0, Mathf.RoundToInt(BattleUseStats.INT.m_currentstat * 0.1f + 5)) ;
        DOTween.To(() => { return TempMana; }, x => TempMana = x, BattleUseStats.Current_MP, 1f);
        for (int i = 0; i < FieldSkills.Length; i++)//檢查場地
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
            
        }//確認上過的buff是否需要回調
        for (int i = 0; i < Buffs.Count; i++)
        {
            if (Buffs[i].EndTurn == NowTurn)
            {
                Buffs[i].Buffend();
                Buffs.RemoveAt(i);
            }
        }
        m_battleStatus = BattleStatus.PlayerTurn;

    }
    public IEnumerator BossDie()
    {
        if (m_battleStatus == BattleStatus.BossDie)
        {
            Debug.Log("他死透了");
            BossSprite.GetComponent<Animator>().SetBool("DIE", true);//Boss死亡動畫
            yield return new WaitForSeconds(2f);
            Instantiate(Resources.Load("BattleScene/UI/Boss'sTurn 1"));
            //生成戰鬥勝利畫面
            yield return null;
        }
    }
    public void IntoDialogueScene(string _plot_name)
    {
        DialogueOBJ.The_NodePad_Be_read = _plot_name;
        DialogueOBJ.WhichLineItRead = 0;
        m_player.Save();
        SceneManager.LoadScene(1);
    }
    
}
[System.Serializable]
public class BattleAnimationContents
{
    public int NowRegenNumber = 0;
    public int NowDisplayDamage = 0;
    public string TheAnimateBePlayed;
    public GameObject BattleEffect;
    public GameObject FieldPrefab;
    public float AnimationTime;
    public int BattleEffectTime;
    public List<DamageNumber> DamageDelt;
    public List<int> LifeSteal; 
}

[System.Serializable]
public enum BattleStatus
{
    ReadyFight,
    PlayerTurn,
    DamageStep,
    EnemyTurn,
    ChooseEightTrigram,
    BossDie,
    PlayerDie,
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
        switch(BuffStat)
        {
            case Stats.DEF:
                MainBattleSystem.instance.BattleUseStats.DEF.m_currentstat -= Mathf.RoundToInt(Amount);
                return;
            case Stats.SPI:
                MainBattleSystem.instance.BattleUseStats.SPI.m_currentstat -= Mathf.RoundToInt(Amount);
                return;
            case Stats.POW:
                MainBattleSystem.instance.BattleUseStats.POW.m_currentstat -= Mathf.RoundToInt(Amount);
                return;
        }
    }
    public PlayerBuff(Stats st, float am, int startturn, int endturn)
    {
        BuffStat = st;
        Amount = am;
        StartTurn = startturn;
        EndTurn = endturn;
    }
}
[System.Serializable]
public class DamageNumber
{
    public int Number;
    public DamageType type;
    public DamageNumber(int num, DamageType _type)
    {
        Number = num;
        type = _type;
    }
}
