using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int m_amountGreen;
    [SerializeField] private GameObject m_bulletGreenPrefabs;
    private List<GameObject> m_listPoolBulletGreen = new List<GameObject>();
    //
    [SerializeField] private int m_amountRed;
    [SerializeField] private GameObject m_bulletRedPrefabs;
    private List<GameObject> m_listPoolBulletRed = new List<GameObject>();
    //
    [SerializeField] private int m_amountBlue;
    [SerializeField] private GameObject m_bulletBluePrefabs;
    private List<GameObject> m_listPoolBulletBlue = new List<GameObject>();
    void Start()
    {
        for(int i = 0; i < m_amountGreen; i++)
        {
            GameObject obj = Instantiate(m_bulletGreenPrefabs);
            obj.SetActive(false);
            m_listPoolBulletGreen.Add(obj);
        }
        //
        for (int i = 0; i < m_amountRed; i++)
        {
            GameObject obj = Instantiate(m_bulletRedPrefabs);
            obj.SetActive(false);
            m_listPoolBulletRed.Add(obj);
        }
        //
        for (int i = 0; i < m_amountBlue; i++)
        {
            GameObject obj = Instantiate(m_bulletBluePrefabs);
            obj.SetActive(false);
            m_listPoolBulletBlue.Add(obj);
        }
    }

    public GameObject GetPooledBulletGreen()
    {
        for(int i=0; i< m_amountGreen; i++)
        {
            if (!m_listPoolBulletGreen[i].activeInHierarchy)
                return m_listPoolBulletGreen[i];
        }
        return null;
    }
    public GameObject GetPooledBulletRed()
    {
        for (int i = 0; i < m_amountRed; i++)
        {
            if (!m_listPoolBulletRed[i].activeInHierarchy)
                return m_listPoolBulletRed[i];
        }
        return null;
    }
    public GameObject GetPooledBulletBlue()
    {
        for (int i = 0; i < m_amountBlue; i++)
        {
            if (!m_listPoolBulletBlue[i].activeInHierarchy)
                return m_listPoolBulletBlue[i];
        }
        return null;
    }
}
