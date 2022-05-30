using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "scene_obj", menuName = "SceneControllerOBJ")]
public class SceneControllerOBJ : ScriptableObject
{
    public int LastScene;
    public FileStatus Status;
    public BossBase NextBoss;
    public void Clear()
    {
        LastScene = 0;
        Status = FileStatus.choosingPlay;
    }

}
