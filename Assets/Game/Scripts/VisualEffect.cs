using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tập lệnh này gắn vào các đối tượng 'VisualEffect'. Nó phá hủy hoặc vô hiệu hóa chúng sau thời gian xác định.
/// </summary>
public class VisualEffect : MonoBehaviour {

    public float destructionTime;

    private void OnEnable()
    {
        StartCoroutine(Destruction()); //khởi động bộ đếm thời gian hủy diệt
    }

    IEnumerator Destruction() //đợi thời gian ước tính và hủy hoặc hủy kích hoạt đối tượng
    {
        yield return new WaitForSeconds(destructionTime); 
        Destroy(gameObject);
    }
}
