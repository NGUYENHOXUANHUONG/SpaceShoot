using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tập lệnh này xác định kích thước của 'Boundary' tùy thuộc vào Chế độ xem. Khi các đối tượng vượt ra ngoài 'Boundary', chúng sẽ bị phá hủy hoặc ngừng hoạt động.
/// </summary>
public class Boundary : MonoBehaviour {

    BoxCollider2D boundareCollider;

    private void Start()
    {
        boundareCollider = GetComponent<BoxCollider2D>();
        ResizeCollider();
    }

    //thay đổi kích thước của 'Collider' thành kích thước của Viewport nhân 1,5
    void ResizeCollider() 
    {        
        Vector2 viewportSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        viewportSize.x *= 1.5f;
        viewportSize.y *= 1.5f;
        boundareCollider.size = viewportSize;
    }

    //Xóa vật thể khi ra khỏi Collider
    private void OnTriggerExit2D(Collider2D collision) 
    {        
        if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Bonus"))
        {
            Destroy(collision.gameObject);
        }
        //else if (collision.CompareTag("Background"))
        //{
        //    Destroy(collision.gameObject);
        //}
        else if (collision.CompareTag("Planet"))
        {
            Destroy(collision.gameObject);
        }


    }

}
