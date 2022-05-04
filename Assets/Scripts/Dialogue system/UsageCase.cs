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
    //兩個選項
    public GameObject ChoicePanel_2;
    public Button Choice2_1;
    public Button Choice2_2;

    public List<NarrationCharacter> CharactersLists;

    void Start()
    {
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
            //ReadTextDataFromAsset(textAsset);

        //add special chars and functions in other component.
        msgSys.AddSpecialCharToFuncMap("UsageCase", CustomizedFunction);
        //進選項
        msgSys.AddSpecialCharToFuncMap("InChoise2", () => IntoChoiceSection_2(textAsset.name));
        //旁白
        msgSys.AddSpecialCharToFuncMap("N", NarrationSet);
        //尊王
        msgSys.AddSpecialCharToFuncMap("God_basic", () => ChangeMainSpeaker(CharactersLists[0], Emoji.smile));
    }
    public void IntoChoiceSection_2(string _PlotName)
    {
        ChoicePanel_2.SetActive(true);
        switch (_PlotName)
        {
            case "Opening":
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
        m_MainC_Image.enabled = true;
        ui_speaker.text = _MC.CharacterName;
        V_player.clip = _MC.Find_Clip(_emo);
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReadTextDataFromAsset(textAsset);
        }
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
}