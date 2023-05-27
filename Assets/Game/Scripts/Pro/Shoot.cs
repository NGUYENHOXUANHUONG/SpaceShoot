using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Shoot : MonoBehaviour
{
    [SerializeField] private List<TypeBulletPro> m_listBulletPro;
    [SerializeField] private ObjectPool m_objPool;
    //
    public List<obj> m_listpoint;
   
    public TypeColorBullet typeColor = TypeColorBullet.red;
    public int indexCountBullet = 0;
    public float timeTo = 0.5f;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Wait());
        DOTween.SetTweensCapacity(20000, 125);

    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeTo)
        {
            UpdateType();
            time = 0;
        }
        
    }
    private void UpdateType()
    {
       
        int indexColorBullet = 0;
        switch (typeColor)
        {
            case TypeColorBullet.red:
                indexColorBullet = 0;
                break;
            case TypeColorBullet.blue:
                indexColorBullet = 1;
                break;
        }
        //
        int countPoint = m_listBulletPro[indexColorBullet].groupPoint.GetCountPoint();
        if (indexCountBullet > countPoint -2)
        {
            //return;
            indexCountBullet = countPoint -1;
        }
        m_listpoint = m_listBulletPro[indexColorBullet].groupPoint.GetPointGroup(indexCountBullet);
        if (m_listpoint == null)
            return;
        //
        //GameObject obj = null; //= m_objPool.GetPooledBulletRed();//Instantiate(m_bulletPrefabs);
        switch (typeColor)
        {
            case TypeColorBullet.red:
                if (m_listpoint.Count < 1)
                    return;
                //indexColorBullet = 0;
                for (int i = 0; i < m_listpoint.Count; i++)
                {
                    GameObject obj = m_objPool.GetPooledBulletRed();
                    if (obj == null)
                        return;
                    obj.transform.position = m_listpoint[i].pos + transform.position;
                    obj.GetComponent<BulletMove>().Init(transform.position, m_listpoint[i].angle.z, i);
                    obj.SetActive(true);
                }
                break;
            case TypeColorBullet.blue:
                //indexColorBullet = 1;
                for (int i = 0; i < m_listpoint.Count; i++)
                {
                    GameObject obj = m_objPool.GetPooledBulletBlue();
                    if (obj == null)
                        return;
                    obj.transform.position = m_listpoint[i].pos + transform.position;
                    obj.GetComponent<BulletMove>().Init(transform.position, m_listpoint[i].angle.z, i);
                    obj.gameObject.SetActive(true);
                }
                break;
            case TypeColorBullet.green:
                //indexColorBullet = 1;
                for (int i = 0; i < m_listpoint.Count; i++)
                {
                    GameObject obj = m_objPool.GetPooledBulletGreen();
                    if (obj == null)
                        return;
                    obj.transform.position = m_listpoint[i].pos + transform.position;
                    obj.GetComponent<BulletMove>().Init(transform.position, m_listpoint[i].angle.z, i);
                    obj.gameObject.SetActive(true);
                }
                break;
        }

       
       
        
    }
   
}
[Serializable]
public class TypeBulletPro
{
    public GroupPoint groupPoint;
    public TypeColorBullet typeBulet;
    public GameObject prefabBullet;
}

[Serializable]
public enum TypeColorBullet
{
    red,
    blue,
    green
}