using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class SEmaster : MonoBehaviour
{
    public VolumeDataCenter VolumeMaster;
  
    private void Update()
    {

    }
    public void GencrossSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("CrossSceneSE"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().dontdestroy();
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);

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
    public void OnCrossSlectSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("CrossSceneslectSE"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().dontdestroy();
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);
    }
    public void OnSlectSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("slectSE"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);
    }
    public void OnClickSE()
    {
        GameObject Buttonse = Instantiate<GameObject>(Resources.Load<GameObject>("Onclick"), transform.position, Quaternion.identity);
        Buttonse.GetComponent<AudioSource>().volume = VolumeMaster.All * VolumeMaster.SoundEffect;
        Buttonse.GetComponent<SimpleDestroy>().CallDestroy(Buttonse.GetComponent<AudioSource>().clip.length);
    }
}


