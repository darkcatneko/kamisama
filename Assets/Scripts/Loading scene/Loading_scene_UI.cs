using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_scene_UI : MonoBehaviour
{
    public static Loading_scene_UI instance; //獨體
    public SEmaster _semaster;

    //製作三層關係
    public GameObject Buttons;
    public GameObject Setting;
    //音量調整
    public Slider Sound;
    public AudioSource audioSource;
    //是否啟用autosave
    public bool AutoSave = true;
    //public Toggle CheckBox;
    //存檔櫃
    public SaveScriptableObject GameUseData;
    public SaveScriptableObject NewGameUseData;
    public Dialogue_Data_Object dialogueOBJ;
    public SceneControllerOBJ sceneOBJ;
    private void Awake()
    {
        if (this != null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        VolumeUpdate();
        //AutoSaveInformationUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToMainScreen();
        }
    }
    //public void AutoSaveInformationUpdate()
    //{
    //    AutoSave = CheckBox.isOn;
    //}
    public void VolumeUpdate()
    {
        _semaster.VolumeMaster.All = Sound.value;
        audioSource.volume = _semaster.VolumeMaster.All;
    }
    public void NewGameButtonClicked()
    {
        GameUseData.EqualFunction(GameUseData, NewGameUseData);
        GameUseData.Save();
        dialogueOBJ.The_NodePad_Be_read = "Opening";
        dialogueOBJ.WhichLineItRead = 0;
        SceneManager.LoadScene(1);
    }
    public void SaveDataClicked()//未寫
    {

    }
    public void SceneChangeTest()//測試用 正式版會刪掉
    {
        sceneOBJ.LastScene = 1;
        sceneOBJ.Status = FileStatus.choosingPlay;
        SceneManager.LoadScene(1);
    }
    public void PlayButtonClicked()
    {
        sceneOBJ.LastScene = 0;
        sceneOBJ.Status = FileStatus.choosingPlay;
        SceneManager.LoadScene(3);
    }
    public void SettingButtonClicked()
    {
        if (Setting.activeSelf == true)
        {
            Setting.SetActive(false);            
        }
        else
        {
            Setting.SetActive(true);
        }
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
    public void ToMainScreen()
    {
        Buttons.SetActive(true);
        Setting.SetActive(false);
    }
}
