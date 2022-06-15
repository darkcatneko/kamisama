using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TaiwanOni", menuName = "Boss/DayBoss/TaiwanOni")]
public class TaiwanOni : BossBase
{
    

    public  void TaiwanOni_AttackPattern()
    {
        int Pattern;
        Pattern = Random.Range(1, 3);
        switch(Pattern)
        {
            case 1: //撞擊
                MainBattleSystem.instance.BossSprite.GetComponent<Animator>().SetBool("ATK_1",true);//撥動畫
                MainBattleSystem.instance.BossSprite.GetComponent<BattleAnimationEvent>().BossEndAnimation.AddListener(() =>
                {
                    MainBattleSystem.instance.BossSprite.GetComponent<Animator>().SetBool("ATK_1", false);                    
                });               
                MainBattleSystem.instance.BossSprite.GetComponent<BattleAnimationEvent>().TheBossAttack.AddListener(()=> { m_base.BossDamageGen(Stats.ATK, 7, Stats.DEF); });
                return;
            case 2: //失神
                    //撥動畫
                MainBattleSystem.instance.BossSprite.GetComponent<Animator>().SetBool("ATK_2", true);//撥動畫
                MainBattleSystem.instance.BossSprite.GetComponent<BattleAnimationEvent>().BossEndAnimation.AddListener(() =>
                {
                    MainBattleSystem.instance.BossSprite.GetComponent<Animator>().SetBool("ATK_2", false);                    
                });
                MainBattleSystem.instance.BossSprite.GetComponent<BattleAnimationEvent>().TheBossAttack.AddListener(() => { m_base.BossDamageGen(Stats.SPI, 7, Stats.POW); });
                return;
        }
    }    
}
