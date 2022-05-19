using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour
{
    public VolumeDataCenter VolumeMaster;
    private void Update()
    {
        
    }
    public void GenSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("buttonSE"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);
    }
}
public class player
{
   
}


