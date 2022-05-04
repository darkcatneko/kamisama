using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap_Particle : MonoBehaviour
{
    public GameObject Tap_prefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           GameObject tap =  Instantiate(Tap_prefab,new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,-9) , Quaternion.identity);
        }
    }
}
