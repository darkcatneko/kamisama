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
    private void OnMouseUp()
    {
        if (camScroll.instance.startPoint == camScroll.instance.endPoint&&MainSceneDataCenter.instance.status == Player_status.FreeMove)
        {         
            StartCoroutine("ButtonClickedAnimation");
            Level_Button_Function();
        }
    }
    public void Level_Button_Function()
    {
            SpriteClickAnimation.instance.BuffButton.GetComponent<Button>().onClick.AddListener(() => MainSceneDataCenter.instance.Player_save.m_Player.GainLevel(1, _Place));                
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
        yield return null;
    }
    public IEnumerator ExitButtonClickedAnimation()
    {        
        DOTween.To(() => SpriteClickAnimation.instance.Exit_Input, x => SpriteClickAnimation.instance.Exit_Input = x, SpriteClickAnimation.instance.Exit_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Buff_Input, x => SpriteClickAnimation.instance.Buff_Input = x, SpriteClickAnimation.instance.Buff_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Chat_Input, x => SpriteClickAnimation.instance.Chat_Input = x, SpriteClickAnimation.instance.Chat_Origin, 1.5f);
        DOTween.To(() => SpriteClickAnimation.instance.Shader_Input, x => SpriteClickAnimation.instance.Shader_Input = x, 0f, 1f);
        yield return new WaitForSeconds(1f);
        DOTween.To(() => fade, x => fade = x, 1f, 1f);
        SpriteClickAnimation.instance.BlackScreen.SetActive(false);
        MainSceneDataCenter.instance.status = Player_status.FreeMove;
        yield return null;
    }
}
