using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Narration/Character")]
public class NarrationCharacter : ScriptableObject
{
    [SerializeField]
    private string m_CharacterName;
    [SerializeField]
    public int m_CharacterID;
    [SerializeField]
    public CharacterImage[] ImageList;
    public string CharacterName => m_CharacterName;
    public Sprite Find_Pic(Emoji _emoji)
    {
        Sprite finalresult = null;
        foreach (var item in ImageList)
        {
            if (item.m_Emoji == _emoji)
            {
                finalresult = item.FacePNG;
                return finalresult;
            }
        }
        return finalresult;
    }    

}
[System.Serializable]
public enum Emoji
    {
       smile,
       whisper,
       nervous,
       sigh,
       disgust,
       promise,
       cover_sun,
       happy,
       Pout,
       doya,
       majime,
       thinking,
       wordless,
}
[System.Serializable]
public class CharacterImage
{
    public Sprite FacePNG;
    public Emoji m_Emoji;
}
