using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalController : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    private float dX;

    private void FixedUpdate() 
    {
        dX = Input.acceleration.x * Speed;
        transform.position = new Vector2(transform.position.x + dX * Time.deltaTime, transform.position.y);
    }
}
