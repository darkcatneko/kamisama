using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkillVFX : MonoBehaviour
{
    public void SpawnSkillAnimation()
    {
        Instantiate(MainBattleSystem.instance.battleAnimationContents.BattleEffect, Vector3.zero, Quaternion.identity);
    }
}
