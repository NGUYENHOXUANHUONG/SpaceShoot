using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("thời gian để tạo làn sóng 'Enemy' kể từ thời điểm trò chơi bắt đầu")]
    public float timeToStart;

    [Tooltip("Prefab của làn sóng 'Enemy'")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour {

    public EnemyWaves[] enemyWaves;
    Camera mainCamera;   

    private void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }
    }


    //Tạo một làn sóng mới sau một thời gian trì hoãn
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (Player.instance != null)
        {
            Instantiate(Wave);
        }
            
    }

}
