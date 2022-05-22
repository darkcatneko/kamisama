using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBattleSystem : MonoBehaviour
{
    public static MainBattleSystem instance;
    public BattleStatus m_battleStatus = BattleStatus.ReadyFight;
    public SaveScriptableObject m_player;
    public PlayerInformation BattleUseStats;
    public Skillbase SKbase;
    public SkillDatabaseOBJ skillDatabaseOBJ;
    public List<GameObject> SkillButtons;
    public EightTrigrams NowFocusTrigrams;
    public int SkillPain;//¯h³Ò­È
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
            SkillButtons[SkillButton].GetComponent<Button>().onClick.AddListener(SKbase.FindSkillFunction(m_player.SkillBackPack[SkillButton]));
        }       
    }
    public void MinusMana(int ManaCost)
    {        
         BattleUseStats.Current_MP -= ManaCost;        
    }
}

[System.Serializable]
public enum BattleStatus
{
    ReadyFight,
    PlayerTurn,
    DamageStep,
    EnemyTurn,
}
