using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    public void Change_Layer()
    {
        this.GetComponentsInChildren<Transform>()[1].SetAsLastSibling();
    }
  
    public void FinishAnimation()
    {
        this.GetComponent<Animator>().SetTrigger("Finish");
    }
}
