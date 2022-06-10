using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUpdate : MonoBehaviour
{
    public static BossHealthUpdate instance;
    public float TempBossHealth;
    public Image BossFillTwo;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {         
        this.GetComponent<Slider>().value =TempBossHealth/MainBattleSystem.instance.ThisBoss.BossMaxHealth;
    }
}
