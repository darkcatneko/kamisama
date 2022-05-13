using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public enum FileStatus
{
    choosingPlay,
    choosingSave,
}
public class FileSelectingUIController : MonoBehaviour
{
    public int page = 1;

    public List<Button> FileButtons;
    public List<Button> RemoveButtons;
    public List<TextMeshProUGUI> Level;
    public List<TextMeshProUGUI> GameDay;
    public List<TextMeshProUGUI> GameTime;
    public List<TextMeshProUGUI> systemTime;
    public List<SaveScriptableObject> FileCabnit;
    public SaveScriptableObject GameUseData;
    public SaveScriptableObject NewGameUseData;

    public Dialogue_Data_Object dialogueOBJ;
    public SceneControllerOBJ sceneOBJ;
    private void Start()
    {
        foreach (var item in FileCabnit)
        {
            item.Load();
        }
        for (int i = 0; i < FileButtons.Count; i++)
        {
            ButtonFunctionSet(i);
            SetInformationVer2(i);
            RemoveButtonSet(i);
        }
        GameUseData.Load();
    }
    public void SelectThisFile(int _id)
    {
        if (sceneOBJ.Status == FileStatus.choosingPlay)
        {
            GameUseData.EqualFunction(GameUseData, FileCabnit[_id]);
            GameUseData.Save();
            if (GameUseData.Newtogame == true)
            {
                GameUseData.Clear();
                GameUseData.EqualFunction(GameUseData, NewGameUseData);
                GameUseData.Save();
                dialogueOBJ.The_NodePad_Be_read = "Opening";
                dialogueOBJ.WhichLineItRead = 0;
                SceneManager.LoadScene(1);
            }
            else
            {                
                switch (GameUseData.Now_Playing_Scene)
                {
                    case 1:
                        dialogueOBJ.The_NodePad_Be_read = GameUseData.Now_Watching_Plot;
                        dialogueOBJ.WhichLineItRead = GameUseData.Now_Watching_Sentence - 1;
                        SceneManager.LoadScene(GameUseData.Now_Playing_Scene);
                        return;
                    case 2:
                        SceneManager.LoadScene(2);
                        return;
                }
            }
        }
        else if(sceneOBJ.Status == FileStatus.choosingSave)
        {
            FileCabnit[_id].Clear();
            FileCabnit[_id].EqualFunction(FileCabnit[_id], GameUseData);
            FileCabnit[_id].SaveDate = System.DateTime.Now.Year.ToString()+ " / "+ System.DateTime.Now.Month.ToString() + " / " + System.DateTime.Now.Day.ToString();
            FileCabnit[_id].Save();
            FileCabnit[_id].Load();
            SetInformationVer2(_id);                
        }
    }
    public void ButtonFunctionSet(int _id)
    {
        FileButtons[_id].onClick.AddListener(() => SelectThisFile(_id));
    }
    public void SetInformationVer2(int _id)
    {
        Level[_id].text = FileCabnit[_id].m_Player.Level.ToString();
        GameDay[_id].text = "Day" + FileCabnit[_id].TimeSaveData.day.ToString();
        GameTime[_id].text = "/ " + FileCabnit[_id].TimeSaveData.time.ToString() + ":00";
        systemTime[_id].text = FileCabnit[_id].SaveDate;
    }
    public void RemoveButtonSet(int _id)
    {
        RemoveButtons[_id].onClick.AddListener(() => { FileCabnit[_id].EqualFunction(FileCabnit[_id], NewGameUseData);FileCabnit[_id].Save(); SetInformationVer2(_id); });
    }
    public void ExitButtonClick()
    {
        if (sceneOBJ.LastScene == 1)
        {
            dialogueOBJ.WhichLineItRead--;
            SceneManager.LoadScene(1);
        }
        else if (sceneOBJ.LastScene == 2)
        {
            
            SceneManager.LoadScene(2);
        }
        else if (sceneOBJ.LastScene == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void OnNextPageClick()
    {
        foreach (var item in FileCabnit)
        {
            item.Load();
        }
        for (int i = 0; i < FileButtons.Count; i++)
        {
            ButtonFunctionSet(i+6);
            SetInformationVer2(i+6);
            RemoveButtonSet(i+6);
        }
        GameUseData.Load();
    }
    public void OnLastPageClick ()
    {
        foreach (var item in FileCabnit)
        {
            item.Load();
        }
        for (int i = 0; i < FileButtons.Count; i++)
        {
            ButtonFunctionSet(i);
            SetInformationVer2(i);
            RemoveButtonSet(i);
        }
        GameUseData.Load();
    }
}
