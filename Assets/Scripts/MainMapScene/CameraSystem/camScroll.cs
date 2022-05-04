using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScroll : MonoBehaviour
{
    public Vector3 Original;
    public Vector3 Difference;
    public Vector3 ResetCamera;

    public Vector3 startPoint;
    public Vector3 endPoint;

    private bool drag = false;
    public bool CanClick = true;
    public static camScroll instance;
    private void Awake()
    {
       instance = this;
    }
    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }
    private void Update()
    {
        endPoint = this.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (MainSceneDataCenter.instance.status == Player_status.FreeMove)
        {
            if (Input.GetMouseButton(0))
            {
                Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (drag == false)
                {
                    drag = true;
                    Original = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    startPoint = this.transform.position;
                }
            }
            else
            {
                drag = false;
            }
            if (drag)
            {
                Camera.main.transform.position = Original - Difference;
            }
        }
        
    }

}
