using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_scene_UI : MonoBehaviour
{
    public static Loading_scene_UI instance; //�W��

    //�s�@�T�h���Y
    public GameObject Buttons;
    public GameObject SaveFiles;
    public GameObject Setting;
    //���q�վ�
    public Slider Sound;
    public AudioSource audioSource;
    public float Volume;
    //�O�_�ҥ�autosave
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
    public void NewGameButtonClicked()//���g
    {

    }
    public void SaveDataClicked()//���g
    {

    }
    public void SceneChangeTest()//���ե� �������|�R��
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
