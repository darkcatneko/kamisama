using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUpdate : MonoBehaviour
{
    public static PlayerHealthBarUpdate instance;
    public Image SecondBar;
    public int TempWhite;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        this.GetComponent<Slider>().value = (float)MainBattleSystem.instance.TempHP / (float)MainBattleSystem.instance.BattleUseStats.MaxHP;
        SecondBar.fillAmount = (float)TempWhite / (float)MainBattleSystem.instance.BattleUseStats.MaxHP;
    }
}
