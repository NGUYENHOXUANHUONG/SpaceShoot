using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletMove : MonoBehaviour
{
    public int index = 0;
    private Tween tween;
    public Vector2 pos;
    public Vector2 m_target;
  
    public float ang = 0;
    private void OnDestroy()
    {
        //tween.onKill();
    }
    public void Init(Vector2 direction, float angle, int id)
    {
        
        pos = transform.position;
        index = id;
        transform.eulerAngles = new Vector3(0, 0, angle);
        Vector2 temp = transform.position;
       
        m_target = direction;
        ang = angle;
        //t = Mathf.Atan2(angle);
        //m_target = new Vector2(temp.x - 5f, temp.y + 5f);
        //if (angle < 0)
        //    m_target = new Vector2(temp.x + 5f, temp.y + 5f);
       
        //transform.DOMove(m_target * 5f, 1.2f).SetEase(Ease.Linear).OnComplete(() => { gameObject.SetActive(false); });
        //transform.DOMoveY(direction.y + 5f, 1.2f).SetEase(Ease.Linear).OnComplete(() => { gameObject.SetActive(false); });

        //tween =  transform.DOMoveY(temp.y * 5f, 0.9f).SetEase(Ease.Linear).OnComplete(()=> {
        //    //Destroy(gameObject);
        //    //tween.onKill();
        //    gameObject.SetActive(false);
        //    //StartCoroutine(waitDestroy());
        //});
    }
    private void Update()
    {
        //transform.position += Vector3.up*5f * Time.deltaTime; 
        if (transform.position.y - pos.y > 9.5f)
            gameObject.SetActive(false);
        transform.Translate(Vector3.up * 15f * Time.deltaTime);
    }
    IEnumerator waitDestroy()
    {
        yield return null;
        Destroy(gameObject);
    }
        
}
