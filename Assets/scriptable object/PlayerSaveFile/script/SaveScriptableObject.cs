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
    public int Exp;
    //�ޯ�]
    public int Exp_To_Next_Level => Mathf.RoundToInt((Level^3*10)/11);
    public int BasicMana => Level + Mathf.RoundToInt(INT.m_currentstat * 0.1f);
    public int Mana_regen_speed;

    
    public int Current_MP;
    public int Current_HP;
    public int MaxHP;
    #region ���a�ƭ�
    public PlayerStats POW = new PlayerStats(Stats.POW, 1);
    public PlayerStats SPI = new PlayerStats(Stats.SPI, 1);
    public PlayerStats LUK = new PlayerStats(Stats.LUK, 1);
    public PlayerStats DEX = new PlayerStats(Stats.DEX, 1);
    public PlayerStats INT = new PlayerStats(Stats.INT, 1);
    public PlayerStats HP = new PlayerStats(Stats.HP, 1);
    public PlayerStats DEF = new PlayerStats(Stats.DEF, 1);
    public PlayerStats ATK = new PlayerStats(Stats.ATK, 1);
    #endregion 
    #region �غc��
    public PlayerInformation(int _level, int _exp)
    {
        Level = _level;
        Exp = _exp;
    }
    public PlayerInformation()
    {
        Level = 1;
        Exp = 0;
    }
    #endregion 
    public void GainExp(int _exp)
    {
        Exp += _exp;
        while(Exp>=Exp_To_Next_Level)
        {
            Exp -= Exp_To_Next_Level;
            Level++;
        }
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
    POW,//�k�N���m�O
    SPI,//�k�N�����O
    LUK,//�Y�M�����\�v
    DEX,//�j�ײv
    INT,//MP�W��
    HP,//HP
    DEF,//��˭�
    ATK,//���z�����O
}
[System.Serializable]
public class PlayerStats
{
    public Stats m_stat;
    public float m_currentstat;
    public PlayerStats(Stats _stat, float m_current)
    {
        m_stat = _stat;
        m_currentstat = m_current;
    }
}


