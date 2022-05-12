using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SkillDataBase")]
public class SkillDatabaseOBJ : ScriptableObject
{
    public List<SkillScriptableObject> SkillData;
    public SkillScriptableObject GetSkillInformation(int _id)
    {
        for (int i = 0; i < SkillData.Count; i++)
        {
            if (SkillData[i].ID == _id)
            {
                return SkillData[_id];
            }
        }
        return null;
    }
}
