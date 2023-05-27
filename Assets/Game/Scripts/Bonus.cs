using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player") 
        {
            PlayerShooting player = collision.GetComponent<PlayerShooting>(); 
            if (player == null)
                return;
            //Debug.Log(player.weaponPower + "==============="+ player.maxWeaponPower);
            if (player.weaponPower < player.maxWeaponPower)
                player.AddBulletPlayer(1); 
            Destroy(gameObject);
        }
        
    }
}
