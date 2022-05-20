using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "Scriptable Object/Narration/Character")]
public class NarrationCharacter : ScriptableObject
{
    [SerializeField]
    private string m_CharacterName;
    [SerializeField]
    public int m_CharacterID;
    [SerializeField]
    public CharacterImage[] L2DList;
    public string CharacterName => m_CharacterName;
    public VideoClip Find_Clip(Emoji _emoji)
    {
        VideoClip finalresult = null;
        foreach (var item in L2DList)
        {
            if (item.m_Emoji == _emoji)
            {
                finalresult = item.clip;
                return finalresult;
            }
        }
        return finalresult;
    }
    public VideoClip Find_LoopClip(Emoji _emoji)
    {
        VideoClip finalresult = null;
        foreach (var item in L2DList)
        {
            if (item.m_Emoji == _emoji)
            {
                finalresult = item.Loopclip;
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
       shock,
       whisper,
       nervous,
       angry,
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
       shy,
}
[System.Serializable]
public class CharacterImage
{
    public VideoClip clip;
    public VideoClip Loopclip;
    public Emoji m_Emoji;
}
