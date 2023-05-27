using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBossMoving : MonoBehaviour
{
    public float speed;
    private Vector2 MoveDirection;
    private void Update()
    {
        transform.Translate(MoveDirection * speed * Time.deltaTime);
    }

    
    public void SetMoveDirection(Vector2 dir)
    {
        MoveDirection = dir;
    }
}
