using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Flaganimation : MonoBehaviour
{
    public GameObject flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Startflaganimation(Vector3 _endpoint)
    {
        StartCoroutine("flaganimation",_endpoint);

    }
    public IEnumerator flaganimation(Vector3 endpoint)
    {
        transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutQuart);
        transform.DOMove(endpoint, 1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.55f);
        transform.DOPunchScale(new Vector3(0.3f, 0.1f, 0.1f), 0.5f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.5f);
        transform.position =endpoint;

        //transform.DOPunchScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f);
        
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.9f).SetEase(Ease.OutBounce);

        //yield return new WaitForSeconds(0.005f);

        //transform.DORotate(new Vector3(-360, 1230, 0), 0.1f).SetEase(Ease.OutQuad);



        yield return null;

    }
}
