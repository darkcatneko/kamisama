using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CamMovememt : MonoBehaviour
{
    private Camera m_camera;
    private Func<Vector3> GetCameraFollowPositionFunc;
    private Func<float> GetCameraZoomFunc;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc, Func<float> GetCameraZoomFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }

    private void Start()
    {
        m_camera = transform.GetComponent<Camera>();
    }
    public void SetCameraFollowPosition(Vector3 cameraFollowPosition)
    {
        SetGetCameraFollowPositionFunc(() => cameraFollowPosition);
    }
    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }
    public void SetCameraZoom(float cameraZoom)
    {
        SetGetCameraZoomFunc(() => cameraZoom);
    }
    public void SetGetCameraZoomFunc(Func<float> GetCameraZoomFunc)
    {
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }
    void Update()
    {
        //HandleMovement();
        //HandleZoom();


    }
    private void HandleMovement()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 1f;
        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);
            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }
            transform.position = newCameraPosition;
        }
    }
    private void HandleZoom()
    {
        float cameraZoom = GetCameraZoomFunc();

        float cameraZoomDifference = cameraZoom - m_camera.orthographicSize;
        float cameraZoomSpeed = 1f;
        m_camera.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;
        if (cameraZoomDifference>0)
        {
            if (m_camera.orthographicSize>cameraZoom)
            {
                m_camera.orthographicSize = cameraZoom;
            }
        }
        else
        {
            if (m_camera.orthographicSize<cameraZoom)
            {
                m_camera.orthographicSize = cameraZoom;
            }
        }
    }
}
