using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_scene_UI : MonoBehaviour
{
    public static Loading_scene_UI instance; //獨體

    //製作三層關係
    public GameObject Buttons;
    public GameObject SaveFiles;
    public GameObject Setting;
    //音量調整
    public Slider Sound;
    public AudioSource audioSource;
    public float Volume;
    //是否啟用autosave
    public bool AutoSave = true;
    public Toggle CheckBox;
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
        AutoSaveInformationUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToMainScreen();
        }
    }
    public void AutoSaveInformationUpdate()
    {
        AutoSave = CheckBox.isOn;
    }
    public void VolumeUpdate()
    {
        Volume = Sound.value;
        audioSource.volume = Volume;
    }
    public void NewGameButtonClicked()//未寫
    {

    }
    public void SaveDataClicked()//未寫
    {

    }
    public void SceneChangeTest()//測試用 正式版會刪掉
    {
        SceneManager.LoadScene(1);
    }
    public void PlayButtonClicked()
    {
        Buttons.SetActive(false);
        SaveFiles.SetActive(true);
    }
    public void SettingButtonClicked()
    {
        Buttons.SetActive(false);
        Setting.SetActive(true);
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
    public void ToMainScreen()
    {
        Buttons.SetActive(true);
        SaveFiles.SetActive(false);
        Setting.SetActive(false);
    }
}
