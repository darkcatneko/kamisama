using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    
    public AudioSource Music;
    public VolumeDataCenter m_VolumeDataCenter;
    public Slider AllBar;
    public Slider SE_Bar;
    public Slider BGM_Bar;

    public float StartVolume;
   
    private void Start()
    {
        if (Music == null)
        {
            Music = GameObject.Find("MainBGM").GetComponent<AudioSource>();
        }
       StartVolume = Music.volume;
       AllBar.value  = m_VolumeDataCenter.All;
       BGM_Bar.value = m_VolumeDataCenter.BGM;
       SE_Bar.value = m_VolumeDataCenter.SoundEffect;
    }
    private void Update()
    {
        m_VolumeDataCenter.All = AllBar.value;
        m_VolumeDataCenter.BGM = BGM_Bar.value;
        m_VolumeDataCenter.SoundEffect = SE_Bar.value;
    }
    private void LateUpdate()
    {
        Music.volume = m_VolumeDataCenter.All * m_VolumeDataCenter.BGM;
    }
}
