using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float Speed;

    private void Update() 
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector2(-17f, transform.position.y), Speed * Time.deltaTime);
    }
}
