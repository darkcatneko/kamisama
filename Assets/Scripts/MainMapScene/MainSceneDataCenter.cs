using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainSceneDataCenter : MonoBehaviour
{
    public GameObject VolumePanel;
    public SEmaster _semaster;

    public SaveScriptableObject Player_save;
    public SkillDatabaseOBJ m_SkillDatabaseOBJ;
    public Dialogue_Data_Object dialogue_Data_Object;
    public SceneControllerOBJ sceneOBJ;
    public static MainSceneDataCenter instance;
    public Player_status status = Player_status.FreeMove;

    public GameObject Flag1;
    public GameObject Flag2;
    public GameObject Flag3;

    public GameObject SkillPanel;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {        
        DOTween.Clear(true);
        Player_save.Load();
        CheckFlag();
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
            Flag1.GetComponent<Image>().color = new Color(1,1,1,1);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (instance.Player_save.FlagCount == 2)
        {
            Flag1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag3.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else if (instance.Player_save.FlagCount == 1)
        {
            Flag1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Flag3.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            Flag1.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Flag2.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Flag3.GetComponent<Image>().color = new Color(1, 1, 1, 0);
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
    public void VolumeButtonClicked()
    {
        if (VolumePanel.activeSelf == false)
        {
            MainSceneDataCenter.instance.status = Player_status.VolumeSetting;
            VolumePanel.SetActive(true);
        }
        else
        {
            MainSceneDataCenter.instance.status = Player_status.Setting;
            VolumePanel.SetActive(false);
        }
        
    }
    public void SkillButtonClick()
    {        
        if (status == Player_status.SkillInterface)
        {
            status = Player_status.FreeMove;
            SkillPanel.SetActive(false);
        }
        else if(status == Player_status.FreeMove)
        {
            status = Player_status.SkillInterface;
            SkillPanel.SetActive(true);
        }
    }
}
[System.Serializable]
public enum Player_status
{
    FreeMove,
    ButtonClicked,
    Setting,
    VolumeSetting,
    SkillInterface,
}

