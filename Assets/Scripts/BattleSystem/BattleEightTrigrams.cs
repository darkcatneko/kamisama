using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEightTrigrams : MonoBehaviour
{
    public EightTrigrams ThisPlate;
    private void OnMouseUp()
    {
        MainBattleSystem.instance.NowFocusTrigrams = ThisPlate;
    }
}
