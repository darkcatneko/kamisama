using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEightTrigrams : MonoBehaviour
{
    public EightTrigrams ThisPlate;
    private void OnMouseUp()
    {
        if (MainBattleSystem.instance.ReadyAttack == false&&MainBattleSystem.instance.m_battleStatus == BattleStatus.ChooseEightTrigram)
        {
            MainBattleSystem.instance.NowFocusTrigrams = ThisPlate;
            MainBattleSystem.instance.ReadyAttack = true;
        }
    }
}
