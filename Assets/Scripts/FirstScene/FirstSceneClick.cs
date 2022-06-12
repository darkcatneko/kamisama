using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstSceneClick : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)|| Input.GetMouseButtonDown(0))
        {
            OnClick.Invoke();
        }
    }

}
