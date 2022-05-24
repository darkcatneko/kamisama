using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationEvent : MonoBehaviour
{
    public void GenBattleEffect()
    {
        Instantiate(MainBattleSystem.instance.battleAnimationContents.BattleEffect, Vector3.zero, Quaternion.identity);
    }
    public void GenDamageNumber()
    {
        int Damage = MainBattleSystem.instance.battleAnimationContents.DamageDelt[MainBattleSystem.instance.battleAnimationContents.NowDisplayDamage];
        MainBattleSystem.instance.battleAnimationContents.NowDisplayDamage++;
        //怪物受傷反饋
        //怪物血條減少
    }
    public void RefreshAnimationContent()
    {
        MainBattleSystem.instance.battleAnimationContents = new BattleAnimationContents();
    }
}
