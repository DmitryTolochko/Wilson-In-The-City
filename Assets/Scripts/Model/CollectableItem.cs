using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public float Speed;
    public PoolObjectType Type;
    public bool IsCollected;
    public bool IsInvisible;
    public bool IsRaised;

    private void Update() 
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector2(-17f, transform.position.y), Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (IsRaised)
            return;
        if (other.transform.name == "Player")
            return; 
        if (other.transform.name == "Background")
            transform.position = new Vector2(
                transform.position.x, 
                0f);
        else
            transform.position = new Vector2(
                transform.position.x, 
                transform.position.y + other.bounds.size.y + 0.5f);
        
        IsRaised = true;
    }

    private void OnBecameInvisible() 
    {
        if (transform.position.x < 0)
            IsInvisible = true;
    }
}
