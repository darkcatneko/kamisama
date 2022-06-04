using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillButtonDoTween : MonoBehaviour
{
    public void OnPointerEnter()
    {
        if (MainBattleSystem.instance.m_battleStatus == BattleStatus.PlayerTurn)
        {
            this.GetComponent<RectTransform>().DOScale(new Vector3(1.1f, 1.1f, 0), 0.3f);
        }        
    }
    public void OnPointerExit()
    {
        this.GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 0), 0.3f);
    }
    public void Onclick()
    {
        this.GetComponent<RectTransform>().DOScale(new Vector3(0.95f, 0.95f, 0), 0.1f);
        Invoke("OnPointerExit", 0.1f);
    }    
}
