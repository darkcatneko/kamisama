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

    public SpriteRenderer Map_Place_name;
    
    private void Start()
    {
        Exit_end = SpriteClickAnimation.instance.Exit_Origin;
        Buff_end = SpriteClickAnimation.instance.Buff_Origin;
        Chat_end = SpriteClickAnimation.instance.Chat_Origin;
        Skill_end = SpriteClickAnimation.instance.Skill_Origin;
        
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
        }
    }
    public IEnumerator ButtonClickedAnimation()
    {
        MainSceneDataCenter.instance.status = Player_status.ButtonClicked;        
        DOTween.To(() => fade, x => fade = x, 0f, 1f);    
        yield return new WaitForSeconds(1f);
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
            StartCoroutine("ExitButtonClickedAnimation");
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
    public IEnumerator ExitButtonClickedAnimation()
    {
        ClearFunction();        
        SpriteClickAnimation.instance.panel.DOFade(0f, 1f);
        DOTween.To(() => { return SpriteClickAnimation.instance.Map_Pictures.GetComponent<RectTransform>().anchoredPosition; }, v => { SpriteClickAnimation.instance.Map_Pictures.GetComponent<RectTransform>().anchoredPosition = v; }, SpriteClickAnimation.instance.Map_Pictures_origin, 1f);
        DOTween.To(() => SpriteClickAnimation.instance.Skill_Input, x => SpriteClickAnimation.instance.Skill_Input = x, SpriteClickAnimation.instance.Skill_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Exit_Input, x => SpriteClickAnimation.instance.Exit_Input = x, SpriteClickAnimation.instance.Exit_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Buff_Input, x => SpriteClickAnimation.instance.Buff_Input = x, SpriteClickAnimation.instance.Buff_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Chat_Input, x => SpriteClickAnimation.instance.Chat_Input = x, SpriteClickAnimation.instance.Chat_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Flag_Input, x => SpriteClickAnimation.instance.Flag_Input = x, SpriteClickAnimation.instance.Flag_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Shader_Input, x => SpriteClickAnimation.instance.Shader_Input = x, 0f, 1f);
        yield return new WaitForSeconds(1f);
        DOTween.To(() => fade, x => fade = x, 1f, 1f);
        MainSceneDataCenter.instance.status = Player_status.FreeMove;
        SpriteClickAnimation.instance.Loading = false;
        SpriteClickAnimation.instance.BlackScreen.SetActive(false);
        yield return null;
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
                        MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, _stat);//執行動作
                        StartCoroutine("ExitButtonClickedAnimation");
                        //進入戰鬥
                        MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();
                        SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    }                   
                }
                );
            SpriteClickAnimation.instance.ChatButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    if (MainSceneDataCenter.instance.status == Player_status.ButtonClicked)
                    {
                        MainSceneDataCenter.instance.Player_save.m_Player.Gain_Love_Level(2);
                        MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();
                        switch (_stat)
                        {
                            case Stats.POW:
                                Into_Dialogue("Sen_Nong_Chat");
                                return;
                            case Stats.SPI:
                                Into_Dialogue("Wood_Carving_Chat");
                                return;
                            case Stats.DEX:
                                Into_Dialogue("Bench_Chat");
                                return;
                            case Stats.INT:
                                Into_Dialogue("Market_Chat");
                                return;
                            case Stats.HP:
                                Into_Dialogue("Bar_Chat");
                                return;
                            case Stats.DEF:
                                Into_Dialogue("Unagi_Chat");
                                return;
                            case Stats.ATK:
                                Into_Dialogue("Yong_Chuan_Chat");
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
                            MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();
                            StartCoroutine("MinusFlag");
                            MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] = true;
                            //演出
                            StartCoroutine("ExitButtonClickedAnimation");
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
                            MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();
                            MainSceneDataCenter.instance.Player_save.Can_Get_Flag = false;
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
                            MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();
                            StartCoroutine("MinusFlag");
                            MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] = true;
                            //演出
                            StartCoroutine("ExitButtonClickedAnimation");
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
                        MainSceneDataCenter.instance.Player_save.m_Player.Gain_Love_Level(2);
                        MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();

                        switch (_stat)
                        {
                            case Stats.Yao_Wan:
                                Into_Dialogue("Yao_Wan_Chat");
                                return;
                            case Stats.Fong_Shin:
                                Into_Dialogue("Fong_Shin_Chat");
                                return;
                            case Stats.Kin_hua:
                                Into_Dialogue("Kin_hua_Chat");
                                return;
                            case Stats.Ron_Xiu:
                                Into_Dialogue("Ron_Xiu_Chat");
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
                            MainSceneDataCenter.instance.Player_save.TimeSaveData.PlusTime();
                            StartCoroutine("MinusFlag");
                            MainSceneDataCenter.instance.Player_save.MapFlagCheck[(int)_stat] = true;
                            //演出
                            StartCoroutine("ExitButtonClickedAnimation");
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
        MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = _plot_name;
        MainSceneDataCenter.instance.dialogue_Data_Object.WhichLineItRead = 0;
        MainSceneDataCenter.instance.Player_save.Save();
        SceneManager.LoadScene(1);
    }
    public void ClearFunction()
    {
        SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.RemoveAllListeners();
        SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.RemoveAllListeners();
        SpriteClickAnimation.instance.ChatButton.GetComponent<Button>().onClick.RemoveAllListeners();
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
}
