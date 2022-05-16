using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class SkillPanelController : MonoBehaviour
{
    public TextMeshProUGUI PlayerStatsInt;
    public TextMeshProUGUI PlayerLevel;

    public List<GameObject> SkillBackPackButtons;
    public List<GameObject> SkillBoardButtons;
    private int[] intarray = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public Image AimingPic;
    public int FocusNum = 0;
    public int FocusSkill = 0;
    public int OnPage = 1;
    public Image SkillBoard;
    public TextMeshProUGUI HowtoGetSkill;
    void Start()
    {
        for (int i = 0; i < SkillBoardButtons.Count; i++)
        {
            SetSkillBoard(i+1,i);
        }
        SetSkillBackButton();
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerInformation(PlayerStatsInt);
    }
    public void SetPlayerInformation(TextMeshProUGUI _text)
    {
        PlayerLevel.text = MainSceneDataCenter.instance.Player_save.m_Player.Level.ToString();
        _text.text = 
            MainSceneDataCenter.instance.Player_save.m_Player.HP.m_currentstat.ToString() + "\n"
           +MainSceneDataCenter.instance.Player_save.m_Player.ATK.m_currentstat.ToString()+"\n"
           + MainSceneDataCenter.instance.Player_save.m_Player.DEF.m_currentstat.ToString() + "\n"
           + MainSceneDataCenter.instance.Player_save.m_Player.POW.m_currentstat.ToString() + "\n"
           + MainSceneDataCenter.instance.Player_save.m_Player.SPI.m_currentstat.ToString() + "\n"
           + MainSceneDataCenter.instance.Player_save.m_Player.DEX.m_currentstat.ToString() + "\n"
           + MainSceneDataCenter.instance.Player_save.m_Player.INT.m_currentstat.ToString() + "\n"
           + MainSceneDataCenter.instance.Player_save.m_Player.LUK.m_currentstat.ToString() + "\n"
           + MainSceneDataCenter.instance.Player_save.m_Player.Love.m_currentstat.ToString();
    }
    public void SetSkillBackButton()
    {        
        for (int i = 0; i < SkillBackPackButtons.Count; i++)
        {
            Debug.Log(i);
            if (MainSceneDataCenter.instance.Player_save.SkillBackPack[i] == 0)
            {
                SkillBackPackButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillPicture/None");//無技能的圖片
            }
            else
            {
                SkillBackPackButtons[i].GetComponent<Image>().sprite = MainSceneDataCenter.instance.m_SkillDatabaseOBJ.GetSkillInformation(MainSceneDataCenter.instance.Player_save.SkillBackPack[i]).SkillPic;                
            }            
        }
    }

    public void SetSkillButtonFunction(int _id)
    {
        FocusNum = _id;
        Debug.Log(_id);
        DOTween.To(() => { return AimingPic.GetComponent<Transform>().localPosition; }, v => { AimingPic.GetComponent<Transform>().localPosition = v; }, SkillBackPackButtons[_id].GetComponent<Transform>().localPosition, 0.3f);
        if (MainSceneDataCenter.instance.Player_save.SkillBackPack[_id]!=0)
        {
            SkillBoard.sprite = MainSceneDataCenter.instance.m_SkillDatabaseOBJ.GetSkillInformation(MainSceneDataCenter.instance.Player_save.SkillBackPack[FocusNum]).SkillCardPic;
        }
    }

    public void SetSkillBackButtonUpdate(int focus)
    {
        if (MainSceneDataCenter.instance.Player_save.SkillBackPack[focus] == 0)
        {
            SkillBackPackButtons[focus].GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillPicture/None");//無技能的圖片
        }
        else
        {
            SkillBackPackButtons[focus].GetComponent<Image>().sprite = MainSceneDataCenter.instance.m_SkillDatabaseOBJ.GetSkillInformation(MainSceneDataCenter.instance.Player_save.SkillBackPack[focus]).SkillPic;            
        }
    }

    public Sprite FindTrigrams(EightTrigrams eightTrigrams)
    {
        return Resources.Load<Sprite>("EightTrigrams/"+eightTrigrams.ToString());
    }
    public void SetSkillBoard(int _id,int _buttonNum)
    {
        SkillBoardButtons[_buttonNum].GetComponentsInChildren<Image>()[1].sprite =FindTrigrams(MainSceneDataCenter.instance.m_SkillDatabaseOBJ.GetSkillInformation(_id).m_eightTrigrams);
        SkillBoardButtons[_buttonNum].GetComponentInChildren<TextMeshProUGUI>().text = MainSceneDataCenter.instance.m_SkillDatabaseOBJ.GetSkillInformation(_id).SkillName;
        SkillBoardButtons[_buttonNum].GetComponent<Button>().onClick.AddListener(
            ()=> 
            {
                FocusSkill = _id;
                if (MainSceneDataCenter.instance.Player_save.SkillUnlockCheck[_id-1]!=true)
                {
                    SkillBoard.sprite = Resources.Load<Sprite>("SkillPicture/NoneCard");//無技能的圖片
                    HowtoGetSkill.text = MainSceneDataCenter.instance.m_SkillDatabaseOBJ.GetSkillInformation(_id).SkillLearnWay;
                }
                else
                {
                    SkillBoard.sprite = MainSceneDataCenter.instance.m_SkillDatabaseOBJ.GetSkillInformation(_id).SkillCardPic;
                    HowtoGetSkill.text = "";
                }
            });
    }
    public void EquipButtonPress()
    {
        Equip(FocusNum, FocusSkill);
    }
    public void Equip(int FocusBackPack, int FocusSkill)
    {
        if (MainSceneDataCenter.instance.Player_save.SkillUnlockCheck[FocusSkill - 1] == true)
        {
            for (int i = 0; i < MainSceneDataCenter.instance.Player_save.SkillBackPack.Length; i++)
            {
                if (MainSceneDataCenter.instance.Player_save.SkillBackPack[i] == FocusSkill)
                {
                    MainSceneDataCenter.instance.Player_save.SkillBackPack[i] = 0;
                    MainSceneDataCenter.instance.Player_save.SkillBackPack[FocusBackPack] = FocusSkill;
                    SetSkillBackButtonUpdate(FocusNum);
                    SetSkillBackButtonUpdate(i);
                    return;
                }
            }
            MainSceneDataCenter.instance.Player_save.SkillBackPack[FocusBackPack] = FocusSkill;
            SetSkillBackButtonUpdate(FocusNum);
        }
        Debug.Log("白癡7414");
    }
}
