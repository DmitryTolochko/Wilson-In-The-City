using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private new Collider2D collider;

    private void Start() 
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.name == "Background")
            return;

        if (other.collider.bounds.size.y/2 + other.transform.position.y <=
            transform.localPosition.y - collider.bounds.size.y/2)
            StartCoroutine(GameModel.StartGameOverRoutine());
        // else if (Mathf.Abs((other.collider.bounds.size.y/2 + other.transform.position.y) -
        //     (transform.localPosition.y - collider.bounds.size.y/2)) <= 0.5f)

        // print((other.collider.bounds.size.y/2 + other.transform.position.y) + "   " + 
        // (transform.position.y - collider.bounds.size.y/2));
    }
}
