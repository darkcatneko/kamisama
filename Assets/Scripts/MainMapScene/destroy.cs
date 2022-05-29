using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public int deathtime = 1;
    void Start()
    {
        Destroy(this.gameObject, deathtime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
