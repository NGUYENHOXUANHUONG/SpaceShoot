using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    public GameObject pooledBullet;
    public List<GameObject> bullets;
    private bool notEnoughBulletsInPlool = true;

    private Transform player;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
        bullets = new List<GameObject>();
        
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i] != null && !bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (notEnoughBulletsInPlool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        return null;
    }

}
