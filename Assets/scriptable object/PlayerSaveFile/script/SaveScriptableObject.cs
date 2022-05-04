using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "NewSaveFile", menuName = "SaveData/Data")]
public class SaveScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public TimeClass TimeSaveData;
    public PlayerInformation m_Player;
    public bool[] MapFlagCheck = new bool[14];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        Debug.Log(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);

            file.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        TimeSaveData.day = 1;
        TimeSaveData.time = 8;
        m_Player = new PlayerInformation();
    }
    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {

    }
}
[System.Serializable]
public class PlayerInformation 
{
    public int Level;   
    //技能包
    public int BasicMana => Level + Mathf.RoundToInt(INT.m_currentstat * 0.1f);
    public int Mana_regen_speed;

    
    public int Current_MP;
    public int Current_HP;
    public int MaxHP;
    #region 玩家數值
    public PlayerStats POW = new PlayerStats(Stats.POW, 1);
    public PlayerStats SPI = new PlayerStats(Stats.SPI, 1);
    public PlayerStats LUK = new PlayerStats(Stats.LUK, 1);
    public PlayerStats DEX = new PlayerStats(Stats.DEX, 1);
    public PlayerStats INT = new PlayerStats(Stats.INT, 1);
    public PlayerStats HP = new PlayerStats(Stats.HP, 1);
    public PlayerStats DEF = new PlayerStats(Stats.DEF, 1);
    public PlayerStats ATK = new PlayerStats(Stats.ATK, 1);
    public PlayerStats Love = new PlayerStats(Stats.Love, 1);
    public PlayerStats Yao_Wan = new PlayerStats(Stats.Yao_Wan, 1);
    public PlayerStats Fong_Shin = new PlayerStats(Stats.Fong_Shin, 1);
    public PlayerStats Kin_hua = new PlayerStats(Stats.Kin_hua, 1);
    public PlayerStats Ron_Xiu = new PlayerStats(Stats.Ron_Xiu, 1);
    #endregion 
    #region 建構式
    public PlayerInformation(int _level)
    {
        Level = _level;
    }
    public PlayerInformation()
    {
        Level = 1;
    }
    #endregion 
    public void GainLevel(int _lv, Stats _stat)
    {
        Level += _lv;
        for (int i = 0; i < _lv; i++)
        {
            POW.m_currentstat += Random.Range(1, Level / 5 + 1);
            SPI.m_currentstat += Random.Range(1, Level / 5 + 1);
            DEX.m_currentstat += Random.Range(1, Level / 5 + 1);
            INT.m_currentstat += Random.Range(1, Level / 5 + 1);
            HP.m_currentstat += Random.Range(1,  Level / 5 + 1);
            DEF.m_currentstat += Random.Range(1, Level / 5 + 1);
            ATK.m_currentstat += Random.Range(1, Level / 5 + 1);
            int chance;
            chance = Random.Range(0, 101);
            if (chance > 85)
            {
                LUK.m_currentstat += Random.Range(1, Level / 5 + 1);
            }
        }        
        switch(_stat)
        {
            case Stats.INT:
                Yao_Wan.m_currentstat += 1;
                INT.m_currentstat += Random.Range(1, (Level/5)*((Level / 10) + 1));
                return;
            case Stats.DEX:
                Fong_Shin.m_currentstat += 1;
                DEX.m_currentstat += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                return;
            case Stats.ATK:
                Fong_Shin.m_currentstat += 1;
                ATK.m_currentstat += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                return;
            case Stats.POW:
                Kin_hua.m_currentstat += 1;
                POW.m_currentstat += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                return;
            case Stats.SPI:
                Kin_hua.m_currentstat += 1;
                SPI.m_currentstat += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                return;
            case Stats.HP:
                Ron_Xiu.m_currentstat += 1;
                HP.m_currentstat += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                return;
            case Stats.DEF:
                Ron_Xiu.m_currentstat += 1;
                DEF.m_currentstat += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                return;
        }
    }
    public void GainLevelVerTwo(int _lv, Stats _stat)
    {
        Level += _lv;
        for (int i = 0; i < _lv; i++)
        {
            POW.m_currentstat += Level / 20 + 1;
            SPI.m_currentstat += Level / 20 + 1;
            DEX.m_currentstat += Level / 20 + 1;
            INT.m_currentstat += Level / 20 + 1;
            HP.m_currentstat  += Level / 20 + 1;
            DEF.m_currentstat += Level / 20 + 1;
            ATK.m_currentstat += Level / 20 + 1;
            int chance;
            chance = Random.Range(0, 101);
            if (chance > 85)
            {
                LUK.m_currentstat += Level / 10 + 1;
            }
        }
        switch (_stat)
        {
            case Stats.INT:
                Yao_Wan.m_currentstat += 1;
                INT.m_currentstat += Mathf.RoundToInt( Mathf.Sqrt(Mathf.Pow(Yao_Wan.m_currentstat,1.2f) / (Level/10 + 1)));
                INT.m_currentstat -= Level / 20 + 1;                     
                return;
            case Stats.DEX:
                Fong_Shin.m_currentstat += 1;
                DEX.m_currentstat += Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(Fong_Shin.m_currentstat, 1.2f) / (Level / 10+1)));
                DEX.m_currentstat -= Level / 20 + 1;
                return;
            case Stats.ATK:
                Fong_Shin.m_currentstat += 1;
                ATK.m_currentstat += Mathf.RoundToInt(Mathf.Sqrt( Mathf.Pow(Fong_Shin.m_currentstat, 1.2f) / (Level / 10 + 1)));
                ATK.m_currentstat -= Level / 20 + 1;
                return;
            case Stats.POW:
                Kin_hua.m_currentstat += 1;
                POW.m_currentstat += Mathf.RoundToInt(Mathf.Sqrt( Mathf.Pow(Kin_hua.m_currentstat, 1.2f) / (Level / 10 + 1)));
                POW.m_currentstat -= Level / 20 + 1;
                return;
            case Stats.SPI:
                Kin_hua.m_currentstat += 1;
                SPI.m_currentstat += Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(Kin_hua.m_currentstat, 1.2f) / (Level / 10 + 1)));
                SPI.m_currentstat -= Level / 20 + 1;
                return;
            case Stats.HP:
                Ron_Xiu.m_currentstat += 1;
                HP.m_currentstat += Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(Ron_Xiu.m_currentstat, 1.2f) / (Level / 10 + 1)));
                HP.m_currentstat -= Level / 20 + 1;
                return;
            case Stats.DEF:
                Ron_Xiu.m_currentstat += 1;
                DEF.m_currentstat += Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(Ron_Xiu.m_currentstat, 1.2f) / (Level / 10 + 1)));
                DEF.m_currentstat -= Level / 20 + 1;
                return;
        }
    }//測試版
    public void GainLevelVerThree(int _lv, Stats _stat)
    {
        Level += _lv;
        for (int i = 0; i < _lv; i++)
        {
            SPI.m_currentstat += 1;
            DEX.m_currentstat += 1;
            INT.m_currentstat += 1;
            HP.m_currentstat  += 1;
            DEF.m_currentstat += 1;
            ATK.m_currentstat += 1;
            POW.m_currentstat += 1;
            int chance;
            chance = Random.Range(0, 101);
            if (chance > 85)
            {
                LUK.m_currentstat += Level / 10 + 1;
            }
        }
        switch (_stat)
        {
            case Stats.INT:
                Yao_Wan.m_currentstat += 1;
                INT.m_currentstat += Mathf.RoundToInt(LogicLine(Yao_Wan.m_currentstat));                
                return;
            case Stats.DEX:
                Fong_Shin.m_currentstat += 1;
                DEX.m_currentstat += Mathf.RoundToInt(LogicLine(Fong_Shin.m_currentstat));
                return;
            case Stats.ATK:
                Fong_Shin.m_currentstat += 1;
                ATK.m_currentstat += Mathf.RoundToInt(LogicLine(Fong_Shin.m_currentstat));
                return;
            case Stats.POW:
                Kin_hua.m_currentstat += 1;
                POW.m_currentstat += Mathf.RoundToInt(LogicLine(Kin_hua.m_currentstat));
                return;
            case Stats.SPI:
                Kin_hua.m_currentstat += 1;
                SPI.m_currentstat += Mathf.RoundToInt(LogicLine(Kin_hua.m_currentstat));
                return;
            case Stats.HP:
                Ron_Xiu.m_currentstat += 1;
                HP.m_currentstat += Mathf.RoundToInt(LogicLine(Ron_Xiu.m_currentstat));
                return;
            case Stats.DEF:
                Ron_Xiu.m_currentstat += 1;
                DEF.m_currentstat += Mathf.RoundToInt(LogicLine(Ron_Xiu.m_currentstat));
                return;
        }
    }//測試版
    public float LogicLine(float x)
    {
        float y;
        if (x < 24)
        {
            y = 3f / (1f + Mathf.Pow(2.7f, -(x * 0.02f))) * x + 1f;
        }
        else if( x< 40)
        {
            y = 6f / ((x - 15) * 0.08f);
        }
        else
        {
            y = Level / 20 ;
        }
        return y;
    }
    public PlayerInformation Setup_battleInformation (PlayerInformation m_Playerstats)
    {
        PlayerInformation Battle_used;
        Battle_used = m_Playerstats;
        Battle_used.MaxHP =Mathf.RoundToInt(m_Playerstats.HP.m_currentstat) ;
        Battle_used.Current_MP = 5;
        Battle_used.Current_HP = Mathf.RoundToInt(m_Playerstats.HP.m_currentstat);
        Battle_used.Mana_regen_speed = 5;
        return Battle_used;
    }
}
[System.Serializable]
public enum Stats
{
    POW,//法術防禦力
    SPI,//法術攻擊力
    LUK,//擲杯的成功率
    DEX,//迴避率
    INT,//MP上限
    HP,//HP
    DEF,//減傷值
    ATK,//物理攻擊力
    Love,//好感
    Yao_Wan,//藥王廟
    Fong_Shin,//風神
    Kin_hua,//金華
    Ron_Xiu,//榕樹
}
[System.Serializable]
public class PlayerStats
{
    public Stats m_stat;
    public int m_currentstat;
    public PlayerStats(Stats _stat, int m_current)
    {
        m_stat = _stat;
        m_currentstat = m_current;
    }
}


