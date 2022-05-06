﻿using System.Collections;
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

    private ES_MessageSystem msgSys;
    public TextMeshProUGUI uiText;
    public TextAsset textAsset;
    private List<string> textList = new List<string>();
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
    public SaveScriptableObject _playerSave;

    void Start()
    {
        msgSys = this.GetComponent<ES_MessageSystem>();

        textAsset = FindNotePad(dialogueOBJ.The_NodePad_Be_read);
        //add special chars and functions in other component.
        msgSys.AddSpecialCharToFuncMap("UsageCase", CustomizedFunction);
        //進選項
        msgSys.AddSpecialCharToFuncMap("InChoise2", () => IntoChoiceSection_2(textAsset.name));
        //旁白
        msgSys.AddSpecialCharToFuncMap("N", NarrationSet);
        //尊王
        msgSys.AddSpecialCharToFuncMap("God_basic", () => ChangeMainSpeaker(CharactersLists[0], Emoji.smile));
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
        ReadTextDataFromAsset(textAsset);
        dialogueOBJ.Clear();
    }
    public void IntoChoiceSection_2(string _PlotName)
    {
        ChoicePanel_2.SetActive(true);
        switch (_PlotName)
        {
            case "Loli_Home_day_1":
                SetChoiceButtonFunction(Choice2_1, "i choice a", "1-1");
                SetChoiceButtonFunction(Choice2_2, "i choice b", "1-2");
                return;
            default:
                return;
        }
    }
    public void SetChoiceButtonFunction(Button _target,string _choice,string _ConnectPlotName)
    {
        On_choice = true;
        _target.GetComponentInChildren<Text>().text = _choice;
        _target.onClick.AddListener(()=> 
        { 
            textAsset = (Resources.Load<TextAsset>("TextAsset/" + _ConnectPlotName)); 
            On_choice = false; msgSys.Next(); ReadTextDataFromAsset(textAsset);
            Choice2_1.onClick.RemoveAllListeners();
            Choice2_2.onClick.RemoveAllListeners();
            ChoicePanel_2.SetActive(false);
        });           
    }
    private void ChangeMainSpeaker(NarrationCharacter _MC,Emoji _emo)
    {
        object[] _obj = new object[2] { _MC,_emo};
        StartCoroutine("ChangeMainSpeakerAnimate", _obj);
    }
    public IEnumerator ChangeMainSpeakerAnimate(object[] _obj)
    {
        ClearOutRenderTexture(bug);
        NarrationCharacter _MC = (NarrationCharacter)_obj[0];
        Emoji _emo = (Emoji)_obj[1];

        V_player.isLooping = false;
        V_player.clip = _MC.Find_Clip(_emo);        
        m_MainC_Image.enabled = true;
        V_player.Play();
        ui_speaker.text = _MC.CharacterName;
        yield return new WaitForSeconds(2f);
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

        if (Input.GetKeyDown(KeyCode.Space) && On_choice == false)
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
}
