using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill")]
public class SkillScriptableObject : ScriptableObject
{
    public string SkillName;
    public int ID;
    public SkillVariable skillVariable;
    public EightTrigrams m_eightTrigrams;  
    public Stats stats;
    public float ManaCost;
    public Sprite SkillPic;
    [TextArea(15, 20)]
    public string SkillDescribe;
    public GameObject AnimationPrefab;
    public GameObject FieldPrefab;
}
