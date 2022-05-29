using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleAnimationEvent : MonoBehaviour
{

    public void GenBattleEffect()
    {
        Instantiate(MainBattleSystem.instance.battleAnimationContents.BattleEffect, Vector3.zero, Quaternion.identity);
    }
    public void GenDamageNumber()
    {
        Vector3 Place = new Vector3(Random.Range(-20, 0) * 0.1f, 0f, Random.Range(-20, 0) * 0.1f);
        int Damage = MainBattleSystem.instance.battleAnimationContents.DamageDelt[MainBattleSystem.instance.battleAnimationContents.NowDisplayDamage];
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
        //©Çª«¦å±ø´î¤Ö
    }
    public void MonsterGetHit()
    {
        StartCoroutine("ChangeColor", MainBattleSystem.instance.BossSprites);
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
            Prefab[i].material = defualt;
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
            CamShake(0.2f, 0.3f,30);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine("ChangeColor2", MainBattleSystem.instance.PlayerSprites);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            MainBattleSystem.instance.battleAnimationContents.DamageDelt.Add(2);
            MainBattleSystem.instance.battleAnimationContents.BattleEffect = MainBattleSystem.instance.skillDatabaseOBJ.GetSkillInformation(2).AnimationPrefab;
            MainBattleSystem.instance.PlayerAnimator.SetBool("Medicine", true);
          }
        if (Input.GetKeyDown(KeyCode.U))
        {
            MainBattleSystem.instance.PlayerAnimator.SetBool("Medicine", false);
        }
    }
}
