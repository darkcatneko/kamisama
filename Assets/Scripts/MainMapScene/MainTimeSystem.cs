using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTimeSystem : MonoBehaviour
{
    public TextMeshProUGUI Day_Text;
    public TextMeshProUGUI Time_Text;

    public static MainTimeSystem instance;//獨體
    public TimeClass Time;
    public List<GameObject> FlagPrefabs;
    private void Awake()
    {
            instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        Time = MainSceneDataCenter.instance.Player_save.TimeSaveData;
        Day_Text.text = Time.day.ToString()+" /";
        Time_Text.text = Time.time.ToString()+":00";
    }
    public void ReloadFlags()
    {
        foreach (var item in FlagPrefabs)
        {
            Destroy(item);
        }
        for (int i = 0; i < MainSceneDataCenter.instance.Player_save.MapFlagCheck.Length; i++)
        {
            MainSceneDataCenter.instance.Player_save.MapFlagCheck[i] = false;
        }
    }

}
[System.Serializable]

public class TimeClass
{
    public int day;
    public int time;
    public bool Get_Flag = false;
    public void PlusTime()
    {
        time++;
        if (time>=23)
        {
            MainTimeSystem.instance.ReloadFlags();
            day++;
            MainSceneDataCenter.instance.Player_save.Can_Get_Flag = true;
            time = 8;
            MainSceneDataCenter.instance.Player_save.Save();
            //進入戰鬥
        }
    }
    
    public TimeClass()
    {
        day = 1;
        time = 8;
        Get_Flag = false;
    }
    public TimeClass(int _day, int _time,bool _get)
    {
        day = _day;
        time = _time;
        Get_Flag = _get;
    }
}