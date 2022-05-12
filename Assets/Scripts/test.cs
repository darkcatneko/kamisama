using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour
{
    private void Start()
    {
        DOTween.Clear(true);
    }
    public void MathTest()
    {

        SceneManager.LoadScene(0);
    }
}

