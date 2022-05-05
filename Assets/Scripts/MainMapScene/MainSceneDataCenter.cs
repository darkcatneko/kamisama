using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneDataCenter : MonoBehaviour
{
    public SaveScriptableObject Player_save;
    public Dialogue_Data_Object dialogue_Data_Object;
    public static MainSceneDataCenter instance;
    public Player_status status = Player_status.FreeMove;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        Player_save.Clear();
    }
}
[System.Serializable]
public enum Player_status
{
    FreeMove,
    ButtonClicked,
}

