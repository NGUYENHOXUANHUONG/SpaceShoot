using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1 : MonoBehaviour
{
    public Transform target;
    public float movementSpeed = 5f;
    public GameObject destructionVFX;
    public GameObject hitEffect;
    public int health;


    public float bulletAmount = 10f;
    public float startAngle = 260f;
    public float endAngle = 100f;

    private Transform player;

    private Vector3 velocity = Vector3.zero;


    public GameObject pooledBullet1;
    public bool isShoottable = false;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCooldown;

    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("Fire", 0f, 2f);
    }


    private void Update() 
    {
        
        

        if (player == null)
        {
            return;
        }

        //
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, 0), ref velocity, 2f);

        fireCooldown -= Time.deltaTime;

        if (fireCooldown < 0)
        {
            fireCooldown = timeBtwFire;
            Fire1();
        }
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.instance.GetBullet();

            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BulletBossMoving>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }

    private void Fire1()
    {

        var bulletTmp = Instantiate(pooledBullet1, transform.position, player.transform.rotation);

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

        if (player != null)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 direction = playerPos - transform.position;
            //Vector2 direction = target.position - transform.position;


            float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
            bulletTmp.transform.eulerAngles = new Vector3(0, 0, angle - 270);
            //
            rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
    }


    private void MoveAndAttackPlayer()
    {
        if (player == null) 
        Debug.Log("Null player");

    }
    private IEnumerator WaitMoveAndAttackPlayer()
    {
        yield return new WaitForSeconds(1f);

    }

    private bool IsPlayerVisible()
    {
        return true;
    }

    private void MoveTowardsPlayer()
    {
        float x = player.position.x;
        transform.Translate(new Vector2(x, transform.position.y) * movementSpeed * Time.deltaTime);
    }


    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destruction();
            UIManager.instance.AddScore(100);
        }
        else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
