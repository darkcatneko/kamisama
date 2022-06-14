using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RemptyTool.ES_MessageSystem;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;

[RequireComponent(typeof(ES_MessageSystem))]
public class UsageCase : MonoBehaviour
{
    private bool On_choice = false;
    private bool On_VideoPlay = false;
    private GameObject BlackScreen;
    private ES_MessageSystem msgSys;
    public TextMeshProUGUI uiText;
    public TextAsset textAsset;
    public SpriteRenderer Background;
    [SerializeField]
    private List<string> textList = new List<string>();
    [SerializeField]
    private int textIndex = 0;
    public VideoPlayer V_player;
    public RawImage m_MainC_Image;
    public TextMeshProUGUI ui_speaker;

    public RenderTexture bug;
    //兩個選項
    public GameObject ChoicePanel_2;
    public Button Choice2_1;
    public Button Choice2_2;

    public List<NarrationCharacter> CharactersLists;
    public Dialogue_Data_Object dialogueOBJ;
    public SceneControllerOBJ sceneOBJ;
    public SaveScriptableObject _playerSave;
    public SaveScriptableObject AutoSave;
    public BossDatabase bossdata;
    public GameObject BgmBlock;
    void Start()
    {

        msgSys = this.GetComponent<ES_MessageSystem>();
        _playerSave.Load();
        _playerSave.Now_Playing_Scene = 1;
        textAsset = FindNotePad(dialogueOBJ.The_NodePad_Be_read);
        msgSys.AddSpecialCharToFuncMap("BadEnd", () => { SceneManager.LoadScene(0); });
        msgSys.AddSpecialCharToFuncMap("IntoBattle", () => { IntoBattle(); });
        msgSys.AddSpecialCharToFuncMap("First", () => { FirstDialogue(); });
        //背景BGM宣告
        msgSys.AddSpecialCharToFuncMap("normalBGM", () => { ChangeBgm("DialogueBgm/Lights"); });
        msgSys.AddSpecialCharToFuncMap("FunBGM", () => { ChangeBgm("DialogueBgm/Girls's_Lunch_Royalty"); });
        msgSys.AddSpecialCharToFuncMap("MainTempleBGM", () => { ChangeBgm("DialogueBgm/chinese"); });
        msgSys.AddSpecialCharToFuncMap("SadStoryBGM", () => { ChangeBgm("DialogueBgm/SAD"); });
        msgSys.AddSpecialCharToFuncMap("AnotherStartBGM", () => { ChangeBgm("DialogueBgm/Confident_Beginning"); });
        msgSys.AddSpecialCharToFuncMap("HeartBreakingBGM", () => { ChangeBgm("DialogueBgm/Anxious"); });
        //背景指令碼宣告
        msgSys.AddSpecialCharToFuncMap("test", () => { ChangeBackground("test"); });
        msgSys.AddSpecialCharToFuncMap("BG_Sen_Nong", () => { ChangeBackground("BG_Sen_Nong"); });
        msgSys.AddSpecialCharToFuncMap("BG_Wood_Carving", () => { ChangeBackground("BG_Wood_Carving"); });
        msgSys.AddSpecialCharToFuncMap("BG_Bench", () => { ChangeBackground("BG_Bench"); });
        msgSys.AddSpecialCharToFuncMap("BG_Market", () => { ChangeBackground("BG_Market"); });
        msgSys.AddSpecialCharToFuncMap("BG_Bar", () => { ChangeBackground("BG_Bar"); });
        msgSys.AddSpecialCharToFuncMap("BG_Unagi", () => { ChangeBackground("BG_Unagi"); });
        msgSys.AddSpecialCharToFuncMap("BG_Yong_Chuan", () => { ChangeBackground("BG_Yong_Chuan"); });
        msgSys.AddSpecialCharToFuncMap("BG_Loli_Home", () => { ChangeBackground("BG_Loli_Home"); });
        msgSys.AddSpecialCharToFuncMap("BG_Loli_Home_inside", () => { ChangeBackground("BG_Loli_Home_inside"); });
        msgSys.AddSpecialCharToFuncMap("BG_Yao_Wan", () => { ChangeBackground("BG_Yao_Wan"); });
        msgSys.AddSpecialCharToFuncMap("BG_Fong_Shin", () => { ChangeBackground("BG_Fong_Shin"); });
        msgSys.AddSpecialCharToFuncMap("BG_Kin_hua", () => { ChangeBackground("BG_Kin_hua"); });
        msgSys.AddSpecialCharToFuncMap("BG_Ron_Xiu", () => { ChangeBackground("BG_Ron_Xiu"); });
        msgSys.AddSpecialCharToFuncMap("BG_Office", () => { ChangeBackground("BG_Office"); });
        msgSys.AddSpecialCharToFuncMap("BG_Res_Area", () => { ChangeBackground("BG_Res_Area"); });
        msgSys.AddSpecialCharToFuncMap("BG_Black", () => { ChangeBackground("BG_Black"); });
        //結束新手劇情
        msgSys.AddSpecialCharToFuncMap("OldGame", ()=> { _playerSave.Newtogame = false; _playerSave.Save(); });
        //結束劇情
        msgSys.AddSpecialCharToFuncMap("END", EndDialogue);
        //add special chars and functions in other component.
        msgSys.AddSpecialCharToFuncMap("UsageCase", CustomizedFunction);
        //進選項
        msgSys.AddSpecialCharToFuncMap("InChoise2", () => IntoChoiceSection_2(textAsset.name));
        //旁白
        msgSys.AddSpecialCharToFuncMap("N", NarrationSet);
        //尊王
        msgSys.AddSpecialCharToFuncMap("God_smile", () => ChangeMainSpeaker(CharactersLists[0], Emoji.smile));
        msgSys.AddSpecialCharToFuncMap("God_happy", () => ChangeMainSpeaker(CharactersLists[0], Emoji.happy));
        msgSys.AddSpecialCharToFuncMap("God_serious", () => ChangeMainSpeaker(CharactersLists[0], Emoji.serious));
        msgSys.AddSpecialCharToFuncMap("God_angry", () => ChangeMainSpeaker(CharactersLists[0], Emoji.angry));
        msgSys.AddSpecialCharToFuncMap("God_sad", () => ChangeMainSpeaker(CharactersLists[0], Emoji.sad));
        msgSys.AddSpecialCharToFuncMap("God_nervous", () => ChangeMainSpeaker(CharactersLists[0], Emoji.nervous));
        msgSys.AddSpecialCharToFuncMap("God_shy", () => ChangeMainSpeaker(CharactersLists[0], Emoji.shy));
        msgSys.AddSpecialCharToFuncMap("God_doya", () => ChangeMainSpeaker(CharactersLists[0], Emoji.doya));
        msgSys.AddSpecialCharToFuncMap("God_shock", () => ChangeMainSpeaker(CharactersLists[0], Emoji.shock));
        msgSys.AddSpecialCharToFuncMap("God_say", () => ChangeMainSpeaker(CharactersLists[0], Emoji.say));
        msgSys.AddSpecialCharToFuncMap("God_hate", () => ChangeMainSpeaker(CharactersLists[0], Emoji.hate));
        //過場
        msgSys.AddSpecialCharToFuncMap("BlackScreenIn", () => GenBlackScreen_1());
        msgSys.AddSpecialCharToFuncMap("BlackScreenOut", () => GenBlackScreen_2());
        //無臉角色
        msgSys.AddSpecialCharToFuncMap("MainCharacter_nomal", () => ChangeMainSpeakerWithNoLive2D("你"));
        msgSys.AddSpecialCharToFuncMap("UncleChen_nomal", () => ChangeMainSpeakerWithNoLive2D("陳叔"));
        msgSys.AddSpecialCharToFuncMap("Boss_day1", () => ChangeMainSpeakerWithNoLive2D("野鬼"));
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
        ReadTextDataFromAssetLoading(textAsset, dialogueOBJ.WhichLineItRead);
        if (dialogueOBJ.TheBackGroundPic!=""&&dialogueOBJ.TheBackGroundPic!=null)//回憶背景圖片
        {
            Debug.Log(dialogueOBJ.TheBackGroundPic);
            ChangeBackground(dialogueOBJ.TheBackGroundPic);
        }
        if (dialogueOBJ.TheBackGroundMusic!="" && dialogueOBJ.TheBackGroundMusic != null)
        {
            ChangeBgm("DialogueBgm/" + dialogueOBJ.TheBackGroundMusic);
        }
        if (dialogueOBJ.NowSpeaker != "" && dialogueOBJ.NowSpeaker != null)
        {
            ui_speaker.text = dialogueOBJ.NowSpeaker;
        }
        dialogueOBJ.Clear();        
    }
    public void IntoChoiceSection_2(string _PlotName)
    {
        ChoicePanel_2.SetActive(true);
        switch (_PlotName)
        {
            case "Opening":
                SetChoiceButtonFunction(Choice2_1, "總之先問問她怎麼了", "0-1-A", 10);
                SetChoiceButtonFunction(Choice2_2, "就這麼繞過去", "0-1-B", 0);
                return;
            case "Loli_Home_day_1":
                SetChoiceButtonFunction(Choice2_1, "鬼是為了隱藏在人群中嗎？", "Loli_Home_day_1_A", 10);
                SetChoiceButtonFunction(Choice2_2, "我太不懂為什麼……", "Loli_Home_day_1_B", 0);
                return;
            case "1-3":
                SetChoiceButtonFunction(Choice2_1, "「很多鬼魂……很多業績是吧……」", "1-3-A", 10);
                SetChoiceButtonFunction(Choice2_2, "「我要過勞死了……」", "1-3-B", 0);
                return;
            default:
                return;
        }
    }
    public void SetChoiceButtonFunction(Button _target,string _choice,string _ConnectPlotName,int _love_it_get)
    {
        On_choice = true;
        _target.GetComponentInChildren<Text>().text = _choice;
        _target.onClick.AddListener(()=> 
        { 
            textAsset = (Resources.Load<TextAsset>("TextAsset/" + _ConnectPlotName)); 
            On_choice = false; msgSys.Next(); ReadTextDataFromAsset(textAsset);
            _playerSave.m_Player.Gain_Love_Level(_love_it_get);
            Choice2_1.onClick.RemoveAllListeners();
            Choice2_2.onClick.RemoveAllListeners();
            ChoicePanel_2.SetActive(false);
        });           
    }
    private void FirstDialogue()
    {        
        dialogueOBJ.The_NodePad_Be_read = "Opening";
        dialogueOBJ.WhichLineItRead = 0;
        SceneManager.LoadScene(1);
    }
    private void ChangeBgm(string _BGM)
    {
        if (BgmBlock == null)
        {
            BgmBlock = Instantiate(Resources.Load<GameObject>(_BGM), transform.position, Quaternion.identity);
        }
        else if (BgmBlock.name != _BGM && BgmBlock != null) 
        {
            Destroy(BgmBlock);
            BgmBlock = null;
            BgmBlock = Instantiate(Resources.Load<GameObject>(_BGM), transform.position, Quaternion.identity);
        }        
    }
    private void ChangeBackground(string _picname)
    {
        Background.sprite = Resources.Load<Sprite>("DialogueBackground/"+_picname);
        dialogueOBJ.TheBackGroundPic = _picname;
    }
    private void ChangeMainSpeaker(NarrationCharacter _MC,Emoji _emo)
    {
        StopAllCoroutines();
        object[] _obj = new object[2] { _MC,_emo};
        StartCoroutine("ChangeMainSpeakerAnimate", _obj);
    }
    private void ChangeMainSpeakerWithNoLive2D(string _name)
    {
        ui_speaker.text = _name;        
    }
    public IEnumerator ChangeMainSpeakerAnimate(object[] _obj)
    {
        ClearOutRenderTexture(bug);
        On_VideoPlay = true;
        NarrationCharacter _MC = (NarrationCharacter)_obj[0];
        Emoji _emo = (Emoji)_obj[1];

        V_player.isLooping = false;
        V_player.clip = _MC.Find_Clip(_emo);        
        m_MainC_Image.enabled = true;
        V_player.Play();
        ui_speaker.text = _MC.CharacterName;
        yield return new WaitForSeconds((float)_MC.Find_Clip(_emo).length+0.05f);
        On_VideoPlay = false;
        V_player.isLooping = true; 
        V_player.clip = _MC.Find_LoopClip(_emo);
        V_player.Play();       
    }
    private void NarrationSet()
    {
        ui_speaker.text = "";
        m_MainC_Image.enabled = false;
    }
    private void CustomizedFunction()
    {
        Debug.Log("Hi! This is called by CustomizedFunction!");
    }    
    public void ReadTextDataFromAsset(TextAsset _textAsset)
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = 0;
        var lineTextData = _textAsset.text.Split('\n');
        foreach (string line in lineTextData)
        {
            textList.Add(line);
        }
    }
    public void ReadTextDataFromAssetLoading(TextAsset _textAsset,int _line)
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = _line;
        var lineTextData = _textAsset.text.Split('\n');
        foreach (string line in lineTextData)
        {
            textList.Add(line);
        }
    }
    public NarrationCharacter FindCharacter(List<NarrationCharacter> _list, int _id)
    {
        NarrationCharacter _my_c;
        foreach (var item in _list)
        {
            if (item.m_CharacterID == _id)
            {
                _my_c = item;
                return _my_c;
            }
        }
        return null;
    }
    public void GenBlackScreen_1()
    {
        NarrationSet();
        On_choice = true;
        BlackScreen = Instantiate(Resources.Load<GameObject>("DialogueBlackScreen/BlackScreen"), transform.position, Quaternion.identity);
        Invoke("EndBlackScreen", 1.5f);
    }
    public void EndBlackScreen()
    {
        On_choice = false;
        msgSys.Next();        
    }
    public void GenBlackScreen_2()
    {
        NarrationSet();
        On_choice = true;
        BlackScreen.GetComponent<Animator>().SetTrigger("CanFade");
        Invoke("EndBlackScreen", 1.5f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //You can sending the messages from strings or text-based files.
            if (msgSys.IsCompleted)
            {
                msgSys.SetText("Send the messages![lr] HelloWorld![w]");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && On_choice == false && On_VideoPlay == false)
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }

        //If the message is complete, stop updating text.
        if (msgSys.IsCompleted == false)
        {
            uiText.text = msgSys.text;
        }

        //Auto update from textList.
        if (msgSys.IsCompleted == true && textIndex < textList.Count)
        {
            msgSys.SetText(textList[textIndex]);            
            textIndex++;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            EndDialogue();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            IntoChoiceSection_2(textAsset.name);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            IntoBattle();
        }
    }
    public void NextSentence()
    {
        if (On_choice == false && On_VideoPlay == false)
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }
    }
    public void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }
    public TextAsset FindNotePad(string _str)
    {
        TextAsset T;
        T = Resources.Load<TextAsset>("TextAsset/" + _str);
        return T;
    }
    public void EndDialogue()
    {
        _playerSave.Save();
        SceneManager.LoadScene(2);        
    }
    public void IntoBattle()
    {        
        switch (textAsset.name)
        {
            case "1-2":
                sceneOBJ.NextBoss = bossdata.GetBossInformation(0);
                _playerSave.Save();
                SceneManager.LoadScene(4);
                return;
            case "2-2":
                
                return;
            case "3-2":
               
                return;
            case "4-2":

                return;
            case "5-2":

                return;
            case "6-2":

                return;
            case "7-2":

                return;
            default:
                return;
        }
    }
    public void LoadingButtonPress()
    {
        dialogueOBJ.TheBackGroundPic = Background.GetComponent<SpriteRenderer>().sprite.name;
        dialogueOBJ.TheBackGroundMusic = BgmBlock.GetComponent<AudioSource>().clip.name;
        dialogueOBJ.The_NodePad_Be_read = textAsset.name;
        dialogueOBJ.WhichLineItRead = textIndex;
        dialogueOBJ.NowSpeaker = ui_speaker.text;
        sceneOBJ.LastScene = 1;
        sceneOBJ.Status = FileStatus.choosingPlay;
        SceneManager.LoadScene(3);
    }
    public void SavingButtonPress()
    {
        dialogueOBJ.TheBackGroundPic = Background.GetComponent<SpriteRenderer>().sprite.name;
        dialogueOBJ.TheBackGroundMusic = BgmBlock.GetComponent<AudioSource>().clip.name;
        dialogueOBJ.The_NodePad_Be_read = textAsset.name;
        dialogueOBJ.WhichLineItRead = textIndex;
        dialogueOBJ.NowSpeaker = ui_speaker.text;
        sceneOBJ.LastScene = 1;
        sceneOBJ.Status = FileStatus.choosingSave;
        _playerSave.Now_Speaker = dialogueOBJ.NowSpeaker;
        _playerSave.Now_BackgroundPic = dialogueOBJ.TheBackGroundPic;
        _playerSave.Now_BGM = dialogueOBJ.TheBackGroundMusic;
        _playerSave.Now_Watching_Sentence = dialogueOBJ.WhichLineItRead;
        _playerSave.Now_Watching_Plot = dialogueOBJ.The_NodePad_Be_read;
        _playerSave.IfSpecialTime = dialogueOBJ.IfSpecialToChat;
        _playerSave.TempNote = dialogueOBJ.TempSaveNodePad;
        _playerSave.Save();
        SceneManager.LoadScene(3);
    }
    public void BackToTitle()
    {
        dialogueOBJ.DeepClear();
        SceneManager.LoadScene(0);
    }
    private void OnApplicationQuit()
    {
        _playerSave.Clear();
        dialogueOBJ.DeepClear();
    }
}
