using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tập lệnh này xác định sức khỏe và hành vi của 'Enemy's'
/// </summary>
public class Enemy : MonoBehaviour
{

    #region FIELDS

    public int health;

    [Tooltip("Prefab đạn của 'Enemy's")]
    public GameObject Projectile;

    [Tooltip("VFX prefab tạo ra sau khi phá hủy")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    [HideInInspector] public int shotChance;
    [HideInInspector] public float shotTimeMin, shotTimeMax;
    #endregion


    private void Start()
    {
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));

    }

    void ActivateShooting()
    {
        if (UIManager.instance.gameOver)
        {
            return;
        }
        if (Random.value < (float)shotChance / 100)
        {
            
            Instantiate(Projectile, gameObject.transform.position, Player.instance.transform.rotation);//Quaternion.identity);
        }
    }

    //Phương thức gaay sát thương cho 'Enemy'
    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destruction();
            UIManager.instance.AddScore(1);
        }
        else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }

    }

    //nếu 'Enemy' va chạm với 'Player' sẽ Endgame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.Destruction();
            UIManager.instance.health = 0;
            UIManager.instance.EndGame();
        }
    }

    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
