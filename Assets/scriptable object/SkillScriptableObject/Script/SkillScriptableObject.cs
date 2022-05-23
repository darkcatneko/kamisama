using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class SkillScriptableObject : ScriptableObject
{
    public Stats Temple;
    public SkillType m_Skilltype;
    public string SkillName;
    public int ID;
    public SkillVariable skillVariable;
    public EightTrigrams m_eightTrigrams;  
    public Stats stats;
    public float ManaCost;
    public Sprite SkillPic;
    public Sprite SkillCardPic;
    [TextArea(15, 20)]
    public string SkillDescribe;
    [TextArea(15, 20)]
    public string SkillLearnWay;
    public GameObject AnimationPrefab;
    public GameObject FieldPrefab;
    public int[] LevelUpCondition;
}
public enum SkillType
{
    BasicSkill,
    TempleSkill,
    SpecialSkill,
}
