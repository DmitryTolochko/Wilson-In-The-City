using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalController : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    private float dX;

    private void Update() 
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                rb.AddForce(Vector2.up * Speed, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate() 
    {
        dX = Input.acceleration.x * Speed;
        transform.position = new Vector2(transform.position.x + dX * Time.deltaTime, transform.position.y);
    }
}
