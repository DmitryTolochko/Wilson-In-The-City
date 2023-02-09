using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalController : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;

    private float dX;
    private int pressCount;
    private bool IsOnCollision;

    private void Update() 
    {
        if (Time.timeScale != 0 && pressCount < 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                pressCount += 1;
                rb.AddForce(Vector2.up * Speed, ForceMode2D.Impulse);
            }
        }
        else if (transform.localPosition.y < -2.3f || IsOnCollision)
        {
            pressCount = 0;
            IsOnCollision = false;
        }
    }

    private void FixedUpdate() 
    {
        dX = Input.acceleration.x * Speed;
        transform.position = new Vector2(transform.position.x + dX * Time.deltaTime, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.name != "Background")
            IsOnCollision = true;
    }
}
