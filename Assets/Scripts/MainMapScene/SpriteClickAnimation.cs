using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpriteClickAnimation : MonoBehaviour
{
    public static SpriteClickAnimation instance;//獨體
    //物件
    public GameObject BlackScreen;
    public Image Place_image;
    public Material Place_shader;
    public float Shader_Input;
    //按鈕設置
    public GameObject ExitButton; public Vector2 Exit_Origin; public Vector2 Exit_end = new Vector3(-446,379); public Vector2 Exit_Input;
    public GameObject BuffButton; public Vector2 Buff_Origin; public Vector2 Buff_end = new Vector3(452,-180); public Vector2 Buff_Input;
    public GameObject ChatButton; public Vector2 Chat_Origin; public Vector2 Chat_end = new Vector3(452, 90); public Vector2 Chat_Input;
    public GameObject FlagButton; public Vector2 Flag_Origin; public Vector2 Flag_end = new Vector3(452, 90); public Vector2 Flag_Input;

    public TextMeshProUGUI FlagCount;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Exit_Origin = ExitButton.GetComponent<RectTransform>().anchoredPosition;
        Buff_Origin = BuffButton.GetComponent<RectTransform>().anchoredPosition;
        Chat_Origin = ChatButton.GetComponent<RectTransform>().anchoredPosition;
        Flag_Origin = FlagButton.GetComponent<RectTransform>().anchoredPosition;
        Exit_Input = Exit_Origin;
        Buff_Input = Buff_Origin;
        Chat_Input = Chat_Origin;
        Flag_Input = Flag_Origin;
    }
    // Update is called once per frame
    void Update()
    {
       Place_shader.SetFloat("_Fade", Shader_Input);
       ExitButton.GetComponent<RectTransform>().anchoredPosition = Exit_Input;
       BuffButton.GetComponent<RectTransform>().anchoredPosition = Buff_Input;
       ChatButton.GetComponent<RectTransform>().anchoredPosition = Chat_Input;
       FlagButton.GetComponent<RectTransform>().anchoredPosition = Flag_Input;
        FlagCount.text = MainSceneDataCenter.instance.Player_save.FlagCount + "/3";
    }
    
}
