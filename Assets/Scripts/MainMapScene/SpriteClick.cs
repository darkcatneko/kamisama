using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SpriteClick : MonoBehaviour
{
    public float fade = 1;
    public float UI_fade = 0;
    public Vector2 Exit_end;
    public Vector2 Buff_end;
    public Vector2 Chat_end;
    public Vector2 Skill_end;
    public Stats _Place;
    public string StatsGrowth;
    public SpriteRenderer Map_Place_name;
    
    private void Start()
    {
        Exit_end = SpriteClickAnimation.instance.Exit_Origin;
        Buff_end = SpriteClickAnimation.instance.Buff_Origin;
        Chat_end = SpriteClickAnimation.instance.Chat_Origin;
        Skill_end = SpriteClickAnimation.instance.Skill_Origin;
        FlagPicUpdate();
    }
    private void Update()
    {
        this.GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);

    }
    private void OnMouseEnter()
    {
        if (MainSceneDataCenter.instance.status == Player_status.FreeMove)
        {
            Map_Place_name.DOFade(1, 0.3f);
            this.transform.DOScale(new Vector3(1.2f, 1.2f, 0), 0.3f);
        }
        
    }
    private void OnMouseExit()
    {       
            Map_Place_name.DOFade(0, 0.3f);
            this.transform.DOScale(new Vector3(1f, 1f, 0), 0.3f);                
    }

    private void OnMouseUp()
    {
        if (camScroll.instance.startPoint == camScroll.instance.endPoint&&MainSceneDataCenter.instance.status == Player_status.FreeMove)
        {         
            StartCoroutine("ButtonClickedAnimation");

            MainSceneDataCenter.instance._semaster.GenSE();
        }
    }
    public IEnumerator ButtonClickedAnimation()
    {
        MainSceneDataCenter.instance.status = Player_status.ButtonClicked;
        //DOTween.To(() => fade, x => fade = x, 0f, 1f);
        this.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 1f),0.4f,1);
        yield return new WaitForSeconds(0.4f);
        SpriteClickAnimation.instance.BlackScreen.SetActive(true);
        SpriteClickAnimation.instance.panel.DOFade(0.76f, 1f);
        SpriteClickAnimation.instance.Map_little_icon.sprite = this.GetComponent<SpriteRenderer>().sprite;
        SpriteClickAnimation.instance.Map_Pictures.GetComponent<Image>().sprite = Resources.Load<Sprite>("MapRealPicture/RealPic" + (int)_Place);
        SpriteClickAnimation.instance.Map_icon_name.GetComponent<Image>().sprite = Resources.Load<Sprite>("MapNamePic/NamePic" + (int)_Place);
        DOTween.To(() => { return SpriteClickAnimation.instance.Map_Pictures.GetComponent<RectTransform>().anchoredPosition; }, v => { SpriteClickAnimation.instance.Map_Pictures.GetComponent<RectTransform>().anchoredPosition = v; }, SpriteClickAnimation.instance.Map_Pictures_end, 1f);
        DOTween.To(() => SpriteClickAnimation.instance.Skill_Input, x => SpriteClickAnimation.instance.Skill_Input = x, SpriteClickAnimation.instance.Skill_end, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Exit_Input, x => SpriteClickAnimation.instance.Exit_Input = x, SpriteClickAnimation.instance.Exit_end, 1.5f);
        SpriteClickAnimation.instance.ExitButton.GetComponent<Button>().onClick.AddListener(() =>
        {
        if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
            {
            StartCoroutine("ExitButtonClickedAnimation",1);
            }            
        });
        DOTween.To(() => SpriteClickAnimation.instance.Buff_Input, x => SpriteClickAnimation.instance.Buff_Input = x, SpriteClickAnimation.instance.Buff_Middle, 1f);
        DOTween.To(() => SpriteClickAnimation.instance.Chat_Input, x => SpriteClickAnimation.instance.Chat_Input = x, SpriteClickAnimation.instance.Chat_Middle, 1f);
        DOTween.To(() => SpriteClickAnimation.instance.Flag_Input, x => SpriteClickAnimation.instance.Flag_Input = x, SpriteClickAnimation.instance.Flag_Middle, 1f);
        DOTween.To(() => SpriteClickAnimation.instance.Shader_Input, x => SpriteClickAnimation.instance.Shader_Input = x, 1f, 0.7f);
        yield return new WaitForSeconds(1f);
        SpriteClickAnimation.instance.Loading = true;
        SpriteClickAnimation.instance.BuffButton.SetActive(true);
        SpriteClickAnimation.instance.FlagButton.SetActive(true);
        SpriteClickAnimation.instance.ChatButton.SetActive(true);        
        DOTween.To(() => SpriteClickAnimation.instance.Buff_Input, x => SpriteClickAnimation.instance.Buff_Input = x, SpriteClickAnimation.instance.Buff_end, 0.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Chat_Input, x => SpriteClickAnimation.instance.Chat_Input = x, SpriteClickAnimation.instance.Chat_end, 0.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Flag_Input, x => SpriteClickAnimation.instance.Flag_Input = x, SpriteClickAnimation.instance.Flag_end, 0.5f);
        SetUpButttonImage(_Place);
        yield return new WaitForSeconds(0.5f);
        SetUPButtonFunction(_Place);        
        yield return null;
    }
    public IEnumerator ExitButtonClickedAnimation(int _i)
    {
        ClearFunction();                       
        DOTween.To(() => { return SpriteClickAnimation.instance.Map_Pictures.GetComponent<RectTransform>().anchoredPosition; }, v => { SpriteClickAnimation.instance.Map_Pictures.GetComponent<RectTransform>().anchoredPosition = v; }, SpriteClickAnimation.instance.Map_Pictures_origin, 1f);
        DOTween.To(() => SpriteClickAnimation.instance.Skill_Input, x => SpriteClickAnimation.instance.Skill_Input = x, SpriteClickAnimation.instance.Skill_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Exit_Input, x => SpriteClickAnimation.instance.Exit_Input = x, SpriteClickAnimation.instance.Exit_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Buff_Input, x => SpriteClickAnimation.instance.Buff_Input = x, SpriteClickAnimation.instance.Buff_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Chat_Input, x => SpriteClickAnimation.instance.Chat_Input = x, SpriteClickAnimation.instance.Chat_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Flag_Input, x => SpriteClickAnimation.instance.Flag_Input = x, SpriteClickAnimation.instance.Flag_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Shader_Input, x => SpriteClickAnimation.instance.Shader_Input = x, 0f, 1f);
        yield return new WaitForSeconds(_i);
        SpriteClickAnimation.instance.panel.DOFade(0f, 0.5f);
        MainSceneDataCenter.instance.status = Player_status.FreeMove;
        //DOTween.To(() => fade, x => fade = x, 1f, 1f);        
        SpriteClickAnimation.instance.Loading = false;
        yield return new WaitForSeconds(0.5f);
        SpriteClickAnimation.instance.BlackScreen.SetActive(false);
        yield return null;
    }
    public IEnumerator PlusTime(int _wait)
    {
        MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();
        yield return new WaitForSeconds(_wait);
        MainSceneDataCenter.instance.Player_save.TimeSaveData.CheckSpecialDay();
    }
    public void SetUPButtonFunction(Stats _stat)
    {
        if ((int)_stat < 7)//七個地標
        {
            SpriteClickAnimation.instance.ChatButton.SetActive(true);
            SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        StatsGrowth = MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, _stat);                    
                        //強化數值演出 
                        if ((int)_stat == 0|| (int)_stat == 1)
                        {
                            StartCoroutine("PlayerLevelAnimation", Stats.Kin_hua);
                        }
                        else if ((int)_stat == 2 || (int)_stat == 6)
                        {
                            StartCoroutine("PlayerLevelAnimation", Stats.Fong_Shin);
                        }
                        else if ((int)_stat == 4 || (int)_stat == 5)
                        {
                            StartCoroutine("PlayerLevelAnimation", Stats.Ron_Xiu);
                        }
                        else if ((int)_stat == 3)
                        {
                            StartCoroutine("PlayerLevelAnimation", Stats.Yao_Wan);
                        }
                        CheckIfGetNewSkill_BasicSkill(MainSceneDataCenter.instance.Player_save.m_Player.Level);                                               
                        StartCoroutine("ExitButtonClickedAnimation", 1.5f);
                        StartCoroutine("PlusTime", 2f);
                        SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    }                   
                }
                );
            SpriteClickAnimation.instance.ChatButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        int RandomDialogue; RandomDialogue = Random.Range(1, 4);
                        MainSceneDataCenter.instance.Player_save.m_Player.Gain_Love_Level(2);
                        
                        if (MainSceneDataCenter.instance.Player_save.TimeSaveData.time == 8|| MainSceneDataCenter.instance.Player_save.TimeSaveData.time == 22)
                        {
                            MainSceneDataCenter.instance.dialogue_Data_Object.IfSpecialToChat = true;
                            switch (_stat)
                            {
                                case Stats.POW:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad =  "Sen_Nong_Chat_"+RandomDialogue.ToString();
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.SPI:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Wood_Carving_Chat_" + RandomDialogue.ToString();
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.DEX:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Bench_Chat_" + RandomDialogue.ToString();
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.INT:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Market_Chat_" + RandomDialogue.ToString();
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.HP:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Bar_Chat_" + RandomDialogue.ToString();
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.DEF:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Unagi_Chat_" + RandomDialogue.ToString();
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.ATK:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Yong_Chuan_Chat_" + RandomDialogue.ToString();
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                            }                            
                        }
                        StartCoroutine("PlusTime", 2f);
                        switch (_stat)
                        { 
                            case Stats.POW:
                                Into_Dialogue("Sen_Nong_Chat_" + RandomDialogue.ToString());
                                return;
                            case Stats.SPI:
                                Into_Dialogue("Wood_Carving_Chat_" + RandomDialogue.ToString());
                                return;
                            case Stats.DEX:
                                Into_Dialogue("Bench_Chat_" + RandomDialogue.ToString());
                                return;
                            case Stats.INT:
                                Into_Dialogue("Market_Chat_" + RandomDialogue.ToString());
                                return;
                            case Stats.HP:
                                Into_Dialogue("Bar_Chat_" + RandomDialogue.ToString());
                                return;
                            case Stats.DEF:
                                Into_Dialogue("Unagi_Chat_" + RandomDialogue.ToString());
                                return;
                            case Stats.ATK:
                                Into_Dialogue("Yong_Chuan_Chat_" + RandomDialogue.ToString());
                                return;
                        }                        
                        //未完待續
                    }

                }
                );
            SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        if (MainSceneDataCenter.instance.Player_save.FlagCount > 0 && MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] == false)
                        {                            
                            StartCoroutine("MinusFlag");
                            MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] = true;
                            //演出
                            ADDFlag();
                            StartCoroutine("ExitButtonClickedAnimation",1);
                            StartCoroutine("PlusTime", 2f);
                            SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.RemoveAllListeners();
                        }
                        else
                        {
                            SpriteClickAnimation.instance.FlagButton.transform.DOShakePosition(0.5f, 15f);
                        }
                   }                    
                }
                );
        }
        else if ((int)_stat == 7)
        {
            SpriteClickAnimation.instance.ChatButton.SetActive(false);
            SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        if (MainSceneDataCenter.instance.Player_save.Can_Get_Flag == true)
                        {
                            MainSceneDataCenter.instance.Player_save.FlagCount = 3;
                            MainSceneDataCenter.instance.Player_save.Can_Get_Flag = false;
                            if (MainSceneDataCenter.instance.Player_save.TimeSaveData.time == 8 || MainSceneDataCenter.instance.Player_save.TimeSaveData.time == 22)
                            {
                                MainSceneDataCenter.instance.dialogue_Data_Object.IfSpecialToChat = true;
                                switch (MainTimeSystem.instance.Time.day)
                                {
                                    case 1:
                                        MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Loli_Home_day_1";
                                        StartCoroutine("ExitButtonClickedAnimation", 1f);
                                        StartCoroutine("PlusTime", 1f);
                                        return;
                                    case 2:
                                        MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Loli_Home_day_2";
                                        StartCoroutine("ExitButtonClickedAnimation", 1f);
                                        StartCoroutine("PlusTime", 1f);
                                        return;
                                    case 3:
                                        MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Loli_Home_day_3";
                                        StartCoroutine("ExitButtonClickedAnimation", 1f);
                                        StartCoroutine("PlusTime", 1f);
                                        return;
                                    case 4:
                                        MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Loli_Home_day_4";
                                        StartCoroutine("ExitButtonClickedAnimation", 1f);
                                        StartCoroutine("PlusTime", 1f);
                                        return;
                                    case 5:
                                        MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Loli_Home_day_5";
                                        StartCoroutine("ExitButtonClickedAnimation", 1f);
                                        StartCoroutine("PlusTime", 1f);
                                        return;
                                    case 6:
                                        MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Loli_Home_day_6";
                                        StartCoroutine("ExitButtonClickedAnimation", 1f);
                                        StartCoroutine("PlusTime", 1f);
                                        return;
                                    case 7:
                                        MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Loli_Home_day_7";
                                        StartCoroutine("ExitButtonClickedAnimation", 1f);
                                        StartCoroutine("PlusTime", 1f);
                                        return;
                                }
                            }
                            StartCoroutine("PlusTime", 2f);
                            switch (MainTimeSystem.instance.Time.day)//進入西蘿殿每日劇情
                            {
                                case 1:
                                    Into_Dialogue("Loli_Home_day_1");
                                    return;
                                case 2:
                                    Into_Dialogue("Loli_Home_day_2");
                                    return;
                                case 3:
                                    Into_Dialogue("Loli_Home_day_3");
                                    return;
                                case 4:
                                    Into_Dialogue("Loli_Home_day_4");
                                    return;
                                case 5:
                                    Into_Dialogue("Loli_Home_day_5");
                                    return;
                                case 6:
                                    Into_Dialogue("Loli_Home_day_6");
                                    return;
                                case 7:
                                    Into_Dialogue("Loli_Home_day_7");
                                    return;
                            }
                        }
                        if (MainSceneDataCenter.instance.Player_save.Can_Get_Flag == false)
                        {
                            SpriteClickAnimation.instance.BuffButton.transform.DOShakePosition(0.5f, 15f);
                            Debug.Log(2);
                        }
                    } 
                }
                );           
            SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        if (MainSceneDataCenter.instance.Player_save.FlagCount > 0&&MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat]==false)
                        {
                            StartCoroutine("MinusFlag");
                            MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] = true;
                            //演出
                            ADDFlag();
                            StartCoroutine("ExitButtonClickedAnimation", 1);
                            StartCoroutine("PlusTime", 2f);
                            SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.RemoveAllListeners();
                        }
                        else
                        {
                            SpriteClickAnimation.instance.FlagButton.transform.DOShakePosition(0.5f, 15f);
                            Debug.Log(2);
                        }
                    }   
                }
                );
        }
        else if ((int)_stat > 7 )
        {
            SpriteClickAnimation.instance.ChatButton.SetActive(true);
            SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        //開啟技能獲取介面
                    }
                }
                );
            SpriteClickAnimation.instance.ChatButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        int RandomDialogue; RandomDialogue = Random.Range(1, 4);
                        MainSceneDataCenter.instance.Player_save.m_Player.Gain_Love_Level(2);
                        if (MainSceneDataCenter.instance.Player_save.TimeSaveData.time == 8 || MainSceneDataCenter.instance.Player_save.TimeSaveData.time == 22)
                        {
                            MainSceneDataCenter.instance.dialogue_Data_Object.IfSpecialToChat = true;
                            switch (_stat)
                            {
                                case Stats.Yao_Wan:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Yao_Wan_Chat";
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.Fong_Shin:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Fong_Shin_Chat";
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.Kin_hua:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Kin_hua_Chat";
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;
                                case Stats.Ron_Xiu:
                                    MainSceneDataCenter.instance.dialogue_Data_Object.TempSaveNodePad = "Ron_Xiu_Chat";
                                    StartCoroutine("ExitButtonClickedAnimation", 1f);
                                    StartCoroutine("PlusTime", 1f);
                                    return;                                
                            }
                        }
                        StartCoroutine("PlusTime", 2f);

                        switch (_stat)
                        {
                            case Stats.Yao_Wan:
                                Into_Dialogue("Yao_Wan_Chat" + RandomDialogue.ToString());
                                return;
                            case Stats.Fong_Shin:
                                Into_Dialogue("Fong_Shin_Chat" + RandomDialogue.ToString());
                                return;
                            case Stats.Kin_hua:
                                Into_Dialogue("Kin_hua_Chat" + RandomDialogue.ToString());
                                return;
                            case Stats.Ron_Xiu:
                                Into_Dialogue("Ron_Xiu_Chat" + RandomDialogue.ToString());
                                return;
                        }
                        //未完待續
                    }

                }
                );
            SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        if (MainSceneDataCenter.instance.Player_save.FlagCount > 0 && MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] == false)
                        {
                            StartCoroutine("MinusFlag");
                            MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] = true;
                            //演出
                            ADDFlag();
                            StartCoroutine("ExitButtonClickedAnimation",1);
                            StartCoroutine("PlusTime", 2f);
                            SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.RemoveAllListeners();
                        }
                        else 
                        {
                            SpriteClickAnimation.instance.FlagButton.transform.DOShakePosition(0.5f, 15f);
                        }
                    }     
                }
                );
        }
    }
    //切劇情模式的function
    public void Into_Dialogue(string _plot_name)
    {
        ClearFunction();
        MainSceneDataCenter.instance.IntoDialogueScene(_plot_name);
    }
    public void ClearFunction()
    {
        SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.RemoveAllListeners();
        SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.RemoveAllListeners();
        SpriteClickAnimation.instance.ChatButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public void CheckIfGetNewSkill_BasicSkill(int _lv)
    {
        foreach (var item in MainSceneDataCenter.instance.m_SkillDatabaseOBJ.SkillData)
        {
            if (item.m_Skilltype == SkillType.BasicSkill)
            {
                if (item.LevelUpCondition[0] == _lv&&MainSceneDataCenter.instance.Player_save.SkillUnlockCheck[item.ID-1] == false)
                {
                    MainSceneDataCenter.instance.Player_save.SkillUnlockCheck[item.ID - 1] = true;
                    SpriteClickAnimation.instance.SkillPic.sprite = item.SkillPic;
                    SpriteClickAnimation.instance.SkillName.text = "「"+item.SkillName+"」";
                    StartCoroutine("PlayerGetSkillAnimation");
                }
            }
        }
    }
    public IEnumerator PlayerGetSkillAnimation()
    {
        DOTween.To(() => { return SpriteClickAnimation.instance.SkillPanel.GetComponent<RectTransform>().anchoredPosition; }, v => { SpriteClickAnimation.instance.SkillPanel.GetComponent<RectTransform>().anchoredPosition = v; }, SpriteClickAnimation.instance.skillpanel_end, 0.75f);
        yield return new WaitForSeconds(1.5f);
        DOTween.To(() => { return SpriteClickAnimation.instance.SkillPanel.GetComponent<RectTransform>().anchoredPosition; }, v => { SpriteClickAnimation.instance.SkillPanel.GetComponent<RectTransform>().anchoredPosition = v; }, SpriteClickAnimation.instance.skillpanel_original-new Vector2(4000,0), 1f);
        yield return new WaitForSeconds(1f);
        SpriteClickAnimation.instance.SkillPanel.SetActive(false);
        SpriteClickAnimation.instance.SkillPanel.GetComponent<RectTransform>().anchoredPosition = SpriteClickAnimation.instance.skillpanel_original;
        yield return new WaitForSeconds(0.1f);
        SpriteClickAnimation.instance.SkillPanel.SetActive(true);
    }
    #region 旗子動畫
    public IEnumerator AddFlag()
    {
        MainSceneDataCenter.instance.Flag1.GetComponent<Image>().DOFade(1, 1f);
        MainSceneDataCenter.instance.Flag2.GetComponent<Image>().DOFade(1, 1f);
        MainSceneDataCenter.instance.Flag3.GetComponent<Image>().DOFade(1, 1f);
        yield return null;
    }
    public IEnumerator MinusFlag()
    {
        if (MainSceneDataCenter.instance.Player_save.FlagCount == 3)
        {
            MainSceneDataCenter.instance.Flag3.GetComponent<Image>().DOFade(0, 1f);
        }
        else if (MainSceneDataCenter.instance.Player_save.FlagCount == 2)
        {
            MainSceneDataCenter.instance.Flag2.GetComponent<Image>().DOFade(0, 1f);
        }
        else if (MainSceneDataCenter.instance.Player_save.FlagCount == 1)
        {
            MainSceneDataCenter.instance.Flag1.GetComponent<Image>().DOFade(0, 1f);
        }
        MainSceneDataCenter.instance.Player_save.FlagCount--;
        yield return null;
    }
    
    #endregion
    public IEnumerator PlayerLevelAnimation(Stats _stats)
    {
        string temple = "";
        if (_stats == Stats.Kin_hua)
        {
            temple = "金華";
        }
        else if (_stats == Stats.Fong_Shin)
        {
            temple = "風神";
        }
        else if (_stats == Stats.Yao_Wan)
        {
            temple = "藥王";
        }
        else if (_stats == Stats.Ron_Xiu)
        {
            temple = "榕樹";
        }
        SpriteClickAnimation.instance.Player_stats.text = "血量\n物攻\n物防\n魔攻\n魔防\n迴避\n智慧\n幸運\n" + temple;
        SpriteClickAnimation.instance.PlayerLVint.text =(MainSceneDataCenter.instance.Player_save.m_Player.Level).ToString();
        SpriteClickAnimation.instance.PlayerLVPast.text = (MainSceneDataCenter.instance.Player_save.m_Player.Level - 1).ToString();
        SpriteClickAnimation.instance.StatsGrowthInt.text = StatsGrowth;
        SpriteClickAnimation.instance.NowPlayer.text = GetstatString(_stats);
        DOTween.To(() => { return SpriteClickAnimation.instance.LevelUpPanel.GetComponent<RectTransform>().anchoredPosition; }, v => { SpriteClickAnimation.instance.LevelUpPanel.GetComponent<RectTransform>().anchoredPosition = v; }, SpriteClickAnimation.instance.stats_end, 0.75f);
        //yield return new WaitUntil(()=>SpriteClickAnimation.instance.GoBack == true);
        yield return new WaitForSeconds(0.75f);
        GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>("main scene/UI/LevelUpGreenArrow"), transform.position, Quaternion.identity, SpriteClickAnimation.instance.LevelUPArrow.transform);
        yield return new WaitForSeconds(0.75f);
        //SpriteClickAnimation.instance.GoBack = false;
        DOTween.To(() => { return SpriteClickAnimation.instance.LevelUpPanel.GetComponent<RectTransform>().anchoredPosition; }, v => { SpriteClickAnimation.instance.LevelUpPanel.GetComponent<RectTransform>().anchoredPosition = v; }, SpriteClickAnimation.instance.stats_original-new Vector2(4000, 0), 1f);
        yield return new WaitForSeconds(1f);
        SpriteClickAnimation.instance.LevelUpPanel.SetActive(false);
        SpriteClickAnimation.instance.LevelUpPanel.GetComponent<RectTransform>().anchoredPosition = SpriteClickAnimation.instance.stats_original;
        yield return new WaitForSeconds(0.1f);
        SpriteClickAnimation.instance.LevelUpPanel.SetActive(true);
    }
    public string GetstatString(Stats _stats)
    {
        var player = MainSceneDataCenter.instance.Player_save.m_Player;
        string a = player.HP.m_currentstat.ToString() + "\n" + player.ATK.m_currentstat.ToString() + "\n" + player.DEF.m_currentstat.ToString() + "\n" + player.POW.m_currentstat.ToString() + "\n" + player.SPI.m_currentstat.ToString() + "\n" + player.DEX.m_currentstat.ToString() + "\n" + player.INT.m_currentstat.ToString() + "\n" + player.LUK.m_currentstat.ToString() + "\n";
        switch(_stats)
        {
            case Stats.INT:            
            case Stats.Yao_Wan:
                a = a + player.Yao_Wan.m_currentstat.ToString();
                return a;
            case Stats.Fong_Shin:
            case Stats.ATK:
            case Stats.DEX:
                a = a + player.Fong_Shin.m_currentstat.ToString();
                return a;
            case Stats.Kin_hua:
            case Stats.POW:
            case Stats.SPI:
                a = a + player.Kin_hua.m_currentstat.ToString();
                return a;
            case Stats.Ron_Xiu:
            case Stats.DEF:
            case Stats.HP:
                a = a + player.Ron_Xiu.m_currentstat.ToString();
                return a;
        }
        return null;
    }
    public void SetUpButttonImage(Stats _stat)
    {
        if ((int)_stat < 7)//七個地標
        {
            SpriteClickAnimation.instance.BuffButtonPic.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/Training_ICON");
            SpriteClickAnimation.instance.BuffButtonName.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/TrainingName");
        }
        else if ((int)_stat == 7)
        {
            SpriteClickAnimation.instance.ChatButton.SetActive(false);
            SpriteClickAnimation.instance.BuffButtonPic.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/RealChatting_Icon");
            SpriteClickAnimation.instance.BuffButtonName.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/RealChattingName");
        }
        else if ((int)_stat > 7)
        {
            SpriteClickAnimation.instance.BuffButtonPic.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/Temple_Icon");
            SpriteClickAnimation.instance.BuffButtonName.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/TempleName");
        }
    }
    public void ADDFlag()
    {
        GameObject flag = Instantiate<GameObject>(SpriteClickAnimation.instance.flagprefab, new Vector3(10.31f, 10.82f, 0), Quaternion.identity);
        Transform[] T = this.GetComponentsInChildren<Transform>();
        flag.GetComponent<Flaganimation>().Startflaganimation(T[2].position);
        MainTimeSystem.instance.FlagPrefabs.Add(flag);
    }
    public void FlagPicUpdate()
    {
        if (MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_Place] == true)
        {
            Invoke("ADDFlag", 0.5f);
        }
    }
}
