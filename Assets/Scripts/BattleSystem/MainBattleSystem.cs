using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainBattleSystem : MonoBehaviour
{
    public static MainBattleSystem instance;//獨體
    public BattleStatus m_battleStatus = BattleStatus.ReadyFight;//回合狀態
    public SaveScriptableObject m_player;//玩家記錄檔
    public PlayerInformation BattleUseStats;//戰鬥用數值
    public Skillbase SKbase;//技能庫
    public SkillDatabaseOBJ skillDatabaseOBJ;//技能資訊庫
    public List<GameObject> SkillButtons;//技能按鈕們
    public UnityEvent SkillEvent = new UnityEvent();//放出招式的系統
    public GameObject[] FieldSkills = new GameObject[8];
    public EightTrigrams NowFocusTrigrams;public bool ReadyAttack = false;//場地選擇
    public int SkillPain;//疲勞值
    public moonblocks CritOrNot = moonblocks.None;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
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
                            Debug.Log("傻逼魔力不夠");
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
        SkillEvent.Invoke();

        SkillEvent.RemoveAllListeners();
        ReadyAttack = false;
    }
    public void MinusMana(int ManaCost)
    {        
         BattleUseStats.Current_MP -= ManaCost;        
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
