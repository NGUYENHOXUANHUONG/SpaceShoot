using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Tập lệnh này di chuyển 'Enemy' dọc theo đường đã xác định.
/// </summary>
public class FollowThePath : MonoBehaviour {
        
    [HideInInspector] public Transform [] path;     //điểm đường đi qua 'Enemy' 
    [HideInInspector] public float speed; 
    [HideInInspector] public bool rotationByPath;   //'Enemy' có xoay theo hướng đường đi hay không
    [HideInInspector] public bool loop;             //nếu vòng lặp là đúng, 'Enemy' sẽ quay lại điểm bắt đầu của đường dẫn sau khi hoàn thành đường dẫn
    float currentPathPercent;                       //phần trăm hiện tại của việc hoàn thành con đường
    Vector3[] pathPositions;                        //điểm đường dẫn trong vector3
    [HideInInspector] public bool movingIsActive;   //cho 'Enemy' di chuyển hay k

    //đặt tham số đường dẫn cho 'Enemy' và cho 'Enemy' đến điểm bắt đầu đường dẫn
    public void SetPath() 
    {
        currentPathPercent = 0;
        pathPositions = new Vector3[path.Length];       //chuyển đổi các điểm đường dẫn thành vector3
        for (int i = 0; i < pathPositions.Length; i++)
        {
            pathPositions[i] = path[i].position;
        }
        transform.position = NewPositionByPath(pathPositions, 0); //cho 'Enemy' đến điểm bắt đầu đường dẫn
        if (!rotationByPath)
            transform.rotation = Quaternion.identity;
        movingIsActive = true;
    }

    private void Update()
    {
        
        if (movingIsActive)
        {
            currentPathPercent += speed / 100 * Time.deltaTime;                        //cập nhật tính toán tỷ lệ phần trăm đường dẫn hiện tại theo speed

            transform.position = NewPositionByPath(pathPositions, currentPathPercent); //di chuyển 'Enemy' đến pathPositions, được tính theo phương thức NewPositionByPath
            if (rotationByPath)                                                         //xoay 'Enemy' theo hướng đường đi, nếu 'rotationByPath' đúng
            {
                transform.right = Interpolate(CreatePoints(pathPositions), currentPathPercent + 0.01f) - transform.position;
                transform.Rotate(Vector3.forward * 90);
            }
            if (currentPathPercent > 1)                    //khi con đường hoàn thành
            {
                if (loop)                                   //nếu loop đúng, di chuyển đến điểm bắt đầu; ngược lại xóa hoặc vô hiệu hóa 'Enemy'
                    currentPathPercent = 0;
                else
                {
                    Destroy(gameObject);           
                }
            }
        }
    }

    Vector3 NewPositionByPath(Vector3 [] pathPos, float percent) 
    {
        return Interpolate(CreatePoints(pathPos), currentPathPercent);
    }

    Vector3 Interpolate(Vector3[] path, float t)        //Tính toán vị trí 'Enemy' bằng pp Interpolation
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
