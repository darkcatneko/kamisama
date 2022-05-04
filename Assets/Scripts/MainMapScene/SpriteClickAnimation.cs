using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteClickAnimation : MonoBehaviour
{
    public static SpriteClickAnimation instance;//¿WÅé
    //ª«¥ó
    public GameObject BlackScreen;
    public Image Place_image;
    public Material Place_shader;
    //«ö¶s³]¸m
    public GameObject ExitButton; public Vector2 Exit_Origin; public Vector2 Exit_end = new Vector3(-446,379);
    public GameObject BuffButton; public Vector2 Buff_Origin; public Vector2 Buff_end = new Vector3(452,-180);//¦r¦ê and function
    public GameObject ChatButton; public Vector2 Chat_Origin; public Vector2 Chat_end = new Vector3(452, 90);//¦r¦ê and function

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Exit_Origin = ExitButton.GetComponent<RectTransform>().anchoredPosition;
        Buff_Origin = BuffButton.GetComponent<RectTransform>().anchoredPosition;
        Chat_Origin = ChatButton.GetComponent<RectTransform>().anchoredPosition;
    }
    // Update is called once per frame
    void Update()
    {

    }
    
}
