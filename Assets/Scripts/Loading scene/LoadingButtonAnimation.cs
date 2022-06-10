using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingButtonAnimation : MonoBehaviour
{
    [SerializeField] Image YellowBar;
    private float Width = 0;
    private bool InPic = false;
    private void OnMouseOver()
    {
        InPic = true;
    }
    private void Update()
    {
        YellowBar.rectTransform.sizeDelta = new Vector2(Width, 100f);
        if (InPic)
        {
            YellowBar.color = new Color(1, 1, 1, 1);
        }        
    }
    public void OnPointerEnter()
    {
        InPic = true;
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>("loading scene/buttom key");
        DOTween.To(() => Width, x => Width = x, 500f, 0.5f);
    }

    public void OnPointerExit()
    {
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>("loading scene/Alpha");
        DOTween.To(() => Width, x => Width = x, 0f, 0.5f).OnStepComplete(()=> { InPic = false; });
    }
}
