using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Các đối tượng súng trong 'Player's'
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class PlayerShooting : MonoBehaviour {

    public float fireRate;
    public GameObject projectileObject;
    public GameObject projectileObject1;

    //thời gian 1 shot mới
    [HideInInspector] public float nextFire;

    //sức mạnh vũ khí hiện tại
    [Range(1, 8)]
    public int weaponPower = 1;
    public int maxWeaponPower = 8;

    public Guns guns;
    bool shootingIsActive = true; 
    public static PlayerShooting instance;
    UIManager uiManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (uiManager.gameOver)
        {
            return;
        }
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                if (Input.GetMouseButton(0))
                {
                    MakeAShot();
                    nextFire = Time.time + 1 / fireRate;
                }
                
            }
        }
    }

    //method for a shot
    void MakeAShot() 
    {
        switch (weaponPower) 
        {
            
            case 1:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                break;

            case 2:
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                break;

            case 3:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.centralGunVFX.Play();
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                break;

            case 4:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                guns.centralGunVFX.Play();
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                break;
            
            case 5:
                CreateLazerShot(projectileObject1, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                break;

            case 6:
                CreateLazerShot(projectileObject1, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject1, guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                break;

            case 7:
                CreateLazerShot(projectileObject1, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject1, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject1, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.centralGunVFX.Play();
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                break;

            case 8:
                CreateLazerShot(projectileObject1, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject1, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject1, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                CreateLazerShot(projectileObject1, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                CreateLazerShot(projectileObject1, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                guns.centralGunVFX.Play();
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                break;
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) 
    {
        Instantiate(lazer, pos, Quaternion.Euler(rot));
    }
    public void AddBulletPlayer(int valueBullet)
    {
        weaponPower += valueBullet;
    }
}
