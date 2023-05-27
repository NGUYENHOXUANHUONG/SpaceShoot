using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_listTarget;
    public Vector3 angl;
    public List<obj> m_list;
    private void Start()
    {
        obj ob;
        for (int i = 0; i < m_listTarget.Count; i++)
        {
            ob = new obj();
            ob.obje = m_listTarget[i];
            ob.angle = m_listTarget[i].transform.eulerAngles;
            ob.pos = m_listTarget[i].transform.localPosition;
            m_list.Add(ob);
        }
        angl = m_listTarget[0].transform.eulerAngles;
        //Debug.Log(m_listTarget[0].transform.rotation.z);
    }
    public void OnSetActive(bool isActive)
    {
        for(int i=0; i< m_listTarget.Count; i++)
        {
            m_listTarget[i].transform.gameObject.SetActive(isActive);
        }
    }
    public List<obj> GetPoint()
    { 
        return m_list;
    }
}
[Serializable] 
public class obj
{
   public  GameObject obje;
   public Vector3 angle;
   public Vector3 pos;
}

