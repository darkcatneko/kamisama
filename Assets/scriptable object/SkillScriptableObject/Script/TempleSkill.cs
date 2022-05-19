using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill/TempleSkill")]
public class TempleSkill : SkillScriptableObject
{
    public int MinLevel;
    public int NeedHowManySkillPoint;
    public Stats thisTemple;
    private void Awake()
    {
        m_Skilltype = SkillType.TempleSkill;
        Temple = thisTemple;
        LevelUpCondition[0] = MinLevel;
        LevelUpCondition[1] = NeedHowManySkillPoint;
    }
}
