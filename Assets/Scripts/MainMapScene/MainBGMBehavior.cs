using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBGMBehavior : MonoBehaviour
{
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GameObject.FindGameObjectsWithTag("BGM").Length>1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2&&SceneManager.GetActiveScene().buildIndex != 3)
        {
            Destroy(this.gameObject);
        }
    }
}
