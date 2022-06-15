using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattle : MonoBehaviour
{
    public void BossDieEnd()
    {
        switch(MainBattleSystem.instance.m_player.TimeSaveData.day)
        {
            case 2:
                MainBattleSystem.instance.IntoDialogueScene("1-3");
                return;
        }        

    }
}
