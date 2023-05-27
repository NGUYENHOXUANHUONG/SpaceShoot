using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] planets;
    public GameObject[] backGround;
    public GameObject bonus;
    public GameObject boss1;
    
    
    private void Awake()
    {
        InvokeRepeating("BackGround", 2.0f, 20.0f);
        InvokeRepeating("Bonus", 2.0f, 10.0f);
        InvokeRepeating("Planets", 2.0f, 10.0f);
        Invoke("Boss1", 60.0f);

    }

    private void Boss1()
    {
        Instantiate(boss1, new Vector3(0, 5.8f, 0f), Quaternion.identity);
    }

    private void Planets()
    {
        
        int index = Random.Range(0, planets.Length);
        Instantiate(planets[index], new Vector3(Random.Range(-20.0f, 20.0f), 12, 0), planets[index].transform.rotation);
        if (planets[index] != null && planets[index].transform.position.y < -11)
        {
            Destroy(planets[index]);
        }
    }

    private void BackGround()
    {
        int index = Random.Range(0, backGround.Length);
        GameObject bg = Instantiate(backGround[index]); 
        bg.transform.position = new Vector3(0, 25, 0); 
        StartCoroutine(WaitDestroyBG(bg));
    }

    private void Bonus()
    {
        Vector3 spawnPosTop = new Vector3(Random.Range(-20.0f, 20.0f), 11.0f, 0.0f);
        Instantiate(bonus, spawnPosTop, bonus.transform.rotation);
    }

    private IEnumerator WaitDestroyBG(GameObject bg) 
    {
        yield return new WaitForSeconds(50f);
        Destroy(bg);
    }

}
