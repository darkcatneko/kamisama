using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneDataCenter : MonoBehaviour
{
    public SaveScriptableObject Player_save;
    public Dialogue_Data_Object dialogue_Data_Object;
    public static MainSceneDataCenter instance;
    public Player_status status = Player_status.FreeMove;

    public GameObject Flag1;
    public GameObject Flag2;
    public GameObject Flag3;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CheckFlag();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        Player_save.Clear();
    }
    public void CheckFlag()
    {
        if (Player_save.FlagCount == 3)
        {
            Flag3.GetComponent<Image>().DOFade(1, 1f);
            Flag2.GetComponent<Image>().DOFade(1, 1f);
            Flag1.GetComponent<Image>().DOFade(1, 1f);
        }
        else if (instance.Player_save.FlagCount == 2)
        {
            Flag3.GetComponent<Image>().DOFade(1, 1f);
            Flag2.GetComponent<Image>().DOFade(1, 1f);
            Flag1.GetComponent<Image>().DOFade(0, 1f);
        }
        else if (instance.Player_save.FlagCount == 1)
        {
            Flag3.GetComponent<Image>().DOFade(1, 1f);
            Flag2.GetComponent<Image>().DOFade(0, 1f);
            Flag1.GetComponent<Image>().DOFade(0, 1f);
        }
        else
        {
            Flag3.GetComponent<Image>().DOFade(0, 1f);
            Flag2.GetComponent<Image>().DOFade(0, 1f);
            Flag1.GetComponent<Image>().DOFade(0, 1f);
        }
    }
}
[System.Serializable]
public enum Player_status
{
    FreeMove,
    ButtonClicked,
    Setting,
}

