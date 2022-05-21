using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBattleSystem : MonoBehaviour
{
    public static MainBattleSystem instance;
    public SaveScriptableObject m_player;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        m_player.Load();
        
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
}
[System.Serializable]
public enum BattleStatus
{
    ReadyFight,
    PlayerTurn,
    DamageStep,
    EnemyTurn,
}
