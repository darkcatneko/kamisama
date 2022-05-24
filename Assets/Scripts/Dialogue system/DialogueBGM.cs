using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBGM : MonoBehaviour
{
   
    void Update()
    {
        this.GetComponent<AudioSource>().volume = GameObject.Find("Dialogue System").GetComponent<SEmaster>().VolumeMaster.All * GameObject.Find("Dialogue System").GetComponent<SEmaster>().VolumeMaster.BGM;
    }
}
