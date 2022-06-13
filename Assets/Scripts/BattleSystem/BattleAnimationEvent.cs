using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class BattleAnimationEvent : MonoBehaviour
{
    public Material BasicMat;
    public UnityEvent TheBossAttack = new UnityEvent();
    public UnityEvent BossEndAnimation = new UnityEvent();
    private void Start()
    {
        BasicMat = MainBattleSystem.instance.BossSprites[0].material;       
    }
    public void GenBattleEffect()
    {
        Instantiate(MainBattleSystem.instance.battleAnimationContents.BattleEffect, Vector3.zero, Quaternion.identity);
    }
    public void GenBossBattleEffect(int Skill_Number)
    {
        Instantiate(MainBattleSystem.instance.sceneControllerOBJ.NextBoss.m_base.BossSkillPrefab[Skill_Number], Vector3.zero, Quaternion.identity);
    }
    public void GenFieldEffect()
    {
        MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams] = Instantiate(MainBattleSystem.instance.battleAnimationContents.FieldPrefab, MainBattleSystem.instance.EightTrimSpawnPoint[(int)MainBattleSystem.instance.NowFocusTrigrams].transform.position, Quaternion.identity);
        MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().Place = MainBattleSystem.instance.NowFocusTrigrams;
        MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().Amount = MainBattleSystem.instance.BuffAmount;
        if (MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().SelfDestroyCountDown!=-1)
        {
            MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().StartTurn = MainBattleSystem.instance.NowTurn;
            MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().EndTurn = MainBattleSystem.instance.NowTurn + MainBattleSystem.instance.FieldSkills[(int)MainBattleSystem.instance.NowFocusTrigrams].GetComponent<OnFieldDestroy>().SelfDestroyCountDown;
        }       
    }
    public void GenDamageNumber()
    {
        Vector3 Place = new Vector3(Random.Range(-20, 0) * 0.1f, 0f, Random.Range(-20, 0) * 0.1f);
        int Damage = MainBattleSystem.instance.battleAnimationContents.DamageDelt[MainBattleSystem.instance.battleAnimationContents.NowDisplayDamage].Number;//根據怪物的魔防物防來改
        MainBattleSystem.instance.battleAnimationContents.NowDisplayDamage++;
        GameObject DamageBlock = Instantiate(Resources.Load<GameObject>("DamageNumber"), Place, Quaternion.identity);
        if (Damage>=10)
        {
            if (Damage>=100)
            {
                if (Damage >= 1000)
                {
                    if (Damage >= 10000)
                    {
                        if (Damage >= 99999)
                        {

                        }
                    }
                    else
                    {
                        DamageBlock.transform.Find("mil").gameObject.SetActive(false);
                    }
                }
                else
                {
                    DamageBlock.transform.Find("mil").gameObject.SetActive(false);
                    DamageBlock.transform.Find("tho").gameObject.SetActive(false);
                }
            }
            else
            {
                DamageBlock.transform.Find("mil").gameObject.SetActive(false);
                DamageBlock.transform.Find("tho").gameObject.SetActive(false);
                DamageBlock.transform.Find("hun").gameObject.SetActive(false);
            }
        }
        else
        {
            DamageBlock.transform.Find("mil").gameObject.SetActive(false);
            DamageBlock.transform.Find("tho").gameObject.SetActive(false);
            DamageBlock.transform.Find("hun").gameObject.SetActive(false);
            DamageBlock.transform.Find("ten").gameObject.SetActive(false);
        }
        UnityEvent ev = new UnityEvent();
        ev.AddListener(()=> { MainBattleSystem.instance.ThisBoss.CallBossDamage(Damage); });
        ev.Invoke();
    }
    public void BossAttack()
    {
        TheBossAttack.Invoke();
        TheBossAttack.RemoveAllListeners();
    }
    public void BossEnd()
    {
        BossEndAnimation.Invoke();
        BossEndAnimation.RemoveAllListeners();
    }
    public void MonsterGetHit()
    {
        StartCoroutine("ChangeColor", MainBattleSystem.instance.BossSprites);
        MainBattleSystem.instance.BossSprite.GetComponent<Animator>().SetTrigger("GetHit");
    }
    public void PlayerGetHit()
    {
        StartCoroutine("ChangeColor", MainBattleSystem.instance.PlayerSprites);
        MainBattleSystem.instance.PlayerAnimator.SetTrigger("GetHit");
    }
    public void CamShake(float duration,float strength,int ratio)
    {
        Camera.main.transform.DOShakePosition(duration, strength, ratio);
    }
    public void CamShakeHit()
    {
        CamShake(0.2f, 0.5f, 30);
    }
    public void RefreshAnimationContent()
    {
        MainBattleSystem.instance.battleAnimationContents = new BattleAnimationContents();
    }
    private IEnumerator ChangeColor(List<SpriteRenderer> Prefab)
    {
        Material defualt = Prefab[0].material;
        for (int i = 0; i < Prefab.Count; i++)
        {
            Prefab[i].color = new Color(1,0,0,1);
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < Prefab.Count; i++)
        {
            Prefab[i].color = new Color(1, 1, 1, 1);
            Prefab[i].material = Resources.Load<Material>("BattleScene/BeHitShader/TurnWhite");
        }
        yield return new WaitForSeconds(0.07f);
        for (int i = 0; i < Prefab.Count; i++)
        {
            Prefab[i].material = BasicMat;
        }
    }
    private IEnumerator ChangeColor2(List<SpriteRenderer> Prefab)
    {
        Material defualt = Prefab[0].material;
        for (int i = 0; i < Prefab.Count; i++)
        {
            Prefab[i].material = Resources.Load<Material>("BattleScene/BeHitShader/TurnRed");
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < Prefab.Count; i++)
        {  
            Prefab[i].material = Resources.Load<Material>("BattleScene/BeHitShader/TurnWhite");
        }
        yield return new WaitForSeconds(0.07f);
        for (int i = 0; i < Prefab.Count; i++)
        {
            Prefab[i].material = defualt;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            MainBattleSystem.instance.BossSprite.GetComponent<Animator>().SetTrigger("GetHit");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine("ChangeColor2", MainBattleSystem.instance.PlayerSprites);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
        //    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(2);
            MainBattleSystem.instance.battleAnimationContents.FieldPrefab = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(3).FieldPrefab;
            MainBattleSystem.instance.PlayerAnimator.SetBool("MagicArray", true);
          }
        if (Input.GetKeyDown(KeyCode.U))
        {
            MainBattleSystem.instance.PlayerAnimator.SetBool("MagicArray", false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //    MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(2);
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(6).AnimationPrefab;
            MainBattleSystem.instance.PlayerAnimator.SetBool("Punch", true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MainBattleSystem.instance.PlayerAnimator.SetBool("Punch", false);
        }
    }
}
