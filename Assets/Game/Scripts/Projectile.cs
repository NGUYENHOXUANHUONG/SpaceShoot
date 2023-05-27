using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Xác định thiệt hại và xác định đạn thuộc ‘Enemy’ hay ‘Player’, liệu đạn có bị phá hủy trong vụ va chạm hay không và mức độ thiệt hại.
/// </summary>

public class Projectile : MonoBehaviour {

    public int damage;
    public bool enemyBullet;
    public bool destroyedByCollision;


    private void Update ()
    {
        if (UIManager.instance.gameOver)
        {
            return;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyBullet && collision.tag == "Player")
        {
            if (destroyedByCollision)
            {
                Destroy(gameObject);
                UIManager.instance.SubHealth(damage);
                if (UIManager.instance.health == 0)
                {
                    Player.instance.Destruction();
                    UIManager.instance.EndGame();
                }
            }

        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            if (destroyedByCollision)
            {
                Destroy(gameObject);

            }

        }
        else if (!enemyBullet && collision.tag == "Boss1")
        {
            
            collision.GetComponent<Boss1>().GetDamage(damage);
            if (destroyedByCollision)
            {
                Destroy(gameObject);

            }
        }
    }
}


