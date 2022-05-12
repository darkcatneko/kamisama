using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainSceneDataCenter : MonoBehaviour
{
    
    public SaveScriptableObject Player_save;
    public SkillDatabaseOBJ m_SkillDatabaseOBJ;
    public Dialogue_Data_Object dialogue_Data_Object;
    public SceneControllerOBJ sceneOBJ;
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
        DOTween.Clear(true);
        Player_save.Load();
        Player_save.Now_Playing_Scene = 2;
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
            Flag3.GetComponent<Image>().color = new Color(1,1,1,1);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (instance.Player_save.FlagCount == 2)
        {
            Flag3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag1.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else if (instance.Player_save.FlagCount == 1)
        {
            Flag3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Flag1.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            Flag3.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Flag1.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
    public void LoadingButtonPress()
    {
        sceneOBJ.LastScene = 2;
        sceneOBJ.Status = FileStatus.choosingPlay;
        Player_save.Save();
        SceneManager.LoadScene(3);
    }
    public void SavingButtonPress()
    {
        sceneOBJ.LastScene = 2;
        sceneOBJ.Status = FileStatus.choosingSave;
        Player_save.Save();
        SceneManager.LoadScene(3);
    }
    public void TitleButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
[System.Serializable]
public enum Player_status
{
    FreeMove,
    ButtonClicked,
    Setting,
}

