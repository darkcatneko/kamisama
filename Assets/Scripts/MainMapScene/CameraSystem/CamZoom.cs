using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamZoom : MonoBehaviour
{
    public float sc = 2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setDistance();
        var camera = Camera.main;
        var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if (vcam!=null)
        {
            vcam.m_Lens.OrthographicSize = sc;
        }
    }
    void setDistance()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0&& MainSceneDataCenter.instance.status == Player_status.FreeMove)
        {
            sc += 0.1f;
            sc = Mathf.Clamp(sc, 4f, 6.5f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0&& MainSceneDataCenter.instance.status == Player_status.FreeMove)
        {
            sc -= 0.1f;
            sc = Mathf.Clamp(sc, 4f, 6.5f);
        }

    }
}
