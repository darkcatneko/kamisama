using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
[System.Serializable]
public enum FileStatus
{
    choosingPlay,
    choosingSave,
}
public class FileSelectingUIController : MonoBehaviour
{
    public int page = 1;public GameObject MiddlePoint;public Button Page1Button; public Button Page2Button;public Image RotateImage;
    public  int times;
    public SEmaster _semaster;

    public List<Button> FileButtons;
    public List<Button> RemoveButtons;
    public List<TextMeshProUGUI> Level;
    public List<TextMeshProUGUI> GameDay;
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
        Level[_id].text = "LV."+FileCabnit[_id].m_Player.Level.ToString();
        GameDay[_id].text = "Day" + FileCabnit[_id].TimeSaveData.day.ToString()+ " / " + FileCabnit[_id].TimeSaveData.time.ToString() + ":00";
        systemTime[_id].text = FileCabnit[_id].SaveDate;
    }
    public void RemoveButtonSet(int _id)
    {
        RemoveButtons[_id].onClick.AddListener(() => { FileCabnit[_id].EqualFunction(FileCabnit[_id], NewGameUseData);FileCabnit[_id].SaveDate = ""; FileCabnit[_id].Save(); SetInformationVer2(_id); });
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

    public void RotatePic()
    {
        RotateImage.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.5f);
    }
    public void OnPage1Click()
    {
       
        if (page == 2 )
        {
            DOTween.To(() => { return MiddlePoint.GetComponent<RectTransform>().anchoredPosition; }, v => { MiddlePoint.GetComponent<RectTransform>().anchoredPosition = v; }, new Vector2(0, 0), 0.5f);
            RotateImage.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);            
            Sprite temp = Page1Button.GetComponent<Image>().sprite;
            Page1Button.GetComponent<Image>().sprite = Page2Button.GetComponent<Image>().sprite;
            Page2Button.GetComponent<Image>().sprite = temp;
            page = 1;
        }
        
    }
    public void OnPage2Click ()
    {
        
        if (page == 1 )
        {
            DOTween.To(() => { return MiddlePoint.GetComponent<RectTransform>().anchoredPosition; }, v => { MiddlePoint.GetComponent<RectTransform>().anchoredPosition = v; }, new Vector2(-2260, 0), 0.5f);
            RotateImage.transform.DOLocalRotate(new Vector3(0, 0, 180 ), 0.5f);
            Sprite temp = Page1Button.GetComponent<Image>().sprite;
            Page1Button.GetComponent<Image>().sprite = Page2Button.GetComponent<Image>().sprite;
            Page2Button.GetComponent<Image>().sprite = temp;
            page = 2;
        }
        
    }
}
