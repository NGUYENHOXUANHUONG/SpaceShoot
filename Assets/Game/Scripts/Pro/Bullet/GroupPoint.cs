using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupPoint : MonoBehaviour
{
    [SerializeField] private List<StartPoint> m_listTarget;
    // Start is called before the first frame update
    public void OnSetActive(bool isActive)
    {
        for (int i = 0; i < m_listTarget.Count; i++)
        {
            m_listTarget[i].OnSetActive(isActive);
        }
    }
    public List<obj> GetPointGroup(int index)
    {
        return  m_listTarget[index].GetPoint();
    }
    public int GetCountPoint()
    {
        return m_listTarget.Count;
    }
}
