using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator Instance;

    public RuntimeAnimatorController WilsonController;
    public RuntimeAnimatorController RichardController;
    public RuntimeAnimatorController MarvinController;

    [HideInInspector]
    public Animator Animator;
    private new Collider2D collider;

    private void Start() 
    {
        Animator = transform.Find("Animator").GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        Instance = this;
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

    public static void ChangeSkin(SkinType type)
    {
        switch (type)
        {
            case SkinType.Wilson:
                Instance.Animator.runtimeAnimatorController = Instance.WilsonController;
                break;
            case SkinType.Richard:
                Instance.Animator.runtimeAnimatorController = Instance.RichardController;
                break;
            case SkinType.Marvin:
                Instance.Animator.runtimeAnimatorController = Instance.MarvinController;
                break;
        }
    }
}
