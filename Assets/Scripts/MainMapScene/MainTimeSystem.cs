using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTimeSystem : MonoBehaviour
{
    public static MainTimeSystem instance;//ฟWล้
    public TimeClass Time = new TimeClass();
    private void Awake()
    {
            instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
[System.Serializable]

public class TimeClass
{
    public int day;
    public int time;

    public TimeClass()
    {
        day = 1;
        time = 8;
    }
    public TimeClass(int _day, int _time)
    {
        day = _day;
        time = _time;
    }
}