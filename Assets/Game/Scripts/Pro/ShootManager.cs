using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootManager : MonoBehaviour
{

    [SerializeField] private List<Transform> a1 = new List<Transform>();
    [SerializeField] private List<Transform> a2 = new List<Transform>();
    [SerializeField] private List<Transform> a3 = new List<Transform>();
    [SerializeField] private List<List<Transform>> a;
    //
    [SerializeField] private List<Transform> b1 = new List<Transform>();
    [SerializeField] private List<Transform> b2 = new List<Transform>();
    [SerializeField] private List<Transform> b3 = new List<Transform>();
    [SerializeField] private List<List<Transform>> b;
    //
    [SerializeField] private List<Transform> c1 = new List<Transform>();
    [SerializeField] private List<Transform> c2 = new List<Transform>();
    [SerializeField] private List<Transform> c3 = new List<Transform>();
    [SerializeField] private List<List<Transform>> c ;
    //
    private  List<List<List<Transform>>> m_list;
    //
    private int m_count = 0;
    private TypeColorBullet m_currenTypeBullet = TypeColorBullet.red;
    private int m_indexType = 0;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateTypeBullet(TypeColorBullet type)
    {
        if(type != m_currenTypeBullet)
        {
            m_currenTypeBullet = type;
            if (m_count > 1)
                m_count = 1;
        }else
        {
            if (m_count > 2)
                m_count = 3;
            else
                m_count++;
        }
        SwitchBullet(m_currenTypeBullet);
        //

    }
    private void Off()
    {
        //foreach(List<List<List<Transform>>> item in m_list)
        //{

        //}
        //for (int i=0; i < m_list.Count; i++)
        //{
        //    for (int j = 0; j < m_list[j].Count; j++)
        //    {
                
        //    }
        //}
    }
    void SwitchBullet(TypeColorBullet type)
    {
        switch(type)
        {
            case TypeColorBullet.red:
                m_indexType = 0;
                break;
            case TypeColorBullet.blue:
                m_indexType = 1;
                break;
            case TypeColorBullet.green:
                m_indexType = 2;
                break;
        }
    }
}




