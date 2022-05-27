using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ChoiceAnimation : MonoBehaviour
{
    public void PointerEnter()
    {
        this.GetComponent<Image>().rectTransform.DOScale(new Vector3(1.3f, 1.3f, 0), 0.3f);
    }
    public void PointerExit()
    {
        this.GetComponent<Image>().rectTransform.DOScale(new Vector3(1f, 1f, 0), 0.3f);
    }
}
