using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill/BasicSkill")]
public class BasicSkill : SkillScriptableObject
{
   public int UnlockLevel;
    private void Awake()
    {
        m_Skilltype = SkillType.BasicSkill;
        LevelUpCondition[0] = UnlockLevel;
    }
}
