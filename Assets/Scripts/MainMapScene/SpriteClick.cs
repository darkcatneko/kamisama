using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpriteClick : MonoBehaviour
{
    public float fade = 1;
    public float UI_fade = 0;
    public Vector2 Exit_end;
    public Vector2 Buff_end;
    public Vector2 Chat_end;
    public Stats _Place;

    public SpriteRenderer Map_Place_name;
    
    private void Start()
    {
        Exit_end = SpriteClickAnimation.instance.Exit_Origin;
        Buff_end = SpriteClickAnimation.instance.Buff_Origin;
        Chat_end = SpriteClickAnimation.instance.Chat_Origin;
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
        if (MainSceneDataCenter.instance.status == Player_status.FreeMove)
        {
            Map_Place_name.DOFade(0, 0.3f);
            this.transform.DOScale(new Vector3(1f, 1f, 0), 0.3f);
        }
        
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
        SpriteClickAnimation.instance.Place_image.sprite = this.GetComponent<SpriteRenderer>().sprite;
        DOTween.To(() => SpriteClickAnimation.instance.Shader_Input, x => SpriteClickAnimation.instance.Shader_Input = x, 1f, 1f);
        DOTween.To(() => SpriteClickAnimation.instance.Exit_Input, x => SpriteClickAnimation.instance.Exit_Input = x, SpriteClickAnimation.instance.Exit_end, 1.5f);
        SpriteClickAnimation.instance.ExitButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine("ExitButtonClickedAnimation");
        });
        DOTween.To(() => SpriteClickAnimation.instance.Buff_Input, x => SpriteClickAnimation.instance.Buff_Input = x, SpriteClickAnimation.instance.Buff_end, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Chat_Input, x => SpriteClickAnimation.instance.Chat_Input = x, SpriteClickAnimation.instance.Chat_end, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Flag_Input, x => SpriteClickAnimation.instance.Flag_Input = x, SpriteClickAnimation.instance.Flag_end, 1.5f);
        yield return null;
    }
    public IEnumerator ExitButtonClickedAnimation()
    {        
        DOTween.To(() => SpriteClickAnimation.instance.Exit_Input, x => SpriteClickAnimation.instance.Exit_Input = x, SpriteClickAnimation.instance.Exit_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Buff_Input, x => SpriteClickAnimation.instance.Buff_Input = x, SpriteClickAnimation.instance.Buff_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Chat_Input, x => SpriteClickAnimation.instance.Chat_Input = x, SpriteClickAnimation.instance.Chat_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Flag_Input, x => SpriteClickAnimation.instance.Flag_Input = x, SpriteClickAnimation.instance.Flag_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Shader_Input, x => SpriteClickAnimation.instance.Shader_Input = x, 0f, 1f);
        yield return new WaitForSeconds(1f);
        DOTween.To(() => fade, x => fade = x, 1f, 1f);
        SpriteClickAnimation.instance.BlackScreen.SetActive(false);
        MainSceneDataCenter.instance.status = Player_status.FreeMove;
        yield return null;
    }

    public void SetUPButtonFunction(Stats _stat)
    {
        if ((int)_stat < 8)//七個地標
        {
            SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.AddListener(
                () =>
                { 
                    MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, _stat);//執行動作
                    StartCoroutine("ExitButtonClickedAnimation");
                    //進入戰鬥
                    MainTimeSystem.instance.Time.PlusTime();
                    SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.RemoveAllListeners();
                }
                );
            SpriteClickAnimation.instance.ChatButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    MainSceneDataCenter.instance.Player_save.m_Player.Gain_Love_Level(2);
                    MainTimeSystem.instance.Time.PlusTime();
                    switch (_stat)
                    {
                        case Stats.POW:
                            MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = "Sen_Nong_Chat";
                            return;
                        case Stats.SPI:
                            MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = "Wood_Carving_Chat";
                            return;
                        case Stats.DEX:
                            MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = "Bench_Chat";
                            return;
                        case Stats.INT:
                            MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = "Market_Chat";
                            return;
                        case Stats.HP:
                            MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = "Bar_Chat";
                            return;
                        case Stats.DEF:
                            MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = "Unagi_Chat";
                            return;
                        case Stats.ATK:
                            MainSceneDataCenter.instance.dialogue_Data_Object.The_NodePad_Be_read = "Yong_Chuan_Chat";
                            return;
                    }
                }
                );
            SpriteClickAnimation.instance.FlagButton.GetComponent<Button>().onClick.AddListener(
                () =>
                {

                }
                );
        }
    }
}
