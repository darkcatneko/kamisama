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
    public bool Newtogame = true;

    public string savePath;
    public string SaveDate;
    public TimeClass TimeSaveData;
    public PlayerInformation m_Player;
    public int[] SkillBackPack = new int[10];
    public bool[] MapFlagCheck = new bool[14];
    public bool[] SkillUnlockCheck = new bool[100];
    public bool Can_Get_Flag = true;
    public int FlagCount = 0;

    public int Now_Playing_Scene = 2;
    public string Now_Watching_Plot = "Opening";
    public int Now_Watching_Sentence;
    public string Now_BackgroundPic;
    public string Now_BGM;
    public string Now_Speaker;
    public string TempNote;
    public bool IfSpecialTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetInformationString()
    {
        return "LV:" + m_Player.Level.ToString() + " Day:" + TimeSaveData.day.ToString() + " Time:" + TimeSaveData.time.ToString() + ":00\n" + "NewData:" + Newtogame.ToString();
    }
    public void EqualFunction(SaveScriptableObject a, SaveScriptableObject b)
    {
        a.Newtogame = b.Newtogame;
        a.TimeSaveData.day = b.TimeSaveData.day;
        a.TimeSaveData.time = b.TimeSaveData.time;
        a.m_Player.Level = b.m_Player.Level;
        a.m_Player.POW.m_currentstat = b.m_Player.POW.m_currentstat;
        a.m_Player.SPI.m_currentstat = b.m_Player.SPI.m_currentstat;
        a.m_Player.LUK.m_currentstat = b.m_Player.LUK.m_currentstat;
        a.m_Player.DEX.m_currentstat = b.m_Player.DEX.m_currentstat;
        a.m_Player.INT.m_currentstat = b.m_Player.INT.m_currentstat;
        a.m_Player.HP.m_currentstat = b.m_Player.HP.m_currentstat;
        a.m_Player.DEF.m_currentstat = b.m_Player.DEF.m_currentstat;
        a.m_Player.ATK.m_currentstat = b.m_Player.ATK.m_currentstat;
        a.m_Player.Love.m_currentstat = b.m_Player.Love.m_currentstat;
        a.m_Player.Yao_Wan.m_currentstat = b.m_Player.Yao_Wan.m_currentstat;
        a.m_Player.Fong_Shin.m_currentstat = b.m_Player.Fong_Shin.m_currentstat;
        a.m_Player.Kin_hua.m_currentstat = b.m_Player.Kin_hua.m_currentstat;
        a.m_Player.Ron_Xiu.m_currentstat = b.m_Player.Ron_Xiu.m_currentstat;
        for (int i = 0; i < a.MapFlagCheck.Length; i++)
        {
            a.MapFlagCheck[i] = b.MapFlagCheck[i];
        }
        for (int i = 0; i < a.SkillBackPack.Length; i++)
        {
            a.SkillBackPack[i] = b.SkillBackPack[i];
        }
        for (int i = 0; i < a.SkillUnlockCheck.Length; i++)
        {
            a.SkillUnlockCheck[i] = b.SkillUnlockCheck[i];
        }
        a.FlagCount = b.FlagCount;
        a.Now_Playing_Scene = b.Now_Playing_Scene;
        a.Now_Watching_Plot = b.Now_Watching_Plot;
        a.Now_Watching_Sentence = b.Now_Watching_Sentence;
        a.Now_BackgroundPic = b.Now_BackgroundPic;
        a.Now_Speaker = b.Now_Speaker;
        a.Now_BGM = b.Now_BGM;
        a.IfSpecialTime = b.IfSpecialTime;
        a.TempNote = b.TempNote;
        a.Can_Get_Flag = b.Can_Get_Flag;
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
        Newtogame = true;
        TimeSaveData.day = 1;
        TimeSaveData.time = 8;
        m_Player = new PlayerInformation();
        MapFlagCheck = new bool[14];
        SkillUnlockCheck = new bool[100];
        SkillUnlockCheck[0] = true;
        SkillBackPack = new int[10];
        FlagCount = 0;
        Can_Get_Flag = true;
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
    public int RandomNum()
    {

        return Random.Range(1, Level / 5 + 1);
    }
    public string Orginized(int[] _st,string[] _ar,int _surprise)
    {
        for (int i = 0; i < _ar.Length; i++)
        {
            _ar[i] = _st[i].ToString();
        }
        _ar[_surprise] += "!!";
        return "+" + _ar[4].ToString() + "\n" + "+" + _ar[6].ToString() + "\n" + "+" + _ar[5].ToString() + "\n" + "+"
            + _ar[0].ToString() + "\n" + "+" + _ar[1].ToString() + "\n" + "+" + _ar[2].ToString() + "\n" + "+" + _ar[3].ToString() + "\n" + "+" + _ar[7].ToString() + "\n" + "+" + _ar[8].ToString() + "\n";
    }
    public string GainLevel(int _lv, Stats _stat)
    {
       int[] growth = new int[9];
       string[] growthstring = new string[9];
        Level += _lv;
        for (int i = 0; i < _lv; i++)
        {
            for (int y = 0; y < 7; y++)
            {
                growth[y] = RandomNum();
            }
            POW.m_currentstat += growth[0];         
            SPI.m_currentstat += growth[1];
            DEX.m_currentstat += growth[2];
            INT.m_currentstat += growth[3];
            HP.m_currentstat  += growth[4];
            DEF.m_currentstat += growth[5];
            ATK.m_currentstat += growth[6];
            int chance;
            chance = Random.Range(0, 101);
            if (chance > 85)
            {
                growth[7] = RandomNum();
                LUK.m_currentstat += growth[7];
            }
            else
            {
                growth[7] = 0;
                HP.m_currentstat += growth[7];
            }
        }        
        switch(_stat)
        {
            case Stats.INT:
                Yao_Wan.m_currentstat += 1;
                growth[8] = 1;
                INT.m_currentstat -= growth[3];
                growth[3] += Random.Range(1, (Level / 5) * ((Level / 10) + 1));                
                INT.m_currentstat += growth[3];
                return Orginized(growth, growthstring, 3);
            case Stats.DEX:
                Fong_Shin.m_currentstat += 1;
                growth[8] = 1;
                DEX.m_currentstat -= growth[2];
                growth[2] += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                DEX.m_currentstat += growth[2];
                return Orginized(growth, growthstring, 2);
            case Stats.ATK:
                Fong_Shin.m_currentstat += 1;
                growth[8] = 1;
                ATK.m_currentstat -= growth[6];
                growth[6] += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                ATK.m_currentstat += growth[6];
                return Orginized(growth, growthstring, 6);
            case Stats.POW:
                Kin_hua.m_currentstat += 1;
                growth[8] = 1;
                POW.m_currentstat -= growth[0];
                growth[0] += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                POW.m_currentstat += growth[0];
                return Orginized(growth, growthstring, 0);
            case Stats.SPI:
                Kin_hua.m_currentstat += 1;
                growth[8] = 1;
                SPI.m_currentstat -= growth[1];
                growth[1] += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                SPI.m_currentstat += growth[1];
                return Orginized(growth, growthstring, 1);
            case Stats.HP:
                Ron_Xiu.m_currentstat += 1;
                growth[8] = 1;
                HP.m_currentstat -= growth[4];
                growth[4] += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                HP.m_currentstat += growth[4];
                return Orginized(growth, growthstring, 4);
            case Stats.DEF:
                Ron_Xiu.m_currentstat += 1;
                growth[8] = 1;
                DEF.m_currentstat -= growth[5];
                growth[5] += Random.Range(1, (Level / 5) * ((Level / 10) + 1));
                DEF.m_currentstat += growth[5];
                return Orginized(growth, growthstring, 5);
            default:
                return null;
        }
    }
    public void Gain_Love_Level(int _amount)
    {
        Love.m_currentstat += _amount;
    }
    
   

    public PlayerInformation Setup_battleInformation (PlayerInformation m_Playerstats)
    {
        PlayerInformation Battle_used;
        Battle_used = m_Playerstats;
        Battle_used.MaxHP =Mathf.RoundToInt(m_Playerstats.HP.m_currentstat*10) ;
        Battle_used.Current_MP = Mathf.RoundToInt(m_Playerstats.INT.m_currentstat*0.1f+5);
        Battle_used.Current_HP = Mathf.RoundToInt(m_Playerstats.HP.m_currentstat * 10);
        Battle_used.Mana_regen_speed = 5;
        return Battle_used;
    }
    
    
}
[System.Serializable]
public enum Stats
{
    POW,//法術防禦力
    SPI,//法術攻擊力    
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
    LUK,//擲杯的成功率
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


