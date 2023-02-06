using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public Rigidbody2D rb;

    private new Collider2D collider;
    public static PlayerModel Instance;

    private void Start() 
    {
        Instance = this;
        collider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.name == "Background")
            return;

        OnTriggerEnter2D(other.collider);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent<CollectableItem>(out var item))
        {
            item.IsCollected = true;
            return;
        }

        if ((other.bounds.size.y/2 - other.transform.position.y >
            transform.localPosition.y - collider.bounds.size.y/2)
        && (other.transform.position.x - other.bounds.size.x/2 >=
            transform.localPosition.x))
            StartCoroutine(GameModel.StartGameOverRoutine());
    }

    public static void Reset()
    {
        Instance.transform.localPosition = new Vector2(-6.58f, -2.7f);
        Instance.rb.gravityScale = 9.8f;
        Instance.rb.mass = 0.8f;
    }
}
