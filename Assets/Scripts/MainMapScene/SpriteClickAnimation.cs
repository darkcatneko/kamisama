using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpriteClickAnimation : MonoBehaviour
{
    public static SpriteClickAnimation instance;//獨體
    public bool Loading = false;//圖片參考用bool
    //物件
    public GameObject BlackScreen;
    public Material Place_shader;
    public float Shader_Input;
    //按鈕設置
    public GameObject ExitButton; public Vector2 Exit_Origin; public Vector2 Exit_end = new Vector3(-446,379); public Vector2 Exit_Input; 
    public GameObject BuffButton; public Vector2 Buff_Origin; public Vector2 Buff_end = new Vector3(452,-180); public Vector2 Buff_Input; public Vector2 Buff_Middle;
    public GameObject ChatButton; public Vector2 Chat_Origin; public Vector2 Chat_end = new Vector3(452, 90); public Vector2 Chat_Input; public Vector2 Chat_Middle;
    public GameObject FlagButton; public Vector2 Flag_Origin; public Vector2 Flag_end = new Vector3(452, 90); public Vector2 Flag_Input; public Vector2 Flag_Middle;
    public GameObject SkillButton; public Vector2 Skill_Origin; public Vector2 Skill_end = new Vector3(452, 90); public Vector2 Skill_Input;
    //圖片icon設置
    public GameObject Map_Pictures; public Vector2 Map_Pictures_origin; public Vector2 Map_Pictures_end;
    public Image Map_little_icon;public Image Map_icon_name;
    //button宣告
    public Image BuffButtonPic;
    public Image BuffButtonName;
    public Image panel;
    //玩家提升等級演出
    public GameObject LevelUpPanel;
    public GameObject LevelUPArrow;
    public TextMeshProUGUI PlayerLVint;
    public TextMeshProUGUI PlayerLVPast;
    public TextMeshProUGUI Player_stats;
    public TextMeshProUGUI StatsGrowthInt;
    public TextMeshProUGUI NowPlayer;
    public Vector2 stats_original;
    public Vector2 stats_end;
    //玩家獲得技能演出
    public GameObject SkillPanel;
    public Image SkillPic;
    public TextMeshProUGUI SkillName;
    public Vector2 skillpanel_original;
    public Vector2 skillpanel_end;
    public GameObject flagprefab ;
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
        Skill_Origin = SkillButton.GetComponent<RectTransform>().anchoredPosition;
        Map_Pictures_origin = Map_Pictures.GetComponent<RectTransform>().anchoredPosition;
        stats_original = LevelUpPanel.GetComponent<RectTransform>().anchoredPosition;
        skillpanel_original = SkillPanel.GetComponent<RectTransform>().anchoredPosition;
        Exit_Input = Exit_Origin;
        Buff_Input = Buff_Origin;
        Chat_Input = Chat_Origin;
        Flag_Input = Flag_Origin;
        Skill_Input = Skill_Origin;
    }
    // Update is called once per frame
    void Update()
    {
       Place_shader.SetFloat("_Fade", Shader_Input);
       ExitButton.GetComponent<RectTransform>().anchoredPosition = Exit_Input;
       BuffButton.GetComponent<RectTransform>().anchoredPosition = Buff_Input;
       ChatButton.GetComponent<RectTransform>().anchoredPosition = Chat_Input;
       FlagButton.GetComponent<RectTransform>().anchoredPosition = Flag_Input;
       SkillButton.GetComponent<RectTransform>().anchoredPosition = Skill_Input;
    }
}
