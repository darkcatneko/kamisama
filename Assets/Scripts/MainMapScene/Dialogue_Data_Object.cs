using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue_data", menuName = "Dialogue_data_OBJ")]
public class Dialogue_Data_Object : ScriptableObject
{
    public string The_NodePad_Be_read;
    public int Love_Point;
    [ContextMenu("Clear")]
    public void Clear()
    {
         The_NodePad_Be_read = null;
         Love_Point = 0;
    }
}
