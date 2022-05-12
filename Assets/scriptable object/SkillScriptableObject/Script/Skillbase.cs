using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillbase : MonoBehaviour
{
    
}
[System.Serializable]
public enum EightTrigrams
{
    Qian,
    Xun,
    Kan,
    Gen,
    Kun,
    Zhen,
    Li,
    Dui,
}
[System.Serializable]
public enum SkillVariable
{
    Instance,
    Field,
}
public delegate void CoolSkill(PlayerStats _stats);
