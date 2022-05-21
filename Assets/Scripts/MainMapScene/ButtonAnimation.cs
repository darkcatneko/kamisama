using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour
{
    public Image ButtonBackGround;
    public Image ButtonPicture;
    public Image ButtonName;
    public void OnpointerEnter()
    {
        ButtonName.rectTransform.DOScale(new Vector3(1.3f,1.3f,0), 0.3f);
        ButtonBackGround.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/Hover_Pic");
    }
    public void OnpointerExit()
    {
        ButtonName.rectTransform.DOScale(new Vector3(1f, 1f, 0), 0.3f);
        ButtonBackGround.sprite = Resources.Load<Sprite>("main scene/UI/choice UI/Idle_Pic");
    }
    private void Update()
    {
        if (SpriteClickAnimation.instance.Loading == false)
        {
            ButtonPicture.color = new Color(1, 1, 1, 0);
            ButtonName.color = new Color(1, 1, 1, 0);
            ButtonBackGround.color = new Color(1, 1, 1, 0);

        }
        else
        {
            ButtonPicture.color = new Color(1, 1, 1, 1);
            ButtonName.color = new Color(1, 1, 1, 1);
            ButtonBackGround.color = new Color(1, 1, 1, 1);
        }
    }
}