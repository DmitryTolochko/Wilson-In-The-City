using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator Animator;
    private new Collider2D collider;

    private void Start() 
    {
        Animator = transform.Find("Animator").GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void Update() 
    {
        if (Mathf.Abs(transform.localPosition.y - (-2.745185f)) > 0.05f)
            Animator.SetBool("IsJumping", true);
        else 
            Animator.SetBool("IsJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.name != "Background")
        {
            Animator.SetBool("IsDrifting", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.transform.name == "Background")
            return;
        
        Animator.SetBool("IsDrifting", false);
    }
}
