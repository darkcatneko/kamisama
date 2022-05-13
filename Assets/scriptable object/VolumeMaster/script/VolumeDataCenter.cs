using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VolumeMaster", menuName = "VolumeMaster")]
public class VolumeDataCenter : ScriptableObject
{
    public float All = 1f;
    public float SoundEffect = 0.5f;
    public float BGM = 1f;

    public bool AutoSave = true;
    
}
