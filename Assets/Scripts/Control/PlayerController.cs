using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;

    private int clicksCount;

    private void Update() 
    {
        
        // var dX = Input.acceleration.x * Speed;
        // rb.AddForce(Vector2.right * dX, ForceMode2D.Force);
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                rb.AddForce(Vector2.right * Speed * 5);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                rb.AddForce(Vector2.left * Speed * 5);
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                rb.AddForce(Vector2.up * Speed, ForceMode2D.Impulse);
            }
        }
    }
}
