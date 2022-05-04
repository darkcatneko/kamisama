using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public CamMovememt cameramove;

    private float zoom;
    public Vector3 cameraFollowPosition;
    void Start()
    {
        cameramove.Setup(() => cameraFollowPosition,()=> 5f);

        
    }

    // Update is called once per frame
    void Update()
    {
        float moveAmount = 15f;
        float edgeSize = 40f;
        if (Input.mousePosition.x> Screen.width - edgeSize)
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
            cameraFollowPosition = ClampPosition(cameraFollowPosition);
        }
        if (Input.mousePosition.x <  edgeSize)
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
            cameraFollowPosition = ClampPosition(cameraFollowPosition);
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraFollowPosition.y += moveAmount * Time.deltaTime;
            cameraFollowPosition =ClampPosition(cameraFollowPosition);
        }
        if (Input.mousePosition.y < edgeSize)
        {
            cameraFollowPosition.y -= moveAmount * Time.deltaTime;
            cameraFollowPosition = ClampPosition(cameraFollowPosition);
        }
    }
    private void ZoomIn()
    {
        zoom -= 40f;
        if (zoom < 40f)
        {
            zoom = 40f;
        }
    }
    private void ZoomOut()
    {
        zoom += 40f;
        if (zoom>200f)
        {
            zoom = 200f;
        }
    }
    public Vector3 ClampPosition(Vector3 _pos)
    {
        Vector3 a;
        a = new Vector3(Mathf.Clamp(_pos.x, -20f, 17f), Mathf.Clamp(_pos.y, -5f, 5f),_pos.z);
        return a;
    }
}
