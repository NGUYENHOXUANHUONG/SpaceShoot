using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Tập lệnh này tạo ra một làn sóng enemy. Xác định có bao nhiêu enemy sẽ xuất hiện, tốc độ và khoảng thời gian xuất hiện của chúng. 
/// Nó cũng xác định chế độ shooting của họ. Xác định con đường di chuyển của họ.
/// </summary>
[System.Serializable]
public class Shooting
{
    [Range(0,100)]
    [Tooltip("xác suất mà con tàu của làn sóng này sẽ thực hiện một phát bắn")]
    public int shotChance;

    [Tooltip("thời gian tối thiểu và tối đa kể từ khi bắt đầu con đường khi enemy có thể bắn")]
    public float shotTimeMin, shotTimeMax;
}

public class Wave : MonoBehaviour {

    #region FIELDS
    public GameObject enemy;

    [Tooltip("số lượng 'Enemy' trong làn sóng")]
    public int count;

    [Tooltip("tốc độ đường đi")]
    public float speed;

    [Tooltip("thời gian giữa sự xuất hiện của 'Enemy' trong làn sóng")]
    public float timeBetween;

    [Tooltip("điểm của đường đi. xóa hoặc thêm phần tử vào danh sách nếu bạn muốn thay đổi số điểm")]
    public Transform[] pathPoints;

    [Tooltip("kiểm tra liệu 'Enemy' có xoay theo hướng lối đi hay không")]
    public bool rotationByPath;

    [Tooltip("nếu vòng lặp được kích hoạt, sau khi hoàn thành con đường 'Enemy' sẽ quay trở lại điểm xuất phát")]
    public bool Loop;

    [Tooltip("màu của đường dẫn trong Editor")]
    public Color pathColor = Color.yellow;
    public Shooting shooting;

    [Tooltip("nếu testMode được đánh dấu, sóng sẽ được tạo lại sau 3 giây")]
    public bool testMode;
    #endregion


    private void Start()
    {
        StartCoroutine(CreateEnemyWave()); 
    }

    
    IEnumerator CreateEnemyWave()
    {
        
        for (int i = 0; i < count; i++)
        {
            GameObject newEnemy;
            newEnemy = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
            FollowThePath followComponent = newEnemy.GetComponent<FollowThePath>();
            followComponent.path = pathPoints;
            followComponent.speed = speed;
            followComponent.rotationByPath = rotationByPath;
            followComponent.loop = Loop;
            followComponent.SetPath();
            Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
            enemyComponent.shotChance = shooting.shotChance;
            enemyComponent.shotTimeMin = shooting.shotTimeMin;
            enemyComponent.shotTimeMax = shooting.shotTimeMax;
            newEnemy.SetActive(true);
            yield return new WaitForSeconds(timeBetween);
        }
        if (testMode)       //nếu testMode được kích hoạt, đợi 5 giây và tạo lại wave
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(CreateEnemyWave());
        }
        else if (!Loop)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()  
    {
        DrawPath(pathPoints);  
    }

    void DrawPath(Transform[] path) //vẽ đường dẫn trong Editor
    {
        Vector3[] pathPositions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            pathPositions[i] = path[i].position;
        }
        Vector3[] newPathPositions = CreatePoints(pathPositions);
        Vector3 previosPositions = Interpolate(newPathPositions, 0);
        Gizmos.color = pathColor;
        int SmoothAmount = path.Length * 20;
        for (int i = 1; i <= SmoothAmount; i++)
        {
            float t = (float)i / SmoothAmount;
            Vector3 currentPositions = Interpolate(newPathPositions, t);
            Gizmos.DrawLine(currentPositions, previosPositions);
            previosPositions = currentPositions;
        }
    }

    Vector3 Interpolate(Vector3[] path, float t)    //sử dụng phương pháp nội suy(interpolation) tính toán đường dẫn dọc theo các điểm đường dẫn
    {
        int numSections = path.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;
        Vector3 a = path[currPt];
        Vector3 b = path[currPt + 1];
        Vector3 c = path[currPt + 2];
        Vector3 d = path[currPt + 3];
        return 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }

    Vector3[] CreatePoints(Vector3[] path)  
    {
        Vector3[] pathPositions;
        Vector3[] newPathPos;
        int dist = 2;
        pathPositions = path;
        newPathPos = new Vector3[pathPositions.Length + dist];
        Array.Copy(pathPositions, 0, newPathPos, 1, pathPositions.Length);
        newPathPos[0] = newPathPos[1] + (newPathPos[1] - newPathPos[2]);
        newPathPos[newPathPos.Length - 1] = newPathPos[newPathPos.Length - 2] + (newPathPos[newPathPos.Length - 2] - newPathPos[newPathPos.Length - 3]);
        if (newPathPos[1] == newPathPos[newPathPos.Length - 2])
        {
            Vector3[] LoopSpline = new Vector3[newPathPos.Length];
            Array.Copy(newPathPos, LoopSpline, newPathPos.Length);
            LoopSpline[0] = LoopSpline[LoopSpline.Length - 3];
            LoopSpline[LoopSpline.Length - 1] = LoopSpline[2];
            newPathPos = new Vector3[LoopSpline.Length];
            Array.Copy(LoopSpline, newPathPos, LoopSpline.Length);
        }
        return (newPathPos);
    }
}
