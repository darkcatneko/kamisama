using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingButtonInteraction : MonoBehaviour
{
    public GameObject Setting_Buttons;
    public Vector2 Origin_Pos;
    public Vector2 End_Position;
    public Player_status temp;
    float middle => Mathf.Abs(Origin_Pos.x - End_Position.x);
    private void Start()
    {
        Origin_Pos = Setting_Buttons.GetComponent<RectTransform>().anchoredPosition;
    }
    public void GearButtonClicked ()
    {
        
        if (MainSceneDataCenter.instance.status != Player_status.Setting)
        {
            temp = MainSceneDataCenter.instance.status;
            MainSceneDataCenter.instance.status = Player_status.Setting;
            DOTween.To(() => { return Setting_Buttons.GetComponent<RectTransform>().anchoredPosition; }, v => { Setting_Buttons.GetComponent<RectTransform>().anchoredPosition = v; }, End_Position, 1f);
        }
        else if(MainSceneDataCenter.instance.status == Player_status.Setting)
        {
            MainSceneDataCenter.instance.status = temp;
            temp = Player_status.FreeMove;
            DOTween.To(() => { return Setting_Buttons.GetComponent<RectTransform>().anchoredPosition; }, v => { Setting_Buttons.GetComponent<RectTransform>().anchoredPosition = v; }, Origin_Pos, 1f);
        }
    }
    
}
