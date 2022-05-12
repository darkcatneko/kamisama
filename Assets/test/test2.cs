using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Source
{
    public Source_type _sc;
    public int m_current = 0;
    public Source(Source_type m_sc, int current)
    {
        _sc = m_sc;
        m_current = current;
    }
}
[System.Serializable]
public enum Source_type
{
    money,
    people,
    food,
}
[System.Serializable]
public class PlayerData
{
    public List<Source> Storge;
    int y;
    public int Get_Amount(Source_type _type)
    {            
        foreach (var item in Storge)
        {
            if (item._sc == _type)
            {
                y = item.m_current;
            }
        }
        return y;
    }
    public void AddValue(Source_type _type,int _amount)
    {
        foreach (var item in Storge)
        {
            if (item._sc == _type)
            {
                item.m_current += _amount;
            }
        }
    }
}

public class test2 : MonoBehaviour
{
    [SerializeField] 
    public  PlayerData m_player = new PlayerData();
    void Start()
    {
        Debug.Log(m_player.Get_Amount(Source_type.money));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
           m_player.AddValue(Source_type.people,5);         
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            m_player.AddValue(Source_type.people, 3);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_player.AddValue(Source_type.money, 1);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(m_player.Get_Amount(Source_type.money).ToString() + m_player.Get_Amount(Source_type.food).ToString() + m_player.Get_Amount(Source_type.people).ToString());
        }
    }
}
