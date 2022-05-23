using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroy : MonoBehaviour
{
    public void CallDestroy(float SELONG)
    {
        Destroy(this.gameObject, SELONG);
    }
    public void dontdestroy()
    {
        DontDestroyOnLoad(this);
    }
}
