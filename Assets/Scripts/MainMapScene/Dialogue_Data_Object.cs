using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue_data", menuName = "Dialogue_data_OBJ")]
public class Dialogue_Data_Object : ScriptableObject
{
    public bool IfSpecialToChat = false;
    public string The_NodePad_Be_read;
    public string TempSaveNodePad;
    public string TheBackGroundPic;
    public string TheBackGroundMusic;
    public string NowSpeaker;
    public int WhichLineItRead;
    public int Love_Point;
    [ContextMenu("Clear")]
    public void Clear()
    {
        TheBackGroundMusic = null;
        TheBackGroundPic = null;
        The_NodePad_Be_read = null;
        WhichLineItRead = 0;
        Love_Point = 0;
    }
    [ContextMenu("DeepClear")]
    public void DeepClear()
    {
        NowSpeaker = null;
        TheBackGroundMusic = null;
        TheBackGroundPic = null;
        TempSaveNodePad = null;
        IfSpecialToChat = false;
        The_NodePad_Be_read = null;
        WhichLineItRead = 0;
        Love_Point = 0;
    }
}
