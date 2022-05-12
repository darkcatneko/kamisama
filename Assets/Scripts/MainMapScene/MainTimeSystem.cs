using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTimeSystem : MonoBehaviour
{
    public TextMeshProUGUI Day_Text;
    public TextMeshProUGUI Time_Text;

    public static MainTimeSystem instance;//ฟWล้
    public TimeClass Time;
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
            day++;
            MainSceneDataCenter.instance.Player_save.Can_Get_Flag = true;
            time = 8;
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