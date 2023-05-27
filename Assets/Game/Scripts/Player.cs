using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tập lệnh giúp di chuyển và tạo hiệu ứng phá hủy cho 'Player'
/// </summary>

public class Player : MonoBehaviour
{
    public float minX, maxX, minY, maxY;

    bool controlIsActive = true;
    public float speed;
    Camera mainCamera;
    public GameObject destructionFX;
    public GameObject hitEffect;
    public static Player instance;
    public Transform playerTransform;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        playerTransform = transform;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();
    }

    void Update()
    {
        if (UIManager.instance.gameOver)
        {
            return;
        }

        if (controlIsActive)
        {
            // Chuyển đổi vị trí của chuột sang vị trí trong không gian thế giới
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            // Giới hạn phạm vi di chuyển của Player
            worldPos.x = Mathf.Clamp(worldPos.x, minX, maxX);
            worldPos.y = Mathf.Clamp(worldPos.y, minY, maxY);

            // Di chuyển Player đến vị trí của chuột
            transform.position = Vector3.MoveTowards(transform.position, worldPos, speed);
        }
    }
    void ResizeBorders()
    {
        // Tính toán giới hạn di chuyển của Player
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }

    public void GetDamage(int damage)   
    {
        Destruction();

    }

    public void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //tạo hiệu ứng hình ảnh hủy diệt và phá hủy đối tượng 'Player'
        Destroy(gameObject);
    }
   
}
















