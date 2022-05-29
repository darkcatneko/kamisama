using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour
{
    public VolumeDataCenter VolumeMaster;
    public GameObject bruh;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(bruh);
        }
    }
    public void GenSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("buttonSE"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);
    }
    public void FlipSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("cardSE"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);
    }
    public void OnSlectSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("slectSE"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);
    }
}
public class player
{
   
}


