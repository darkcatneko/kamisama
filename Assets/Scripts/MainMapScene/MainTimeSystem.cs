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
        if (time >= 23)
        {
            MainTimeSystem.instance.ReloadFlags();
            day++;
            MainSceneDataCenter.instance.Player_save.Can_Get_Flag = true;
            time = 8;
            MainSceneDataCenter.instance.Player_save.Save();          
        }
    }
    public void CheckSpecialDay ()//讀文本 
    {
        switch(day)
        {
            case 1:
                if (time == 9)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("1-1");
                }                             
                return;
            case 2:
                if (time == 9)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("2-1");
                }
                else if(time == 8)
                {
                    if (MainSceneDataCenter.instance.Player_save.MapFlagCheck[2]==true)
                    {
                        MainSceneDataCenter.instance.IntoDialogueScene("1-2");
                    }
                    else
                    {
                        Debug.Log("幹你娘給我去插旗子");
                    }
                }
                return;
            case 3:
                if (time == 9)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("3-1");
                }
                else if (time == 8)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("2-2");
                }
                return;
            case 4:
                if (time == 9)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("4-1");
                }
                else if (time == 8)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("3-2");
                }
                return;
            case 5:
                if (time == 9)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("5-1");
                } 
                else if (time == 8)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("4-2");
                }
                return;
            case 6:
                if (time == 9)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("6-1");
                }
                else if (time == 8)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("5-2");
                }
                return;
            case 7:
                if (time == 9)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("7-1");
                }
                else if (time == 8)
                {
                    MainSceneDataCenter.instance.IntoDialogueScene("6-2");
                }
                return;
            case 8:
                {
                    if (time == 8)
                    {
                        MainSceneDataCenter.instance.IntoDialogueScene("7-2");
                    }
                }
                return;
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